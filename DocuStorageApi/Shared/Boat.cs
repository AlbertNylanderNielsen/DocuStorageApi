using System.ComponentModel.DataAnnotations;

namespace DocuStorageApi.Shared;

public class Boat
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; } 

    public virtual ICollection<FileReference> Files { get; set; } = new List<FileReference>();
}
