using DocuStorageApi.Server.Data;
using DocuStorageApi.Shared;
using DocuStorageApi.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DocuStorageApi.Server.Endpoints;

internal record FilePostEndpoint
(
    FocusDbContext _dB,
    FileAdapter _api
)
{
    public static void Map(WebApplication app)
    => app
    .MapPost("/api/v1/files", HandleAsync)
    .WithName(nameof(FilePostEndpoint))
    .WithTags("File")
    .Produces<BoatDtoV1>(StatusCodes.Status200OK, "application/json");

    private async static Task<BoatDtoV1> HandleAsync(
     [FromServices] FilePostEndpoint instance,
     [FromForm] IFormFile file)
    {
        var result = await instance.PostAsync(file);
        return result;
    }
 
    private async Task<BoatDtoV1> PostAsync(IFormFile file)
        => await _api.PostFile(file);

}
