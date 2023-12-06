using DocuStorageApi.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Reflection.Emit;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DocuStorageApi.Server.Data;

public class FocusDbContext : DbContext
{
    private string _path = @"D:\InFocusTest\FileStorageDB\fileshare.db";

    public FocusDbContext(DbContextOptions<FocusDbContext> options) : base(options)
    {
        this.Database.EnsureCreated();
    }

 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (File.Exists(_path))
        {
            var records = JsonSerializer.Deserialize<IEnumerable<Boat>>(File.ReadAllText(_path))!;
            //this.Boats.AddAsync(records);
            foreach (var boat in records)
            {
                boat.Files = new List<FileReference>();
                foreach (var file in boat.Files)
                {
                    file.BoatId = boat.Id;
                    modelBuilder.Entity<FileReference>().HasData(file);
                }
                modelBuilder.Entity<Boat>().HasData(boat);
            }
        }
        else
        {
            modelBuilder.Entity<Boat>().HasData(new Boat { Id = 1, Name = "Boat 1" });
        }
    }

    public override int SaveChanges()
    {
        return base.SaveChanges();
        File.WriteAllText(_path, JsonSerializer.Serialize(this.Boats));
    }

    public DbSet<Boat> Boats { get; set; }
    public DbSet<FileReference> Files { get; set; }
}