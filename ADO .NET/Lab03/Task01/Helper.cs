using System.Data;

namespace Task01
{
    public static class Helper
    {
        public static List<Department> ConvertToList(DataTable dt)
        {
            List<Department> list = new List<Department>();

            foreach (DataRow dr in dt.Rows)
            {
                if (dr.RowState != DataRowState.Deleted)
                {
                    Department dept = new Department
                    {
                        Id = dr["Id"] != DBNull.Value ? Convert.ToInt32(dr["Id"]) : 0,
                        Name = dr["Name"]?.ToString() ?? string.Empty,
                        Capacity = dr["Capacity"] != DBNull.Value ? Convert.ToInt32(dr["Capacity"]) : 0
                    };
                    list.Add(dept);
                }
            }
            return list;
        }
    }
}
