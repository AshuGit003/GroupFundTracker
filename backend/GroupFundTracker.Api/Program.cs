using GroupFundTracker.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<GroupFundDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngular",
//        policy => policy
//        .WithOrigins("http://localhost:4200")
//        .AllowAnyHeader()
//        .AllowAnyMethod());
//});

var app = builder.Build();

app.UseHttpsRedirection();

// Serve index.html automatically at "/"
app.UseDefaultFiles(new DefaultFilesOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "browser")),
    RequestPath = ""
});


// Serve Angular from wwwroot/browser
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "browser")),
    RequestPath = ""
});

//app.UseCors("AllowAngular");

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();
