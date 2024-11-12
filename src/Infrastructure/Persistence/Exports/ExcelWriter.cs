using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Data;
using TajneedApi.Application.Contracts;
using TajneedApi.Domain.Exceptions;

namespace TajneedApi.Infrastructure.Persistence.Exports;

public class ExcelWriter : IExcelWriter
{
    public FileContentResult GenerateExcel<T>(IList<T> data, string fileName = "ExportData")
    {
        if (data == null || !data.Any())
            throw new DomainException($"Cannot generate Excel file for empty list.", ExceptionCodes.DocumentExportListIsNull.ToString(), 403);

        var properties = TypeDescriptor.GetProperties(typeof(T));
        var table = new DataTable("table", "table");

        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.DisplayName, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

        foreach (var item in data)
        {
            var row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.DisplayName] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }

        if (table.Columns.Count == 0 || table.Rows.Count == 0)
            throw new DomainException($"No data to write to the Excel file..", ExceptionCodes.DocumentExportListIsNull.ToString(), 403);

        using var wb = new XLWorkbook();
        wb.Worksheets.Add(table);

        using var memoryStream = new MemoryStream();
        wb.SaveAs(memoryStream);
        memoryStream.Seek(0, SeekOrigin.Begin);

        byte[] resultBytes = memoryStream.ToArray();
        //string base64String = Convert.ToBase64String(resultBytes);
        var todayDate = DateTime.UtcNow;

        return new FileContentResult(resultBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
        {
            FileDownloadName = $"{fileName}_{todayDate:yyyyMMddHHmmss}.xlsx",
        };
    }

    public FileContentResult GenerateCSV<T>(IList<T> data, string fileName = "ExportData")
    {
        if (data == null || !data.Any())
            throw new DomainException($"Cannot generate CSV file for empty list.", ExceptionCodes.DocumentExportListIsNull.ToString(), 403);

        var properties = TypeDescriptor.GetProperties(typeof(T));
        using var memoryStream = new MemoryStream();
        using var streamWriter = new StreamWriter(memoryStream);

        foreach (PropertyDescriptor prop in properties)
            streamWriter.Write(prop.DisplayName + ",");

        streamWriter.WriteLine();
        foreach (var item in data)
        {
            foreach (PropertyDescriptor prop in properties)
            {
                string value = prop.GetValue(item)?.ToString() ?? string.Empty;
                streamWriter.Write(value + ",");
            }
            streamWriter.WriteLine();
        }

        streamWriter.Flush();
        memoryStream.Seek(0, SeekOrigin.Begin);

        byte[] resultBytes = memoryStream.ToArray();
        //string base64String = Convert.ToBase64String(resultBytes);

        var todayDate = DateTime.UtcNow;
        return new FileContentResult(resultBytes, "text/csv")
        {
            FileDownloadName = $"{fileName}_{todayDate:yyyyMMddHHmmss}.csv",
        };
    }

}


