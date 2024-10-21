namespace TajneedApi.Application.Common;

public class FileResponseModel
{
    public byte[]? FileContent { get; set; }
    public string? FileName { get; set; }
    public string? ContentType { get; set; }
}