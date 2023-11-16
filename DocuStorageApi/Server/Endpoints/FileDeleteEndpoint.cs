using DocuStorageApi.Shared;
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

    private async static Task<IResult> HandleAsync(
        [FromServices] FileDeleteEndpoint instance,
        [FromRoute] int id)
    {
        await instance._api.DeleteFile(id);

        return Results.Ok();
    }

    //private Task<Stream> GetFile(int id)
    //    => this._api.GetFile(id);
}
