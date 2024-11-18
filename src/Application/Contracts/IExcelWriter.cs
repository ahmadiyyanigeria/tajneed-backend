using Microsoft.AspNetCore.Mvc;

namespace TajneedApi.Application.Contracts;

public interface IExcelWriter
{
    FileContentResult GenerateCSV<T>(IEnumerable<T> data, string fileName = "ExportData");
    FileContentResult GenerateExcel<T>(IEnumerable<T> data, string fileName = "ExportData");
}