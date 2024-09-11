// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
//Console.ReadKey();

//md => markdown

// way to connect with database
//ADO.Net   (old school) =>.Net framework to establish between application and data source (CRUD)
//Drapper  (ORM)
//EFCore / Entity Framework 

//nget (package = same like npm)

//Ctrl +.

//max conn = 100 
//100 =99
//F9    =>  grate point
//F10     => 50  [to skip another line]
//F5

string connectionString = "Data Source =.;Initial Catalog=SEPDotNetCore;User ID =sa;Password=sasa@123";
Console.WriteLine("Connection string :" + connectionString);
SqlConnection connection = new SqlConnection(connectionString );


Console.WriteLine("Connection Opening ...");
connection.Open();
Console.WriteLine("Connection Opened.");

string query = @"SELECT [BlogId]
,[BlogTitle]
,[BlogAuthor]
,[BlogContent]
,[DeleteFlag]
FROM [dbo].[Tbl_Blog]";

SqlCommand cmd = new SqlCommand(query , connection);
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

//Dataset = a collection of data (in table)
//DataTable => dt
//DataRow => dr
//DataColums

