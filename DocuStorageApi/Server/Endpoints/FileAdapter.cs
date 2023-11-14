using DocuStorageApi.Server.Data;
using DocuStorageApi.Shared;

namespace DocuStorageApi.Server.Endpoints;

internal record FileAdapter
(
    FilesDBContext _db
)
{
    public async Task<IList<FileReference>> ListFilesAsync()
    {
        await Task.CompletedTask;
        return _db.FileReferences;
    }

    public async Task<Stream> GetFile(int id)
    {
        await Task.CompletedTask;
        var filePath = _db.FileReferences.Single(f => f.Id == id).Path;

        return File.OpenRead(filePath);
    }

    public async Task PutFile(IFormFile file)
    {
        await Task.CompletedTask;

        // Copy file to Document Storage location
        var azurePath = @"D:\InFocusTest\AzureFileStorage";
        var path = Path.Combine(azurePath, file.Name);
        await using var fs = new FileStream(path, FileMode.Create);
        await file.OpenReadStream().CopyToAsync(fs);

        // Add database record for file
        _db.FileReferences.Add(new FileReference { Name = file.Name, Path = path, SizeBytes = 0 });
        _db.SaveChanges();

    }

}
