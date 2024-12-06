
using Microsoft.Extensions.Configuration;
using SEPDotNetCore.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SEPDotNetCore.ConsoleApp
{
    internal class AdoDotNetExample2
    {
        private readonly string _connectionString ;
        private readonly AdoDotNetService _adoDotNetService;


        public AdoDotNetExample2(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection")!;
        }


        //public AdoDotNetExample2()
        //{
        //    _adoDotNetService = new AdoDotNetService(_connectionString);
        //}

        public void Read()
        {
            string query = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                                  ,[DeleteFlag]
                              FROM [dbo].[Tbl_Blog] where DeleteFlag = 0";
            var dt = _adoDotNetService.Query(query);
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }
        }

        public void Edit()
        {
            Console.Write("Blog Id: ");
            string id = Console.ReadLine()!;

            string query = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                                  ,[DeleteFlag]
                              FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";

            //SqlParameterModel[] sqlParameters = new SqlParameterModel[1];
            //sqlParameters[0] = new SqlParameterModel
            //{
            //    Name = "@BlogId",
            //    Value = id
            //};

            //var dt = _adoDotNetService.Query(query, sqlParameters);

            var dt = _adoDotNetService.Query(query,
                new SqlParameterModel("@BlogId", id));

            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);
        }

        public void Create()
        {
            Console.WriteLine("Blog Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Blog Author: ");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content: ");
            string content = Console.ReadLine();

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

            int result = _adoDotNetService.Execute(query,
                new SqlParameterModel("@BlogTitle", title),
                new SqlParameterModel("@BlogAuthor", author),
                new SqlParameterModel("@BlogContent", content));

            Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Failed.");
        }

        public void Update()
        {
            Console.WriteLine("Blog Id : ");
            string id = Console.ReadLine();

            Console.WriteLine("Blog Title : ");
            string title = Console.ReadLine();


            Console.WriteLine("Blog Author : ");
            string author = Console.ReadLine();

            Console.WriteLine("Blog Content : ");
            string content = Console.ReadLine();



            string query = $@"UPDATE [dbo].[Tbl_Blog]
             SET [BlogTitle] = @BlogTitle
              ,[BlogAuthor] = @BlogAuthor
             ,[BlogContent] = @BlogContent
             ,[DeleteFlag] = 0
            WHERE BlogId = @BlogId";

            int result = _adoDotNetService.Execute(query,
                new SqlParameterModel("@BlogId", id),
                new SqlParameterModel("@BlogTitle", title),
                new SqlParameterModel("@BlogAuthor", author),
                new SqlParameterModel("@BlogContent", content));
          
            Console.WriteLine(result == 1 ? "Updating Successful" : "Updating Failed");
        }

        public  void Delete()
        {
            Console.WriteLine("Blog Id: ");
            string id = Console.ReadLine();

            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [DeleteFlag] = 1
             WHERE BlogId = @id";

            int result = _adoDotNetService.Execute(query,
               new SqlParameterModel("@id", id));
                       
            Console.WriteLine(result == 0 ? "Deleteing Blog Failed! " : "Successfully Deleted Blog");
        }
    }
}