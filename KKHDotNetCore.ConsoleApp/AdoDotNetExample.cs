using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKHDotNetCore.ConsoleApp
{
    public class AdoDotNetExample
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=KKHDotNetCore;User ID=sa;Password=sa";
        public void Read()
        {
            Console.WriteLine("Connection String: " + _connectionString);
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
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
            SqlDataReader reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["BlogId"]);
                Console.WriteLine(reader["BlogTitle"]);
                Console.WriteLine(reader["BlogAuthor"]);
                Console.WriteLine(reader["BlogContent"]);
            }

            Console.WriteLine("Connection closing...");
            sqlConnection.Close();
            Console.WriteLine("Connection close...");

            Console.ReadKey(true);
        }

        public void Create()
        {
            string title = Console.ReadLine();
            string author = Console.ReadLine();
            string content = Console.ReadLine();
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                    ([BlogTitle]
                    , [BlogAuthor]
                    , [BlogContent]
                    , [DeleteFlag])
                VALUES
                    (@BlogTitle
                    , @BlogAuthor
                    , @BlogContent
                    , @DeleteFlag)
                ";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BlogTitle",    title);
            sqlCommand.Parameters.AddWithValue("@BlogAuthor",   author);
            sqlCommand.Parameters.AddWithValue("@BlogContent",  content);
            int result = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void Edit()
        {
            string id = Console.ReadLine();
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.Open();

            string query = @"SELECT [BlogId]
                    ,[BlogTitle]
                    ,[BlogAuthor]
                    ,[BlogContent]
                    ,[DeleteFlag]
                FROM [dbo].[Tbl_Blog] WHERE BlogId=@BlogId";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            sqlConnection.Close();

            if (dt.Rows.Count > 0)
            {
                Console.WriteLine("Data has found");
            }
            else
            {
                Console.WriteLine("Data not found");
            }
        }
    }
}
