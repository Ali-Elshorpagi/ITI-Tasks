using Lab03.Models;
using Lab03.Repositorires;

namespace Lab03.UoW
{
    public class UnitOFWork
    {
        ITIContext db;
        DepartmentRepository deptReps;
        StudentRepository studReps;
        public UnitOFWork(ITIContext db)
        {
            this.db = db;
        }
        public StudentRepository StudReps
        {
            get
            {
                if (studReps is null)
                    studReps = new StudentRepository(db);
                return studReps;
            }
        }
        public DepartmentRepository DeptReps
        {
            get
            {
                if (deptReps is null)
                    deptReps = new DepartmentRepository(db);
                return deptReps;
            }
        }
        public void save() => db.SaveChanges();
    }
}
