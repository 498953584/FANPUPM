namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class AccountReportAction
    {
        public DataTable GetMonthDirectCostAccount(Guid ProjectCode, DateTime dtStartDate, DateTime dtEndDate)
        {
            DataTable table = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProjectCode", ProjectCode), new SqlParameter("@StartDate", dtStartDate), new SqlParameter("@EndDate", dtEndDate) };
            try
            {
                table = publicDbOpClass.ExecuteDataTable("P_Cry_ProjectCost_MonthDirectCostAccount", commandParameters);
            }
            catch
            {
            }
            return table;
        }

        public DataTable GetMonthDirectCostProportion(Guid ProjectCode, DateTime dtStartDate, DateTime dtEndDate)
        {
            DataTable table = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProjectCode", ProjectCode), new SqlParameter("@StartDate", dtStartDate), new SqlParameter("@EndDate", dtEndDate) };
            try
            {
                table = publicDbOpClass.ExecuteDataTable("P_Cry_ProjectCost_CostProportion", commandParameters);
            }
            catch
            {
            }
            return table;
        }

        public DataTable GetMonthDirectTargetAccount(Guid ProjectCode, DateTime dtStartDate, DateTime dtEndDate)
        {
            DataTable table = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProjectCode", ProjectCode), new SqlParameter("@StartDate", dtStartDate), new SqlParameter("@EndDate", dtEndDate) };
            try
            {
                table = publicDbOpClass.ExecuteDataTable("P_Cry_ProjectCost_DirectTargetAccount", commandParameters);
            }
            catch
            {
            }
            return table;
        }

        public DataTable GetMonthInDirectCostAccount(Guid ProjectCode, DateTime dtStartDate, DateTime dtEndDate)
        {
            DataTable table = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProjectCode", ProjectCode), new SqlParameter("@StartDate", dtStartDate), new SqlParameter("@EndDate", dtEndDate) };
            try
            {
                table = publicDbOpClass.ExecuteDataTable("P_Cry_ProjectCost_MonthInDirectCostAccount", commandParameters);
            }
            catch
            {
            }
            return table;
        }

        public DataTable GetProjectInOverAccount(string ProjectCode)
        {
            DataTable table = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjCode", ProjectCode) };
            try
            {
                table = publicDbOpClass.ExecuteDataTable("P_Cry_Pm2_OverProjectAccount", commandParameters);
            }
            catch
            {
            }
            return table;
        }

        public DataTable GetProjectInProcessAccount(string ProjectCode, DateTime dtStartDate, DateTime dtEndDate)
        {
            DataTable table = new DataTable();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjCode", ProjectCode), new SqlParameter("@StartDate", dtStartDate), new SqlParameter("@EndDate", dtEndDate) };
            try
            {
                table = publicDbOpClass.ExecuteDataTable("P_Cry_ProjectCost_ProjectInProcessAccount", commandParameters);
            }
            catch
            {
            }
            return table;
        }
    }
}

