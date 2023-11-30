using DocuStorageApi.Server.Endpoints;
using DocuStorageApi.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DBContext>(options =>
    options.UseInMemoryDatabase("DocumentStorage"));
//builder.Services.AddScoped<DBContext>();
builder.Services.AddScoped<FileAdapter>();
builder.Services.AddScoped<BoatGetEndpoint>();
builder.Services.AddScoped<FileGetEndpoint>();
builder.Services.AddScoped<FileDeleteEndpoint>();
builder.Services.AddScoped<FilePostEndpoint>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI();
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
BoatGetEndpoint.Map(app);
FileGetEndpoint.Map(app);
FileDeleteEndpoint.Map(app);
FilePostEndpoint.Map(app);

// add a fallback for missing api routes so that the
// default blazor startup page is not returned

app.MapFallback("/api/{**slug}", (ILogger logger) =>
{
    Console.WriteLine("HTTP {RequestPath} API route not found");
    return Results.NotFound();
});

//app.MapGet("/api/ping", () => Results.Ok("Pong"));
app.MapFallbackToFile("index.html");

app.Run();
