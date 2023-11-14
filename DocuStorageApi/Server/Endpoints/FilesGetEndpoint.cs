using DocuStorageApi.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DocuStorageApi.Server.Endpoints;

internal record FilesGetEndpoint
(
    FileAdapter _api
)
{
    public static void Map(WebApplication app)
        => app
        .MapGet("/api/v1/files", HandleAsync);

    private async static Task<IResult> HandleAsync(
        [FromServices] FilesGetEndpoint instance)
    {
        var result = await instance.GetAsync();

        return result is not null ? Results.Ok(result) : Results.NotFound();
    }

    private Task<IList<FileReference>> GetAsync()
        => this._api.ListFilesAsync();
}
