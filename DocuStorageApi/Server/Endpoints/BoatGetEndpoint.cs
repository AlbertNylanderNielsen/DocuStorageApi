using DocuStorageApi.Shared;
using DocuStorageApi.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DocuStorageApi.Server.Endpoints;

internal record BoatGetEndpoint
(
    FileAdapter _api
)
{
    public static void Map(WebApplication app)
        => app
        .MapGet("/api/v1/Boats/{id:int}", HandleAsync);

    private async static Task<IResult> HandleAsync(
        [FromServices] BoatGetEndpoint instance,
        [FromRoute] int id)
    {
        var result = await instance.GetAsync(id);

        return 
            result is not null 
            ? Results.Ok(result) 
            : Results.NotFound();
    }

    private Task<BoatDtoV1?> GetAsync(int id)
        => this._api.GetAsync(id);
}

