using DAL;
using System.Data;

namespace BLL
{
    public static class DepartmentManager
    {
        static DBManager db = new DBManager();

        public static bool AddNewDept(Department dept)
        {
            string cmd = "insert into department (Name, Capacity) values (@name,@capacity)";

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("name", dept.Name);
            dic.Add("capacity", dept.Capacity);

            int x = db.ExecuteNonQuery(cmd, dic);
            return x > 0;
        }

        public static bool UpdateDept(Department dept)
        {
            string cmd = "update department set Name=@name, Capacity=@capacity where Id=@id";

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("name", dept.Name);
            dic.Add("capacity", dept.Capacity);
            dic.Add("id", dept.Id);

            int x = db.ExecuteNonQuery(cmd, dic);
            return x > 0;
        }

        public static bool DeleteDept(int id)
        {
            string cmd = "delete from department where Id=@id";

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("id", id);

            int x = db.ExecuteNonQuery(cmd, dic);
            return x > 0;
        }

        public static Department? GetDeptById(int id)
        {
            string cmd = "select * from department where Id=@id";

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("id", id);

            DataTable dt = db.GetTable(cmd, dic);

            if (dt.Rows.Count == 0)
                return null;

            return ConvertFromDataRowToDepartment(dt.Rows[0]);
        }

        public static DepartmentList GetAllDepartments()
        {
            DataTable dt = db.GetTable("select * from department", null);
            return ConvertFromDataTableToDeptList(dt);
        }

        static DepartmentList ConvertFromDataTableToDeptList(DataTable dt)
        {
            DepartmentList result = new DepartmentList();
            foreach (DataRow dr in dt.Rows)
                result.Add(ConvertFromDataRowToDepartment(dr));

            return result;
        }

        static Department ConvertFromDataRowToDepartment(DataRow dr)
        {
            return new Department
            {
                Id = dr["Id"] != DBNull.Value ? Convert.ToInt32(dr["Id"]) : 0,
                Name = dr["Name"]?.ToString() ?? "",
                Capacity = dr["Capacity"] != DBNull.Value ? Convert.ToInt32(dr["Capacity"]) : 0
            };
        }
    }
}
