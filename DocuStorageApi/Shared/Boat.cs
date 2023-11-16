namespace DocuStorageApi.Shared;

public class Boat
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<FileReference> AllFiles { get; set; } = new();
}
