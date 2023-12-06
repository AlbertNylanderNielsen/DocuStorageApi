using DocuStorageApi.Server.Data;
using DocuStorageApi.Server.Endpoints;
using DocuStorageApi.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

public class FileAdapterTests 
{

 

    public static FocusDbContext GetDbContext() 
        => new(new DbContextOptionsBuilder<FocusDbContext>()
                .UseInMemoryDatabase(databaseName: "Test_DocumentStorage")
                .Options);

    [Fact]
    public async Task GetBoat_Test()
    {
        // Arrange
        var id = 1;
        var _context = GetDbContext();
        var _fileAdapter = new FileAdapter(_context);

        // Act
        BoatDtoV1? dto = await _fileAdapter.GetAsync(id);

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(id, dto.Id);
        Assert.Empty(dto.Files);
    }

    [Fact]
    public async Task PostFile_Test()
    {
        // Arrange
        var _context = GetDbContext();
        var _fileAdapter = new FileAdapter(_context);
        var initialRecordCount = _fileAdapter._context.Files.Count();
        IFormFile file = new FormFile(new MemoryStream(), 0, 924, "Data", "test.txt");

        // Act
        BoatDtoV1? dto = await _fileAdapter.PostFile(file);

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(initialRecordCount + 1, _fileAdapter._context.Files.Count());
    }
     

    [Fact]
    public async Task DeleteFile_Test()
    {
        // Arrange
        var _context = GetDbContext();
        var _fileAdapter = new FileAdapter(_context);
        var lastId = _fileAdapter._context.Files.Last().Id;

        // Act
        BoatDtoV1? dto = await _fileAdapter.DeleteFile(lastId);

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(lastId - 1, _fileAdapter._context.Files.Count());
    }

    //[Fact]
    //public async Task DeleteFile_Test_one()
    //{
    //    // Arrange
    //    var dbContext = GetDbContext();
    //    var fileAdapter = new FileAdapter(dbContext);
    //    var lastId = dbContext.Files.Last().Id;

    //    // Act
    //    await fileAdapter.DeleteFile(lastId);

    //    // Assert
    //    var exists = await dbContext.Files.AnyAsync(x => x.Id == lastId);
    //    Assert.False(exists);
    //}

    //[Fact]
    //public async Task DeleteFile_Test_two()
    //{
    //    // Arrange
    //    var _fileAdapter = GetFileAdapter();
    //    var lastId = _fileAdapter._context.Files.Last().Id;

    //    // Act
    //    await _fileAdapter.DeleteFile(lastId);

    //    // Assert
    //    var dto = await _fileAdapter.GetAsync(1);
    //    var exists = dto.Files.Any(x => x.Id == lastId);
    //    Assert.False(exists);
    //}

}
