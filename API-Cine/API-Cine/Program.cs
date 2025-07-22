using API_Cine.Contexts;
using API_Cine.Repository;
using API_Cine.Repository.Imp;
using API_Cine.Services;
using API_Cine.Services.Imp;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CineDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<IPeliculaRepository, PeliculaRepository>();
builder.Services.AddScoped<IPeliculaSalaCineRepository, PeliculaSalaCineRepository>();
builder.Services.AddScoped<ISalaCineRepository, SalaCineRepository>();

builder.Services.AddScoped<IPeliculaService, PeliculaService>();
builder.Services.AddScoped<ISalaCineService, SalaCineService>();
builder.Services.AddScoped<IPeliculaSalaCineService, PeliculaSalaCineService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();