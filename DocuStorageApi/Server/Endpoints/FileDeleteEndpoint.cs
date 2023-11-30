using DocuStorageApi.Shared;
using DocuStorageApi.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DocuStorageApi.Server.Endpoints;

internal record FileDeleteEndpoint
(
    FileAdapter _api
)
{
    public static void Map(WebApplication app)
        => app
        .MapDelete("/api/v1/files/{id:int}", HandleAsync);

    private async static Task<BoatDtoV1?> HandleAsync(
        [FromServices] FileDeleteEndpoint instance,
        [FromRoute] int id)
    {
        var result = await instance._api.DeleteFile(id);
        return result;
    }

}
