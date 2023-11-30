using DocuStorageApi.Server.Data;
using DocuStorageApi.Shared;
using DocuStorageApi.Shared.DTOs;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DocuStorageApi.Server.Endpoints;

public record FileAdapter
(
    DBContext _context
)
{

    public async Task<BoatDtoV1?> GetAsync(int id)
    {
        await Task.CompletedTask;
        var b = this._context.Boats;
        return  this._context
                .Boats
                .Where(b => b.Id == id)
                .ToBoatDtoV1();

    }

    public async Task<Stream> GetFileStreamAsync(int id)
    {
        await Task.CompletedTask; // don't need this

        var path = this._context.Files.Where(f => f.Id == id).Single().Path;

        return File.OpenRead(path);
    }

    public async Task<BoatDtoV1?> DeleteFile(int id)
    {
        await Task.CompletedTask;
        var path = this._context.Files.Where(f => f.Id == id).Single().Path;

        File.Delete(path);

        // Remove database record for file
        this._context.Boats.First().Files.Remove(this._context.Files.Single(f => f.Id == id));
        this._context.SaveChanges();

        return this._context
         .Boats
         .Include(b => b.Files)
         .ToBoatDtoV1();
    }

    public async Task<BoatDtoV1?> PostFile(IFormFile file)
    {
        await Task.CompletedTask;

        // Copy file to Document Storage location
        var azurePath = @"D:\InFocusTest\AzureFileStorage";
        var path = Path.Combine(azurePath, file.FileName);
        EnsureTargetDirectoryExists();
        await using var fs = new FileStream(path, FileMode.Create);
        await file.OpenReadStream().CopyToAsync(fs);

        // Add database record for file
        this._context.Boats.First().Files.Add(new FileReference { Name = file.FileName, Path = path, SizeBytes = file.Length });
        this._context.SaveChanges();

        return this._context
                .Boats
                .Include(b => b.Files)
                .ToBoatDtoV1();

        // local functions...

        void EnsureTargetDirectoryExists()
            => Directory.CreateDirectory(DirectoryPath());

        //string FullPath()
        //    => Path.Combine(DirectoryPath(), TargetFileName());

        string DirectoryPath()
            => Path.Combine(Directory.GetCurrentDirectory(), azurePath);

        //string NewResourceUrl()
        //    => $"https://localhost:44302/Data/{TargetFileName()}";

        //string TargetFileName()
        //    => $"{file.FileName.Replace('.', '-')}-{Guid.NewGuid()}";

    }

}
