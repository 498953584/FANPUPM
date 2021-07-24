namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class CommonInfo
    {
        public static DataTable GetAgentWorks(string UserCode)
        {
            string spName = "prj_sp_getWaitingJobs";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@yhdm", UserCode) };
            return publicDbOpClass.ExecuteDataTable(spName, commandParameters);
        }

        public static DataTable GetAWACSInfo(string UserCode)
        {
            SetOutTimeInfo();
            string str = "select Message,ManInput,TimeInput,TimeOver,PrjCode,RiskID from prj_AlertMessage where isValid=1 and TimeOver>getdate()";
            return publicDbOpClass.DataTableQuary(str + string.Format(" and ManAlertTo='{0}' order by TimeInput desc", UserCode));
        }

        private static void SetOutTimeInfo()
        {
            string sqlString = "update prj_AlertMessage set isValid=0 where datediff(dd,TimeOver,getdate())>0";
            publicDbOpClass.NonQuerySqlString(sqlString);
        }
    }
}

