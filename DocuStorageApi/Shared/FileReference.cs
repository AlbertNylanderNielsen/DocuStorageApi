using System.ComponentModel.DataAnnotations;

namespace DocuStorageApi.Shared;

public class FileReference
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Path { get; set; }

    public long SizeBytes { get; set; }

    public int BoatId { get; set; }

    public virtual Boat Boat { get; set; }
}
