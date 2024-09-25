
// See https://aka.ms/new-console-template for more information
using SEPDotNetCore.Database.Models;

Console.WriteLine("Hello, World!");

AppDbContext db = new AppDbContext();
var lst = db.TblBlogs.ToList();
