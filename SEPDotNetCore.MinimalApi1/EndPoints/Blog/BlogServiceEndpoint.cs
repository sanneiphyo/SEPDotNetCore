using SEPDotNetCore.Domain.Features.Blog;
using SEPDotNetCore.Database.Models;
namespace SEPDotNetCore.MinimalApi.Endpoints.Blog;
public static class BlogServiceEndpoint
{
    //public static string Test(this string i)
    //{
    //    return i.ToString();
    //}

    public static void UseBlogServiceEndpoint(this IEndpointRouteBuilder app)

    {
        app.MapGet("/blogs", () =>
        {
            BlogService service = new BlogService();
            var lst = service.GetBlogs();
            return Results.Ok(lst);
        })
        .WithName("GetBlogs")
        .WithOpenApi();

        app.MapGet("/blogs/{id}", (int id) =>
        {
            BlogService service = new BlogService();
            var item = service.GetBlog(id);

            if (item is null)
            {
                return Results.BadRequest("No data found.");
            }

            return Results.Ok(item);
        })
        .WithName("GetBlog")
        .WithOpenApi();

        app.MapPost("/blogs", (TblBlog blog) =>
        {
            BlogService service = new BlogService();
            var model = service.CreateBlog(blog);
            return Results.Ok(model);
        })
        .WithName("CreateBlog")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
        {
            BlogService service = new BlogService();
            var item = service.UpdateBlog(id, blog);

            if (item is null)
            {
                return Results.BadRequest("No data found.");
            }           

            return Results.Ok(blog);
        })
        .WithName("UpdateBlog")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", (int id) =>
        {
            BlogService service = new BlogService();
            var item = service.DeleteBlog(id);

            if (item is null)
            {
                return Results.BadRequest("No data found.");
            }

          
            return Results.Ok();
        })
        .WithName("DeleteBlog")
        .WithOpenApi();
    }
}