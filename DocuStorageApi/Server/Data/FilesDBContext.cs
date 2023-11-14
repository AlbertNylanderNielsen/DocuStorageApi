using DocuStorageApi.Shared;
using System.Text.Json;

namespace DocuStorageApi.Server.Data;

public class FilesDBContext
{
    private string _path = @"D:\InFocusTest\FileStorageDB\fileshare.db";

    public FilesDBContext()
    {
        if (File.Exists(_path))
            this.FileReferences = JsonSerializer.Deserialize<IList<FileReference>>(File.ReadAllText(_path))!;

        else
            this.FileReferences = new List<FileReference>();
    }

    public void SaveChanges()
    {
        foreach (var file in this.FileReferences.Where(f => f.Id == 0).ToList())
            file.Id = this.FileReferences.Max(f => f.Id) + 1;
        File.WriteAllText(_path, JsonSerializer.Serialize(this.FileReferences));
    }

    public IList<FileReference> FileReferences { get; set; }
}