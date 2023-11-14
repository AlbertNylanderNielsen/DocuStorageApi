using DocuStorageApi.Server.Endpoints;
using DocuStorageApi.Server.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<FilesDBContext>();
builder.Services.AddScoped<FileAdapter>();
builder.Services.AddScoped<FilesGetEndpoint>();
builder.Services.AddScoped<FileGetEndpoint>();
builder.Services.AddScoped<FilePostEndpoint>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
FilesGetEndpoint.Map(app);
FileGetEndpoint.Map(app);
FilePostEndpoint.Map(app);
//app.MapGet("/api/ping", () => Results.Ok("Pong"));

app.Run();
