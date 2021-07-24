namespace cn.justwin.BLL
{
    using cn.justwin.DAL;
    using cn.justwin.Model;
    using System;

    public class Department
    {
        public DepartmentInfo GetDepartment()
        {
            cn.justwin.DAL.Department department = new cn.justwin.DAL.Department();
            return department.GetDepartment();
        }

        public string GetDepFullName(int depCode)
        {
            cn.justwin.DAL.Department department = new cn.justwin.DAL.Department();
            return department.GetDepFullName(depCode);
        }
    }
}

