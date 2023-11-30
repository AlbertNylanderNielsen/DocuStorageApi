using DocuStorageApi.Shared.DTOs;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace DocuStorageApi.Client.Pages;

public partial class DocumentStorage
{
    [Inject]
    public HttpClient Http { get; set; } = null!;

    [Inject]
    public IJSRuntime JS { get; set; } = null;

    private BoatDtoV1? dto { get; set; }

    protected override async Task OnInitializedAsync()
    {
        dto = await GetAllFiles();
    }

    private async Task<BoatDtoV1?> GetAllFiles()
    {
        try
        {
            return await Http.GetFromJsonAsync<BoatDtoV1?>("/api/v1/boats/1");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }

    private async Task<BoatDtoV1?> DeleteFile(int id)
    {

        try
        {
            using var response = await Http.DeleteAsync($"/api/v1/files/{id}");
            dto = await response.Content.ReadFromJsonAsync<BoatDtoV1?>();
            return dto;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }

    }

    private async Task DownloadFile(FileDtoV1 file)
    {
        var fileName = file.Name;
        var fileURL = $"/api/v1/files/{file.Id}";
        await JS.InvokeVoidAsync("triggerFileDownload", fileName, fileURL);
    }

    const long KiloByte = 1024;
    const long MegaByte = KiloByte * KiloByte;
    const long MaxAllowedSize = 10 * MegaByte;

    private async Task<BoatDtoV1?> UploadFiles(IBrowserFile file)
    {

        using var content = new MultipartFormDataContent();
        using var fileStream = file.OpenReadStream(MaxAllowedSize);
        using var streamContent = new StreamContent(fileStream);
        content.Add(streamContent, "file", file.Name);
        try
        {
            using var response = await Http.PostAsync("/api/v1/files", content);
            dto = await response.Content.ReadFromJsonAsync<BoatDtoV1>();
            return dto;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }

    }
}
