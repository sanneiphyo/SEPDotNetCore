using Microsoft.AspNetCore.Mvc;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(n => new HttpClient()
{
    BaseAddress = new Uri(builder.Configuration.GetSection("ApiDomainUrl").Value!)
});

builder.Services.AddSingleton(n => new RestClient(builder.Configuration.GetSection("ApiDomainUrl").Value!));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

app.MapGet("/birds", async ([FromServices] HttpClient httpClient) =>{
   var response = await  httpClient.GetAsync("birds");
    return response.Content.ReadAsStringAsync();
});


app.MapGet("/pick-a-pile", async ([FromServices] RestClient restClient) => {

    RestRequest request = new RestRequest("pick-a-pile", Method.Get);
    var response = await restClient.GetAsync(request);
    return response.Content;
});

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
