using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SEPDotNetCore.ConsoleApp
{

    public class AdoDotNetExample

    {
        private readonly string _connectionString = "Data Source =.;Initial Catalog=SEPDotNetCore;User ID =sa;Password=sasa@123 , TrustServerCertificate=True;";

            public void Read()
        {


            Console.WriteLine("Connection string :" + _connectionString);
            SqlConnection connection = new SqlConnection(_connectionString);


            Console.WriteLine("Connection Opening ...");
            connection.Open();
            Console.WriteLine("Connection Opened.");

            string query = @"SELECT [BlogId]
                            ,[BlogTitle]
                            ,[BlogAuthor]
                            ,[BlogContent]
                            ,[DeleteFlag]
                            FROM [dbo].[Tbl_Blog]";

            SqlCommand cmd = new SqlCommand(query, connection);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd); //generator to run cmd
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
                Console.WriteLine(reader["DeleteFlag"]);
            };

            Console.WriteLine("Connection closing ...");
            connection.Close();
            Console.WriteLine("Connection closed...");


        }

        public void Create()
        {

            Console.WriteLine(" Blog Title : ");
            string title = Console.ReadLine();


            Console.WriteLine(" Blog Author : ");
            string author = Console.ReadLine();

            Console.WriteLine(" Blog Content : ");
            string content = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            //connection2.Open();
            //string queryinsert = $@"
            //INSERT INTO [dbo].[Tbl_blog]
            //           ([BlogTitle]
            //           ,[BlogAuthor]
            //           ,[BlogContent]
            //           ,[DeleteFlag])
            //     VALUES
            //           ('{title}'
            //           ,'{author}'
            //           ,{content}
            //           ,0
            //		   )";


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

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);

            int result = cmd.ExecuteNonQuery();
            connection.Close();

            //if (result == 1)
            //{

            //    Console.WriteLine("Saving Successful ...");
            //}
            //else

            //{
            //    Console.WriteLine("Saving failed ...");                                                
            //}

            Console.WriteLine(result == 1 ? "Saving Successful " : "Saving faileds");

        }


        public void Edit()
        {

            Console.WriteLine(" Blog Id: ");
            string id = Console.ReadLine();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            string query = @"SELECT [BlogId]
                            ,[BlogTitle]
                            ,[BlogAuthor]
                            ,[BlogContent]
                            ,[DeleteFlag]
                            FROM [dbo].[Tbl_Blog] where BlogId = @BlogId";


            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No data found");
            }

            DataRow dr = dt.Rows[0];
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
                Console.WriteLine(dr["DeleteFlag"]);
            };

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


            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $@"UPDATE [dbo].[Tbl_Blog]
             SET [BlogTitle] = @BlogTitle
              ,[BlogAuthor] = @BlogAuthor
             ,[BlogContent] = @BlogContent
             ,[DeleteFlag] = 0
            WHERE BlogId = @BlogId";


            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();

            connection.Close();

            Console.WriteLine(result == 1 ? "Updating Successful" :"Updating Failed") ;
        }

        public void Delete()
        {
            
            Console.WriteLine("Blog Id: ");
            string id = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();


            string query = @"UPDATE [dbo].[Tbl_Blog]
               SET [DeleteFlag] = 1
             WHERE BlogId = @id";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine(result == 0 ? "Deleteing Blog Failed! " : "Successfully Deleted Blog");
        }

    }



}
