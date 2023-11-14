using DocuStorageApi.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DocuStorageApi.Server.Endpoints;

internal record FileGetEndpoint
(
    FileAdapter _api
)
{
    public static void Map(WebApplication app)
        => app
        .MapGet("/api/v1/files{id}", HandleAsync);

    private async static Task<IResult> HandleAsync(
        [FromServices] FilesGetEndpoint instance,
        [FromRoute] int id)
    {
        var stream = instance._api.GetFile(id);

        return stream is not null ? Results.Ok(stream) : Results.NotFound();
    }

    //private Task<Stream> GetFile(int id)
    //    => this._api.GetFile(id);
}
