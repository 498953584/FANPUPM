namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class CryReport
    {
        public DataTable GetCostYear()
        {
            string sqlString = "";
            DataTable table = new DataTable();
            sqlString = "SELECT 2006 as y union SELECT 2007 as y  union SELECT 2008 as y  union SELECT 2009 as y ";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetFinally_WorkCostList(string pc, string y, string m)
        {
            DataTable table = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pc", pc), new SqlParameter("@year", y), new SqlParameter("@month", m) };
            try
            {
                table = publicDbOpClass.ExecuteDataTable("P_CryRep_Finally_WorkCost", commandParameters);
            }
            catch
            {
            }
            return table;
        }

        public DataTable GetManUseList(string pc)
        {
            return publicDbOpClass.DataTableQuary("Select *,(isnull(sl1,0)+isnull(sl2,0)+isnull(sl3,0)) as SLsum,(isnull(JE1,0)+isnull(JE2,0)+isnull(JE3,0)) as JEsum  from CryRep_V_ManUseA where pc='" + pc + "' ");
        }

        public DataTable GetManUsepcList(string where, string UserCode)
        {
            return publicDbOpClass.DataTableQuary(("exec P_CryRep_Prjtree '" + UserCode + "'") + where);
        }

        public static DataTable GetWorkConsume(string ProjectCode, string Taskcodes)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProjectCode", ProjectCode), new SqlParameter("@TaskCodes", Taskcodes) };
            return publicDbOpClass.ExecuteDataTable("P_Report_WorkConsume", commandParameters);
        }

        public DataTable GetWorkCost_AimCompareList(string pc)
        {
            DataTable table = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pc", pc) };
            try
            {
                table = publicDbOpClass.ExecuteDataTable("P_CryRep_WorkCost_AimCompare", commandParameters);
            }
            catch
            {
            }
            return table;
        }

        public DataTable GetWorkCost_CompareList(string pc)
        {
            DataTable table = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pc", pc) };
            try
            {
                table = publicDbOpClass.ExecuteDataTable("P_CryRep_WorkCost_Compare", commandParameters);
            }
            catch
            {
            }
            return table;
        }

        public DataTable GetWorkCostList(string pc, string y, string m)
        {
            DataTable table = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@pc", pc), new SqlParameter("@year", y), new SqlParameter("@month", m) };
            try
            {
                table = publicDbOpClass.ExecuteDataTable("P_CryRep_WorkCost", commandParameters);
            }
            catch
            {
            }
            return table;
        }

        public DataTable GetWorkCostpcList()
        {
            string sqlString = "select * from (Select distinct PrjCode as pc,( select prjname from v_prj_prjtree where prjguid=a.PrjCode ) as pn  from EPM_Cost_Cbs a ) a where pn is not null";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetWorkCostpcListover()
        {
            string sqlString = "select * from (Select distinct PrjCode as pc,( select prjname from v_prj_prjtree where prjguid=a.PrjCode) as pn from EPM_Cost_Cbs a ) a where pn is not null and pc in(SELECT  prjguid from PT_PrjInfo where prjstate=7)";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetWorkCostpcListoverbuid()
        {
            string sqlString = "select * from (Select distinct PrjCode as pc,( select prjname from v_prj_prjtree where prjguid=a.PrjCode) as pn from EPM_Cost_Cbs a ) a where pn is not null and pc in(SELECT  prjguid from PT_PrjInfo where (prjstate=4 or prjstate=-1) and isvalid=1 )";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetWorkUseList(string pc)
        {
            DataTable table = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProjectCode", pc) };
            try
            {
                table = publicDbOpClass.ExecuteDataTable("P_CryRep_WorkUse", commandParameters);
            }
            catch
            {
            }
            return table;
        }

        public DataTable GetWorkUsepcList(string where)
        {
            string str = "Select distinct pc,( select prjname from v_prj_prjtree where prjguid=a.pc) as pn  from CryRep_V_WorkUse  a";
            return publicDbOpClass.DataTableQuary(str + where);
        }

        public DataTable GetWorkUsepcList(string where, string UserCode)
        {
            return publicDbOpClass.DataTableQuary(("exec P_CryRep_Prjtree '" + UserCode + "'") + where);
        }
    }
}

