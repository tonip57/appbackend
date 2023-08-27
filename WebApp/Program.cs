using Microsoft.Extensions.FileProviders;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IElectricityPriceService, ElectricityPriceService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

if (Directory.Exists(Path.Combine(builder.Environment.ContentRootPath, "./build")))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "./build")),
        RequestPath = ""
    });
}

app.MapControllers();

app.Run();