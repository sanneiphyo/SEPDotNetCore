using Dapper;
using SEPDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEPDotNetCore.ConsoleApp
{
    internal class DapperExample
    {

        private readonly string _connectionString = "Data Source =.;Initial Catalog=SEPDotNetCore;User ID =sa;Password=sasa@123";

        public void Read()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {


                //using(IDbConnection db = new SqlConnection(_connectionString))
                //{
                //    string query = "select * from tbl_blog where DeleteFlag = 0;";
                //    var lst = db.Query(query).ToList();
                //    foreach (var item in lst)
                //    {
                //        Console.WriteLine(item.BlogId);
                //        Console.WriteLine(item.BlogTitle);
                //        Console.WriteLine(item.BlogAuthor);
                //        Console.WriteLine(item.BlogContent);
                //    }
                //}

                string query = "select * from tbl_blog where DeleteFlag = 0;";
                var lst = db.Query<BlogDataModel>(query).ToList(); //ToList is like execute data from database

                foreach (var item in lst)
                {
                    Console.WriteLine(item.BlogId);
                    Console.WriteLine(item.BlogTitle);
                    Console.WriteLine(item.BlogAuthor);
                    Console.WriteLine(item.BlogContent);
                }
            }
            //DTO =>data transfer object

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

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                { 
                    BlogTitle = title, BlogAuthor = author, BlogContent = content 
                });
                Console.WriteLine(result == 1 ? "Saving Successful " : "Saving faileds");
            }
        }

        public void Update(string title ,string author, string content)
        {
            string query = $@"UPDATE [dbo].[Tbl_Blog]
             SET [BlogTitle] = @BlogTitle
              ,[BlogAuthor] = @BlogAuthor
             ,[BlogContent] = @BlogContent
             ,[DeleteFlag] = 0
            WHERE BlogId = @BlogId";
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });
                Console.WriteLine(result == 1 ? "Updating Successful" : "Updating Failed");
            }
        }

        public void Delete(int Id)
        {
          using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"UPDATE Tbl_Blog 
                         SET DeleteFlag = 1 
                         WHERE BlogId = @BlogId";

                int result = db.Execute(query, new BlogDataModel
                {
                    BlogId = Id
                   
                });
                Console.WriteLine(result == 0 ? "Deleteing Blog Failed! " : "Successfully Deleted Blog");
            }

        }


        public void Edit(int Id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Tbl_blog WHERE DeleteFlag = 0 AND BlogId = @BlogId;";

             
                var item = db.Query<BlogDataModel>(query, new 
                { BlogId = Id }).FirstOrDefault(); 

                if (item is null)
                {
                    Console.WriteLine("No Data Found.");
                    return;
                }

                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }



    }


}
