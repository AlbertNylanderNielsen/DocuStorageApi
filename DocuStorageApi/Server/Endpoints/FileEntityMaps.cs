using DocuStorageApi.Shared;
using DocuStorageApi.Shared.DTOs;
using Mapster;

namespace DocuStorageApi.Server.Endpoints;

internal static class FileEntityMaps
{
    //convert a previously loaded entity to a DTO
    public static BoatDtoV1? ToBoatDtoV1(this IQueryable<Boat> entityQuery)
        => entityQuery
        .ProjectToType<BoatDtoV1>()
        .SingleOrDefault();
}
