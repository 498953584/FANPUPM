namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;

    public class mgReport1
    {
        public static bool DeleteInfo(string ReportId)
        {
            return publicDbOpClass.NonQuerySqlString("delete from Prj_Report where ReportId = " + ReportId);
        }

        public static DataTable GetInfoList()
        {
            string sqlWhere = "1 = 1";
            return publicDbOpClass.GetPageData(sqlWhere, "Prj_Report", "ReportId desc");
        }

        public static DataTable GetInfoList(string DptName)
        {
            return publicDbOpClass.GetPageData("ReportDpt like '%" + DptName + "%'", "Prj_Report", "ReportId desc");
        }

        public static int GetMaxId()
        {
            string sqlString = "select isnull(max(ReportId),1) from Prj_Report";
            return (((int) publicDbOpClass.ExecuteScalar(sqlString)) + 1);
        }

        public static DataTable GetSingle(string ReportId)
        {
            return publicDbOpClass.DataTableQuary("select * from Prj_Report where ReportId = " + ReportId);
        }

        public static bool InsertInfo(int ReportId, string ReportDpt, DateTime? ReportTime, string Remark)
        {
            string sqlString = string.Empty;
            if (!ReportTime.HasValue)
            {
                sqlString = string.Concat(new object[] { "insert into Prj_Report (ReportId,ReportDpt,ReportTime,Remark) values (", ReportId, ",'", ReportDpt, "',null,'", Remark, "') " });
            }
            else
            {
                sqlString = string.Concat(new object[] { "insert into Prj_Report (ReportId,ReportDpt,ReportTime,Remark) values (", ReportId, ",'", ReportDpt, "','", ReportTime, "','", Remark, "') " });
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public static bool UpdateInfo(string ReportDpt, DateTime? ReportTime, string Remark, string ReportId)
        {
            string sqlString = string.Empty;
            if (!ReportTime.HasValue)
            {
                sqlString = "update Prj_Report set ReportDpt = '" + ReportDpt + "',ReportTime = null,Remark = '" + Remark + "' where ReportId = " + ReportId + " ";
            }
            else
            {
                sqlString = string.Concat(new object[] { "update Prj_Report set ReportDpt = '", ReportDpt, "',ReportTime = '", ReportTime, "',Remark = '", Remark, "' where ReportId = ", ReportId, " " });
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }
    }
}

