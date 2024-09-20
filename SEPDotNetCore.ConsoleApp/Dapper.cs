﻿using Dapper;
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
    internal class Dapper
    {

        private readonly string _connectionString = "Data Source =.;Initial Catalog=SEPDotNetCore;User ID =sa;Password=sasa@123";

        public void Read()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
               string query = "select * from Tbl_blog where DeleteFlag = 0";
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

        public void Create(string title, string author, string conten)
        {

            string query = @"
                    INSERT INTO [dbo].[Tbl_blog]
                    ([BlogTitle]
                    ,[BlogAuthor]
                    ,[BlogContent]
                    ,[DeleteFlag])
             VALUES
                    (@BlogTitle
                    ,@BlogAuthor
                    ,@BlogContent
                    ,@DeleteFlag
                       ,0
		               )";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query);
                Console.WriteLine(result == 1 ? "Saving Successful " : "Saving faileds");
            }
        }
    }

        
}