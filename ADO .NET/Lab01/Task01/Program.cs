using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Task01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var cs = config.GetConnectionString("DefaultConnection");

            using var con = new SqlConnection(cs);

            var cmd = new SqlCommand("SELECT * FROM Categories", con);

            if (con.State != ConnectionState.Open)
                con?.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var category = new Category()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2)
                };
                Console.WriteLine(category);
            }
            reader.Close();


            SqlTransaction? transaction = null;
            try
            {
                transaction = con?.BeginTransaction();
                cmd.Transaction = transaction;

                cmd.CommandText = "INSERT INTO Categories (Name, Description) VALUES (@name, @description)";
                cmd.Parameters.AddWithValue("@name", "New Category");
                cmd.Parameters.AddWithValue("@description", "Description of new category");
                int affectedRows = cmd.ExecuteNonQuery();
                Console.WriteLine($"INSERT Affected Rows : {affectedRows}");

                cmd.CommandText = "UPDATE Categories SET Name = @name WHERE Id = @id";
                cmd.Parameters.AddWithValue("@name", "Beverages");
                cmd.Parameters.AddWithValue("@id", 1);
                affectedRows = cmd.ExecuteNonQuery();
                Console.WriteLine($"UPDATE Affected Rows : {affectedRows}");

                cmd.CommandText = "DELETE FROM Categories WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", 1);
                affectedRows = cmd.ExecuteNonQuery();
                Console.WriteLine($"DELETE Affected Rows : {affectedRows}");

                transaction?.Commit();
                Console.WriteLine("Transaction committed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                if (transaction is not null)
                {
                    transaction.Rollback();
                    Console.WriteLine("Transaction rolled back!");
                }
            }

            cmd.CommandText = "GetCategoryWithId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", 2);
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                var category = new Category()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2)
                };
                Console.WriteLine(category);
            }
        }
    }
}
