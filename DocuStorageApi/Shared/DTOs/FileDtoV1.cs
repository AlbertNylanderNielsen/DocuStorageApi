namespace DocuStorageApi.Shared.DTOs;

public class FileDtoV1
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public long SizeBytes { get; set; }
}
