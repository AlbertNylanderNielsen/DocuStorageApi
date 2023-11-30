using DocuStorageApi.Server.Data;
using DocuStorageApi.Server.Endpoints;
using DocuStorageApi.Shared;
using DocuStorageApi.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;

public class FileAdapterTests 
{

    public FileAdapter GetFileAdapter()
    {
        var options = new DbContextOptionsBuilder<DBContext>()
            .UseInMemoryDatabase(databaseName: "Test_DocumentStorage") // Use a unique name for each test
            .Options;

        var _context = new DBContext(options);

        return new FileAdapter(_context);

    }

    [Fact]
    public async Task GetBoat_Test()
    {
        // Arrange
        var id = 1;
        var _fileAdapter = GetFileAdapter();

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
        var _fileAdapter = GetFileAdapter();
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
        var _fileAdapter = GetFileAdapter();
        var lastId = _fileAdapter._context.Files.Last().Id;

        // Act
        BoatDtoV1? dto = await _fileAdapter.DeleteFile(lastId);

        // Assert
        Assert.NotNull(dto);
        Assert.Equal(lastId - 1, _fileAdapter._context.Files.Count());
    }
}
