﻿// See https://aka.ms/new-console-template for more information
using SEPDotNetCore.ConsoleApp3;

Console.WriteLine("Hello, World!");

HttpClientExample  httpClientExample = new HttpClientExample();
//await httpClientExample.Read();
//await httpClientExample.Edit(1);


await httpClientExample.Create("test title", "test body", 1);