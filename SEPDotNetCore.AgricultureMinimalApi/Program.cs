using Newtonsoft.Json;

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
    var result = JsonConvert.DeserializeObject<AgricultureResponseModel>(jsonStr)!;
    return Results.Ok(result.Property1);
})
.WithName("GetWeatherForecast")
.WithOpenApi();


app.Run();



public class AgricultureResponseModel
{
    public AgricultureModel[] Property1 { get; set; }
}

public class AgricultureModel
{
    public string Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
}


