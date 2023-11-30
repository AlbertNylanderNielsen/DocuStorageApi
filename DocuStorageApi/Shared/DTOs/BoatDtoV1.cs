namespace DocuStorageApi.Shared.DTOs;

public class BoatDtoV1
{
    public int Id { get; set; }
    public string Name { get; set; } 
    public List<FileDtoV1> Files { get; set; } 
}
