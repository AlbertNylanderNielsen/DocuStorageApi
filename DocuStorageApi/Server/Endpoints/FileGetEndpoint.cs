using DocuStorageApi.Server.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace DocuStorageApi.Server.Endpoints;

internal record FileGetEndpoint
(
    FileAdapter FileAdapter
)
{
    public static void Map(WebApplication app)
        => app
        .MapGet("/api/v1/files/{id:int}", HandleAsync);


    private async static Task<IResult> HandleAsync(
        [FromServices] FileGetEndpoint instance,
        [FromRoute] int id)
    {
        var stream = await instance.GetAsync(id);

        return
            stream is not null
            ? Results.File(stream)
            : Results.NotFound();
    }

    private Task<Stream> GetAsync(int id)
        => this.FileAdapter.GetFileStreamAsync(id);
}

