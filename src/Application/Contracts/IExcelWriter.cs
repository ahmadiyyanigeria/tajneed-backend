using Microsoft.AspNetCore.Mvc;

namespace TajneedApi.Application.Contracts;

public interface IExcelWriter
{
    Task<FileContentResult> GenerateCSVAsync<T>(IEnumerable<T> data, string fileName = "ExportData");
    FileContentResult GenerateExcel<T>(IEnumerable<T> data, string fileName = "ExportData");
}