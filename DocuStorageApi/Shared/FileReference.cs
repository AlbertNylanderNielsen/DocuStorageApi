﻿using System.ComponentModel.DataAnnotations;

namespace DocuStorageApi.Shared;

public class FileReference
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public long SizeBytes { get; set; }
}
