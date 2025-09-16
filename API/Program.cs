using API.Data;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection"));
});

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins(
        "http://localhost:4200",
        "https://localhost:4200"
    ));
builder.Services.AddScoped<ITokenService, TokenService>();

app.MapControllers();

app.Run();
