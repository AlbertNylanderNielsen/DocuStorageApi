using DocuStorageApi.Shared;
using Mapster;

namespace DocuStorageApi.Server.Endpoints;

internal static class FileEntityMaps
{
    // convert a previously loaded entity to a DTO
    public static FileDtoV1 ToFileDtoV1(
        this FileReference entity)
    {
        var dto = entity.Adapt<FileDtoV1>();

        return dto;
    }

}
