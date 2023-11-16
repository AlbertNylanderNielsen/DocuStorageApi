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

    public async Task DeleteFile(int id)
    {
        await Task.CompletedTask;
        var filePath = _db.FileReferences.Single(f => f.Id == id).Path;

        File.Delete(filePath);

        // Remove database record for file
        _db.FileReferences.Remove(_db.FileReferences.Single(f => f.Id == id));
        _db.SaveChanges();
    }

    public async Task<string> PutFile(IFormFile file)
    {
        await Task.CompletedTask;

        // Copy file to Document Storage location
        var azurePath = @"D:\InFocusTest\AzureFileStorage";
        var path = Path.Combine(azurePath, file.Name);
        EnsureTargetDirectoryExists();
        await using var fs = new FileStream(path, FileMode.Create);
        await file.OpenReadStream().CopyToAsync(fs);

        // Add database record for file
        _db.FileReferences.Add(new FileReference { Name = file.FileName, Path = path, SizeBytes = file.Length });
        _db.SaveChanges();

        return NewResourceUrl();


        // local functions...

        void EnsureTargetDirectoryExists()
            => Directory.CreateDirectory(DirectoryPath());

        string FullPath()
            => Path.Combine(DirectoryPath(), TargetFileName());

        string DirectoryPath()
            => Path.Combine(Directory.GetCurrentDirectory(), azurePath);

        string NewResourceUrl()
            => $"https://localhost:44302/Data/{TargetFileName()}";

        string TargetFileName()
            => $"{file.FileName.Replace('.', '-')}-{Guid.NewGuid()}";

    }

}
