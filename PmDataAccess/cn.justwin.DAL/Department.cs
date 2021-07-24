namespace cn.justwin.DAL
{
    using cn.justwin.Model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;

    public class Department
    {
        private void AddChildren(DepartmentInfo dep, DataTable depTab)
        {
            DataRow[] source = depTab.Select(string.Format("i_sjdm = '{0}'", dep.ID));
            if (source.Count<DataRow>() != 0)
            {
                foreach (DataRow row in source)
                {
                    DepartmentInfo info = new DepartmentInfo {
                        ID = DBHelper.GetInt(row["i_bmdm"]),
                        Name = DBHelper.GetString(row["V_BMMC"])
                    };
                    this.AddChildren(info, depTab);
                    dep.Children.Add(info);
                }
            }
        }

        public DepartmentInfo GetDepartment()
        {
            DepartmentInfo dep = null;
            string cmdText = "SELECT * FROM PT_d_bm WHERE c_sfyx = 'y' ";
            DataTable depTab = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]);
            DataRow[] source = depTab.Select("i_sjdm = 0");
            if (source.Count<DataRow>() > 0)
            {
                dep = new DepartmentInfo {
                    ID = DBHelper.GetInt(source[0]["i_bmdm"]),
                    Name = DBHelper.GetString(source[0]["V_BMMC"])
                };
            }
            this.AddChildren(dep, depTab);
            return dep;
        }

        public string GetDepFullName(int depCode)
        {
            string cmdText = string.Format("SELECT dbo.ufnRootDepName('{0}');", depCode);
            return DBHelper.GetString(SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[0]));
        }
    }
}

