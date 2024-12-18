﻿// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using SEPDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;


//Console.WriteLine("Hello, World!");
//Console.ReadKey();

//md => markdown

// way to connect with database
//ADO.Net   (old school) =>.Net framework to establish between application and data source (CRUD)
//Drapper  (ORM)
//EFCore / Entity Framework 

//nget (package = same like npm)

//Ctrl +.


//Dataset = a collection of data (in table)
//DataTable => dt
//DataRow => dr
//DataColums

//max conn = 100 
//100 =99
//F9    =>  grate point
//F10     => 50  [to skip another line]
//F5

//AdoDotNetExample adoDotNet = new AdoDotNetExample();
//adoDotNet.Read();
//adoDotNet.Create();
//adoDotNet.Update();
//adoDotNet.Delete();

// DapperExample dapper = new DapperExample();
//dapper.Read();
//dapper.Create();

//EfCoreExample efCore = new EfCoreExample();
//efCore.Create();
//efCore.Read();


//var services = new ServiceCollection()
//    .AddSingleton<AdoDotNetExample>()
//    .BuildServiceProvider();

//var adoDotNetExample = services.GetRequiredService<AdoDotNetExample>();
//adoDotNetExample.Read();



var services = new ServiceCollection()
    .AddSingleton<AdoDotNetExample2>()
    .BuildServiceProvider();

var adoDotNetExample = services.GetRequiredService<AdoDotNetExample2>();
adoDotNetExample.Read();


Console.ReadKey();

