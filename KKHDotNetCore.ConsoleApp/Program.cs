// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");
Console.ReadKey();

string connectionString = "Data Source=.;Initial Catalog=KKHDotNetCore;User ID=sa;Password=sa";
Console.WriteLine("Connection String: " + connectionString);
SqlConnection sqlConnection = new SqlConnection(connectionString);
Console.WriteLine("Connection opening...");
sqlConnection.Open();
Console.WriteLine("Connection open...");

string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_Blog]";
SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
DataTable dt = new DataTable();
sqlDataAdapter.Fill(dt);
Console.WriteLine("Connection closing...");
sqlConnection.Close();
Console.WriteLine("Connection close...");

foreach(DataRow dr in  dt.Rows)
{
    Console.WriteLine(dr["BlogAuthor"]);
}

Console.ReadKey(true);