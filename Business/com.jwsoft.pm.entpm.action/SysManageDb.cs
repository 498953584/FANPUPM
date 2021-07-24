namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class SysManageDb
    {
        public int GetDefault()
        {
            string sqlString = "";
            sqlString = "select i_SysID from pt_sysInfo_oa where i_IsDefault=1";
            return Convert.ToInt32(publicDbOpClass.ExecuteScalar(sqlString));
        }

        public DataTable QueryAllSys()
        {
            string sqlString = "";
            sqlString = "select * from pt_SysInfo_oa order by i_SysLevel,i_SysOrder asc";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable QuerySys(int iSysID)
        {
            return publicDbOpClass.DataTableQuary("select * from pt_sysInfo_oa where i_SysID = " + iSysID.ToString());
        }
    }
}

