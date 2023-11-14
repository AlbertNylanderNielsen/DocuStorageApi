using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuStorageApi.Shared;

public class FileReference
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public long SizeBytes { get; set; }
}
