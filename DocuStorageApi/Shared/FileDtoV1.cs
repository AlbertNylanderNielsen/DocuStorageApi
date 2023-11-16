using System.ComponentModel.DataAnnotations;

namespace DocuStorageApi.Shared;

public class FileDtoV1
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public long SizeBytes { get; set; }
}
