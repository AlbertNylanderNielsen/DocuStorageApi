using DocuStorageApi.Server.Data;
using DocuStorageApi.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DocuStorageApi.Server.Endpoints;

internal record FilePostEndpoint
(
    FilesDBContext _dB,
    FileAdapter _api
)
{
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
    =>        await _api.PutFile(file);

}
