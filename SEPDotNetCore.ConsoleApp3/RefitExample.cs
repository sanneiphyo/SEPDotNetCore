using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace SEPDotNetCore.ConsoleApp3
{
    public class RefitExample
    {
        public async Task Run()
        {
            var blogApi = RestService.For<IBlogApi>("https://localhost:7249");
            var lst = await blogApi.GetBlogs();

            foreach (var item in lst) 
            {
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }

          
            //GetbyId
            try
            {
                var item3 = await blogApi.GetBlog(1);
            }
            catch (ApiException ex)
            {
                if(ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No Data Found"); 
                }
            }

            //post

            var item4 = await blogApi.CreateBlog(new BlogModel
            {
                BlogTitle = "test",
                BlogAuthor = "test",
                BlogContent = "test",
            });
        }
    }
}
