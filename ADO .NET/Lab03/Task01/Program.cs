using Microsoft.Data.SqlClient;
using System.Data;

namespace Task01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=University;Integrated Security=True;Trust Server Certificate=True";
                using SqlConnection con = new SqlConnection(cs);

                SqlCommand cmd = new SqlCommand("select * from Department", con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.DeleteCommand = builder.GetDeleteCommand();

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                Console.WriteLine($"Rows loaded from database: {dt.Rows.Count}");
                Console.WriteLine("========== Select Departments ==========");
                List<Department> Depts = Helper.ConvertToList(dt);
                foreach (var dept in Depts)
                    Console.WriteLine(dept);

                Console.WriteLine("========== Update Departments ==========");

                if (dt.Rows.Count > 0)
                    dt.Rows[0]["Name"] = "PD";

                Console.WriteLine("========== Delete Departments ==========");
                if (dt.Rows.Count > 1)
                    dt.Rows[1].Delete();

                Console.WriteLine("========== Insert Departments ==========");

                DataRow newRow = dt.NewRow();
                newRow["Name"] = "New Department";
                newRow["Capacity"] = 500;
                dt.Rows.Add(newRow);


                Console.WriteLine("\n========== Row States ==========");
                foreach (DataRow row in dt.Rows)
                    Console.WriteLine($"RowState: {row.RowState}");

                adapter.Update(dt);


                Console.WriteLine("========== Select Departments ==========");
                DataTable checkTable = new DataTable();
                adapter.Fill(checkTable);
                var checkList = Helper.ConvertToList(checkTable);
                foreach (var dept in checkList)
                    Console.WriteLine(dept);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
