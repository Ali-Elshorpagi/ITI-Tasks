using Microsoft.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class DBManager
    {
        private readonly string cs = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=University;Integrated Security=True;Trust Server Certificate=True";

        public DataTable GetTable(string queryText, Dictionary<string, object>? param = null)
        {
            using SqlConnection con = new SqlConnection(cs);
            using SqlCommand cmd = new SqlCommand(queryText, con);

            if (param is not null)
            {
                foreach (var item in param)
                    cmd.Parameters.AddWithValue("@" + item.Key, item.Value);
            }

            using SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public int ExecuteNonQuery(string queryText, Dictionary<string, object> param)
        {
            using SqlConnection con = new SqlConnection(cs);
            using SqlCommand cmd = new SqlCommand(queryText, con);

            foreach (var item in param)
                cmd.Parameters.AddWithValue("@" + item.Key, item.Value);

            con.Open();
            return cmd.ExecuteNonQuery();
        }
    }
}
