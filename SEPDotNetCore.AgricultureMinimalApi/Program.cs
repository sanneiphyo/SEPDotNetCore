using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.MapGet("/agriculture", () =>
{
    string folderPath = "Data/Burmese.Agriculture";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<AgricultureModel>(jsonStr)!;
    return Results.Ok(result.Tbl_Property);
})
.WithName("GetAgricultures")
.WithOpenApi();


app.MapGet("/agriculture{id}", (string id) =>
{
    string folderPath = "Data/Burmese.Agriculture";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<AgricultureModel>(jsonStr)!;

    var item = result.Tbl_Property.FirstOrDefault(x => x.Id == id);

    if (item is null) return Results.BadRequest("No data found");


    return Results.Ok(item);
})
.WithName("GetAgriculture")
.WithOpenApi();



app.Run();



public class AgricultureModel
{
    public Tbl_Property[] Tbl_Property { get; set; }
}

public class Tbl_Property
{
    public string Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
}



