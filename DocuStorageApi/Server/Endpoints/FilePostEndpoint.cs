using DocuStorageApi.Server.Data;
using DocuStorageApi.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DocuStorageApi.Server.Endpoints;

public class FilePostEndpoint
{
    [Inject]
    FilesDBContext _dB {  get; set; }

    string azurePath = @"D:\InFocusTest\AzureFileStorage";

    public static void Map(WebApplication app)
     => app
     .MapPost("/api/v1/files", HandleAsync)
     .WithName(nameof(FilePostEndpoint))
     .WithTags("File");

    private async static Task<IResult> HandleAsync(
     [FromServices] FilePostEndpoint instance,
     [FromForm] IFormFile file)
     => file != null
         ? Results.Ok(await instance.PostAsync(file))
         : Results.BadRequest("File is required");

    private async Task<string> PostAsync(IFormFile file)
    {
        EnsureTargetDirectoryExists();
        using var fileStream = new FileStream(FullPath(), FileMode.Create, FileAccess.Write);
        await file.CopyToAsync(fileStream);

        //FileInfo oFileInfo = new FileInfo(file.FileName);
        //var fileRecord = new FileReference
        //{
        //    Id = _dB.FileReferences.Count
        //                ,
        //    Name = oFileInfo.Name
        //                ,
        //    Path = oFileInfo.FullName
        //                ,
        //    SizeBytes = oFileInfo.Length
        //};

        //_dB.FileReferences.Add(fileRecord);

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
