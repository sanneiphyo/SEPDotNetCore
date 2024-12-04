using SEPDotNetCore.Database.Models;

namespace SEPDotNetCore.Domain.Features.Blog
{
    public interface IBlogService
    {
        TblBlog CreateBlog(TblBlog blog);
        bool? DeleteBlog(int id);
        TblBlog GetBlog(int id);
        List<TblBlog> GetBlogs();
        TblBlog PatchBlog(int id, TblBlog blog);
        TblBlog UpdateBlog(int id, TblBlog blog);
    }
}