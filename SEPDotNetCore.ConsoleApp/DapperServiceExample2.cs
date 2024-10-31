using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEPDotNetCore.Shared;
using System.Threading.Tasks;
using SEPDotNetCore.ConsoleApp.Models;

namespace SEPDotNetCore.ConsoleApp
{
    internal class DapperServiceExample2
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
        private readonly DapperService _dapperService;


        public DapperServiceExample2()
        {
            _dapperService = new DapperService(_connectionString);
        }

        public void Read()
        {
            string query = "select * from tbl_blog where DeleteFlag = 0;";
            var lst = _dapperService.Query<BlogDapperDataModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Create(string title, string author, string content)
        {

            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,0)";


            int result = _dapperService.Execute(query, new BlogDapperDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });
            Console.WriteLine(result == 1 ? "Saving Successful " : "Saving failed");
        }

        public void Update(string title, string author, string content)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
             SET [BlogTitle] = @BlogTitle
              ,[BlogAuthor] = @BlogAuthor
             ,[BlogContent] = @BlogContent
             ,[DeleteFlag] = 0
            WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, new BlogDapperDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });
            Console.WriteLine(result == 1 ? "Updating Successful" : "Updating Failed");
        }

        public void Edit(int Id)
        {
            string query = "select * from tbl_blog where DeleteFlag = 0 and BlogId = @BlogId;";
            var item = _dapperService.QueryFirstOrDefault<BlogDapperDataModel>(query, new BlogDapperDataModel
            {
                BlogId = Id
            });

            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }

        public void Delete(int Id)
        {
            string query = @"UPDATE Tbl_Blog 
                         SET DeleteFlag = 1 
                         WHERE BlogId = @BlogId";

            int result = _dapperService.Execute(query, new BlogDapperDataModel
            {
                BlogId = Id

            });
            Console.WriteLine(result == 0 ? "Deleting Blog Failed! " : "Successfully Deleted Blog");
        }

    }
}
