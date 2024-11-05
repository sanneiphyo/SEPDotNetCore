
// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using SEPDotNetCore.Database.Models;

//Console.WriteLine("Hello, World!");

//AppDbContext db = new AppDbContext();
//var lst = db.TblBlogs.ToList();

//package => Newtonsoft.Json

var blog = new BlogDataModel
{
    Id = 1,
    Title = "Test Title",
    Author = "Test Author",
    Content = "Test Content"
};

string jsonStr = blog.ToString();

Console.WriteLine(jsonStr);
Console.ReadLine();

string jsonStr2 = """{"id":1,"title":"Test Title","author":"Test Author","content":"Test Content"}""";
var blog2 = JsonConvert.DeserializeObject<BlogDataModel>(jsonStr2);


//System.Text.Json.JsonSerializer.Serialize(blog);
//System.Text.Json.JsonSerializer.Deserialize<BlogDataModel>(jsonStr2);


public class BlogDataModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }

}

public  static class Extensions //dev code
{

    public static string ToJson(this object obj)
    {
        string jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
        return jsonStr;

    }

};
