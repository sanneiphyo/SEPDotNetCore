using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEPDotNetCore.SankeLadder.Domain.Features;
using SEPDotNetCore.SnakeLadder.Database.AppDbContextModels;
using SEPDotNetCore.SnakeLadder.RestApi.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
},ServiceLifetime.Transient,ServiceLifetime.Transient);

builder.Services.AddScoped<SnakeLadderGameService>();

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
