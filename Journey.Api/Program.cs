using Journey.Application.UseCases.Trips.Get;
using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.Register;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<JourneyContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<RegisterTripUseCase>();
builder.Services.AddScoped<GetAllTripUseCase>();
builder.Services.AddScoped<GetTripByIdUseCase>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
