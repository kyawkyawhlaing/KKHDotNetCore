// See https://aka.ms/new-console-template for more information
using KKHDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();

DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create("New Title", "New Author", "New Content");
//dapperExample.Edit(1005);
//dapperExample.Delete(1005);

EFcoreExample eFcoreExample = new EFcoreExample();
//eFcoreExample.Read();
eFcoreExample.Create("EFCore Title", "EFCore Author", "EFCore Content");


Console.ReadKey();