namespace Domain.Dtos;

public class FileOptions
{
    public string FileName { get; set; }
    public string BucketName { get; set; }
    public string FileUrl { get; set; }

    public FileOptions()
    {
        FileName = string.Empty;
        BucketName = string.Empty;
        FileUrl = string.Empty;
    }

    public FileOptions(string fileName, string bucketName, string fileUrl)
    {
        FileName = fileName;
        BucketName = bucketName;
        FileUrl = fileUrl;
    }
}
