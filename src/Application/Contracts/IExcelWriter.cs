using Microsoft.AspNetCore.Mvc;
using TajneedApi.Application.Common;

namespace TajneedApi.Application.Contracts;

public interface IExcelWriter
{
    FileContentResult GenerateCSV<T>(IList<T> data, string fileName = "ExportData");
    FileContentResult GenerateExcel<T>(IList<T> data, string fileName = "ExportData");
}