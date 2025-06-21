using FindOutOurHouse.API.AppServices;
using FindOutOurHouse.DAL;
using FindOutOurHouse.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<PlaceService>();
builder.Services.AddTransient<IPlaceRepository, PlaceRepository>();
builder.Services.AddDbContext<EfDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();