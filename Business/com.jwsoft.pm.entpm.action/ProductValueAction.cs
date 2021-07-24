namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;

    public class ProductValueAction
    {
        public string AccumulativeSum(Guid projectCode, string taskCode, string type)
        {
            string sqlString = "";
            if (type == "S")
            {
                object obj2 = sqlString + " select isnull(sum(isnull(MonthOverQuantity,0)),0) as AccumulativeQuantity from EPM_ProjectCost_MonthProductValueDetail " + " where ";
                sqlString = string.Concat(new object[] { obj2, " ProjectCode='", projectCode, "' and TaskCode='", taskCode, "' " });
            }
            if (type == "R")
            {
                object obj3 = sqlString + " select isnull(sum(isnull(SuperQuantity,0)),0) as AccSuperQuantity from EPM_ProjectCost_MonthProductValueDetail " + " where ";
                sqlString = string.Concat(new object[] { obj3, " ProjectCode='", projectCode, "' and TaskCode='", taskCode, "' " });
            }
            if (type == "P")
            {
                object obj4 = sqlString + " select isnull(sum(isnull(PlanOverQuantity,0)),0) as PlanAccumulativeQuantity from EPM_ProjectCost_MonthProductValueDetail " + " where ";
                sqlString = string.Concat(new object[] { obj4, " ProjectCode='", projectCode, "' and TaskCode='", taskCode, "' " });
            }
            try
            {
                return Convert.ToString(publicDbOpClass.ExecuteScalar(sqlString));
            }
            catch
            {
                return "0";
            }
        }

        public int AddProductValueMonthPlan(ArrayList arr, Guid ProjectCode, DateTime dtStartDate)
        {
            string sqlString = "";
            if (arr.Count > 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    object obj2 = sqlString + " update EPM_ProjectCost_MonthProductValueDetail set ";
                    object obj3 = string.Concat(new object[] { obj2, " SynthPrice='", ((MonthProductValueDetail) arr[i]).SynthPrice, "', " });
                    object obj4 = string.Concat(new object[] { obj3, " PlanOverQuantity='", ((MonthProductValueDetail) arr[i]).PlanOverQuantity, "', " });
                    object obj5 = string.Concat(new object[] { obj4, " PlanAccumulativeQuantity=", ((MonthProductValueDetail) arr[i]).PlanAccumulativeQuantity, "-isnull(PlanOverQuantity,0)+", ((MonthProductValueDetail) arr[i]).PlanOverQuantity, ", " });
                    object obj6 = string.Concat(new object[] { obj5, " PlanProductValue=('", ((MonthProductValueDetail) arr[i]).PlanOverQuantity * ((MonthProductValueDetail) arr[i]).SynthPrice, "'), " });
                    object obj7 = string.Concat(new object[] { obj6, " StatDate='", ((MonthProductValueDetail) arr[i]).StatDate, "' " }) + " where ";
                    object obj8 = string.Concat(new object[] { obj7, " ProjectCode='", ((MonthProductValueDetail) arr[i]).TaskList.ProjectCode, "' and " }) + " TaskCode='" + ((MonthProductValueDetail) arr[i]).TaskList.TaskCode + "' and ";
                    sqlString = string.Concat(new object[] { obj8, " datediff(m,FillDate,'", dtStartDate, "')=0 " });
                }
                object obj9 = sqlString + " update EPM_ProjectCost_MonthProductValue set IsHavePlan=1 " + " where ";
                object obj10 = string.Concat(new object[] { obj9, " ProjectCode='", ProjectCode, "' and " });
                sqlString = string.Concat(new object[] { obj10, " datediff(m,StartDate,'", dtStartDate, "')=0 " });
            }
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int AddProductValueMonthReprot(ArrayList arr, Guid ProjectCode, DateTime dtStartDate)
        {
            string sqlString = "";
            if (arr.Count > 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    object obj2 = sqlString + " update EPM_ProjectCost_MonthProductValueDetail set ";
                    object obj3 = string.Concat(new object[] { obj2, " SynthPrice='", ((MonthProductValueDetail) arr[i]).SynthPrice, "', " });
                    object obj4 = string.Concat(new object[] { obj3, " ReportQuantity='", ((MonthProductValueDetail) arr[i]).ReportQuantity, "', " });
                    object obj5 = string.Concat(new object[] { obj4, " SuperQuantity='", ((MonthProductValueDetail) arr[i]).SuperQuantity, "', " });
                    object obj6 = string.Concat(new object[] { obj5, " AccSuperQuantity='", ((MonthProductValueDetail) arr[i]).SuperQuantity, "', " });
                    object obj7 = string.Concat(new object[] { obj6, " FillDate='", ((MonthProductValueDetail) arr[i]).FillDate, "' " }) + " where ";
                    sqlString = ((((string.Concat(new object[] { obj7, " ProjectCode='", ProjectCode, "' and " }) + " TaskCode='" + ((MonthProductValueDetail) arr[i]).TaskList.TaskCode + "' and ") + " ((year(StatDate)='" + dtStartDate.Year.ToString() + "' and ") + " month(StatDate)='" + dtStartDate.Month.ToString() + "') or ") + " (year(FillDate)='" + dtStartDate.Year.ToString() + "' and ") + " month(FillDate)='" + dtStartDate.Month.ToString() + "') ) ";
                }
            }
            object obj8 = sqlString + " update EPM_ProjectCost_MonthProductValue set IsHaveReprot=1 " + " where ";
            sqlString = (string.Concat(new object[] { obj8, " ProjectCode='", ProjectCode, "' and " }) + " year(StartDate)='" + dtStartDate.Year.ToString() + "' and ") + " month(StartDate)='" + dtStartDate.Month.ToString() + "' ";
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int AddProductValueMonthStat(ArrayList arr, Guid ProjectCode, DateTime dtStartDate)
        {
            string sqlString = "";
            if (arr.Count > 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    object obj2 = sqlString + " update EPM_ProjectCost_MonthProductValueDetail set ";
                    object obj3 = string.Concat(new object[] { obj2, " MonthOverQuantity='", ((MonthProductValueDetail) arr[i]).MonthOverQuantity, "', " });
                    object obj4 = string.Concat(new object[] { obj3, " AccumulativeQuantity='", ((MonthProductValueDetail) arr[i]).MonthOverQuantity, "', " });
                    object obj5 = string.Concat(new object[] { obj4, " StatDate='", ((MonthProductValueDetail) arr[i]).StatDate, "' " }) + " where ";
                    sqlString = (((((string.Concat(new object[] { obj5, " ProjectCode='", ((MonthProductValueDetail) arr[i]).TaskList.ProjectCode, "' and " }) + " TaskCode='" + ((MonthProductValueDetail) arr[i]).TaskList.TaskCode + "' and ") + " ((year(FillDate)='" + ((MonthProductValueDetail) arr[i]).StatDate.Year.ToString() + "' and ") + " month(FillDate)='" + ((MonthProductValueDetail) arr[i]).StatDate.Month.ToString() + "') or  ") + " (year(StatDate)='" + ((MonthProductValueDetail) arr[i]).StatDate.Year.ToString() + "' and ") + " month(StatDate)='" + ((MonthProductValueDetail) arr[i]).StatDate.Month.ToString() + "') ) ") + this.GetCompleteCount(((MonthProductValueDetail) arr[i]).TaskList.ProjectCode, ((MonthProductValueDetail) arr[i]).TaskList.TaskCode);
                }
            }
            object obj6 = sqlString + " update EPM_ProjectCost_MonthProductValue set IsHaveStat=1 " + " where ";
            sqlString = (string.Concat(new object[] { obj6, " ProjectCode='", ProjectCode, "' and " }) + " year(StartDate)='" + dtStartDate.Year.ToString() + "' and ") + " month(StartDate)='" + dtStartDate.Month.ToString() + "' ";
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteProductValuePlan(Guid ProjectCode, DateTime dtStartDate)
        {
            string sqlString = "";
            object obj2 = sqlString;
            object obj3 = (string.Concat(new object[] { obj2, " if exists(select * from EPM_ProjectCost_MonthProductValue where ProjectCode='", ProjectCode, "' and year(StartDate)='", dtStartDate.Year.ToString(), "' and month(StartDate)='", dtStartDate.Month.ToString(), "' and IsHavePlan='1' )" }) + " update EPM_ProjectCost_MonthProductValueDetail set ") + " PlanOverQuantity='0' " + " where ";
            object obj4 = string.Concat(new object[] { obj3, " ProjectCode='", ProjectCode, "' and year(FillDate)='", dtStartDate.Year.ToString(), "' and month(FillDate)='", dtStartDate.Month.ToString(), "' " }) + " else ";
            object obj5 = string.Concat(new object[] { obj4, " delete from EPM_ProjectCost_MonthProductValueDetail where  ProjectCode='", ProjectCode, "' and year(FillDate)='", dtStartDate.Year.ToString(), "' and month(FillDate)='", dtStartDate.Month.ToString(), "' " }) + " update EPM_ProjectCost_MonthProductValue set ";
            sqlString = string.Concat(new object[] { obj5, " IsHavePlan='0' where ProjectCode='", ProjectCode, "' and year(StartDate)='", dtStartDate.Year.ToString(), "' and month(StartDate)='", dtStartDate.Month.ToString(), "' " });
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteProductValueReport(Guid ProjectCode, DateTime dtStartDate)
        {
            string sqlString = "";
            object obj2 = sqlString;
            object obj3 = (string.Concat(new object[] { obj2, " if exists(select * from EPM_ProjectCost_MonthProductValue where ProjectCode='", ProjectCode, "' and year(StartDate)='", dtStartDate.Year.ToString(), "' and month(StartDate)='", dtStartDate.Month.ToString(), "' and IsHaveStat='1' )" }) + " update EPM_ProjectCost_MonthProductValueDetail set ") + " ReportQuantity='0',SuperQuantity='0' " + " where ";
            object obj4 = string.Concat(new object[] { obj3, " ProjectCode='", ProjectCode, "' and year(FillDate)='", dtStartDate.Year.ToString(), "' and month(FillDate)='", dtStartDate.Month.ToString(), "' " }) + " else ";
            object obj5 = string.Concat(new object[] { obj4, " delete from EPM_ProjectCost_MonthProductValueDetail where  ProjectCode='", ProjectCode, "' and year(FillDate)='", dtStartDate.Year.ToString(), "' and month(FillDate)='", dtStartDate.Month.ToString(), "' " }) + " update EPM_ProjectCost_MonthProductValue set ";
            sqlString = string.Concat(new object[] { obj5, " IsHaveReprot='0' where ProjectCode='", ProjectCode, "' and year(StartDate)='", dtStartDate.Year.ToString(), "' and month(StartDate)='", dtStartDate.Month.ToString(), "' " });
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int DeleteProductValueStat(Guid ProjectCode, DateTime dtStartDate)
        {
            string sqlString = "";
            object obj2 = sqlString;
            object obj3 = (string.Concat(new object[] { obj2, " if exists(select * from EPM_ProjectCost_MonthProductValue where ProjectCode='", ProjectCode, "' and year(StartDate)='", dtStartDate.Year.ToString(), "' and month(StartDate)='", dtStartDate.Month.ToString(), "' and IsHaveReprot='1' )" }) + " update EPM_ProjectCost_MonthProductValueDetail set ") + " MonthOverQuantity='0' " + " where ";
            object obj4 = string.Concat(new object[] { obj3, " ProjectCode='", ProjectCode, "' and year(StatDate)='", dtStartDate.Year.ToString(), "' and month(StatDate)='", dtStartDate.Month.ToString(), "' " }) + " else ";
            object obj5 = string.Concat(new object[] { obj4, " delete from EPM_ProjectCost_MonthProductValueDetail where  ProjectCode='", ProjectCode, "' and year(StatDate)='", dtStartDate.Year.ToString(), "' and month(StatDate)='", dtStartDate.Month.ToString(), "' " }) + " update EPM_ProjectCost_MonthProductValue set ";
            sqlString = string.Concat(new object[] { obj5, " IsHaveStat='0' where ProjectCode='", ProjectCode, "' and year(StartDate)='", dtStartDate.Year.ToString(), "' and month(StartDate)='", dtStartDate.Month.ToString(), "' " });
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public static DataTable EarnValueAnalyse(string projectCode)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjCode", projectCode) };
            return publicDbOpClass.ExecuteDataTable("EPM_P_Cost_EarnValue", commandParameters);
        }

        private string GetCompleteCount(Guid guidProjectCode, string strTaskCode)
        {
            object obj2 = string.Empty;
            object obj3 = string.Concat(new object[] { obj2, " update EPM_Task_TaskList set CompleteCount=(select isnull(sum(isnull(MonthOverQuantity,0)),0) as CompleteCount from EPM_ProjectCost_MonthProductValueDetail where ProjectCode='", guidProjectCode, "' and TaskCode='", strTaskCode, "' ) " }) + " where ";
            return string.Concat(new object[] { obj3, " ProjectCode='", guidProjectCode, "' and TaskCode='", strTaskCode, "' " });
        }

        public decimal GetCurrMonthOverProductValue(Guid ProjectCode, DateTime dtStartDate)
        {
            decimal num = 0M;
            string sqlString = string.Concat(new object[] { " select isnull(sum(SynthPrice*isnull(SuperQuantity,0.00)),0.00) as PlanProductValue from EPM_ProjectCost_MonthProductValueDetail where ProjectCode='", ProjectCode, "' and datediff(m,'", dtStartDate, "',FillDate)=0 " });
            try
            {
                DataTable table = publicDbOpClass.DataTableQuary(sqlString);
                if (table.Rows.Count > 0)
                {
                    num = Convert.ToDecimal(table.Rows[0]["PlanProductValue"].ToString());
                }
            }
            catch
            {
            }
            return num;
        }

        public decimal GetCurrMonthProductValueSta(Guid ProjectCode, DateTime dtStartDate)
        {
            decimal num = 0M;
            string sqlString = string.Concat(new object[] { " select isnull(sum(SynthPrice*isnull(PlanOverQuantity,0.00)),0.00) as PlanProductValue from EPM_ProjectCost_MonthProductValueDetail where ProjectCode='", ProjectCode, "' and datediff(m,'", dtStartDate, "',FillDate)=0 " });
            try
            {
                DataTable table = publicDbOpClass.DataTableQuary(sqlString);
                if (table.Rows.Count > 0)
                {
                    num = Convert.ToDecimal(table.Rows[0]["PlanProductValue"].ToString());
                }
            }
            catch
            {
            }
            return num;
        }

        private MonthProductValueDetail GetMonthProductValueDetailFromDataRow(DataRow dr)
        {
            MonthProductValueDetail detail = new MonthProductValueDetail {
                Quantity = (dr["Quantity"].ToString() == "") ? 0M : Convert.ToDecimal(dr["Quantity"].ToString()),
                MonthOverQuantity = (dr["MonthOverQuantity"].ToString() == "") ? 0M : Convert.ToDecimal(dr["MonthOverQuantity"].ToString()),
                AccumulativeQuantity = (dr["AccumulativeQuantity"].ToString() == "") ? 0M : Convert.ToDecimal(dr["AccumulativeQuantity"].ToString()),
                SynthPrice = (dr["SynthPrice"].ToString() == "") ? 0M : Convert.ToDecimal(dr["SynthPrice"].ToString()),
                ProductValue = (dr["ProductValue"].ToString() == "") ? 0M : Convert.ToDecimal(dr["ProductValue"].ToString()),
                ReportQuantity = (dr["ReportQuantity"].ToString() == "") ? 0M : Convert.ToDecimal(dr["ReportQuantity"].ToString()),
                SuperQuantity = (dr["SuperQuantity"].ToString() == "") ? 0M : Convert.ToDecimal(dr["SuperQuantity"].ToString()),
                AccSuperQuantity = (dr["AccSuperQuantity"].ToString() == "") ? 0M : Convert.ToDecimal(dr["AccSuperQuantity"].ToString())
            };
            try
            {
                detail.PlanOverQuantity = Convert.ToDecimal(dr["PlanOverQuantity"].ToString());
            }
            catch
            {
            }
            try
            {
                detail.PlanAccumulativeQuantity = Convert.ToDecimal(dr["PlanAccumulativeQuantity"].ToString());
            }
            catch
            {
            }
            try
            {
                detail.PlanProductValue = Convert.ToDecimal(dr["PlanProductValue"].ToString());
            }
            catch
            {
            }
            detail.Unit = dr["Unit"].ToString();
            detail.TaskList.ProjectCode = new Guid(dr["ProjectCode"].ToString());
            detail.TaskList.ParentTaskCode = dr["ParentTaskCode"].ToString();
            detail.TaskList.TaskCode = dr["TaskCode"].ToString();
            detail.TaskList.TaskName = dr["TaskName"].ToString();
            detail.TaskList.WorkLayer = (int) dr["WorkLayer"];
            detail.TaskList.ChildNum = (int) dr["ChildNum"];
            return detail;
        }

        public DataTable GetProductValueMonthData(Guid guidProjectCode, string strTaskCode)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProjectCode", guidProjectCode), new SqlParameter("@TaskCode", strTaskCode) };
            return publicDbOpClass.ExecuteDataTable("EPM_P_ProjectCost_ProductValueMonthData", commandParameters);
        }

        public DataTable GetProductValueMonthPlan(Guid projectCode)
        {
            new ArrayList();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProjectCode", projectCode) };
            return publicDbOpClass.ExecuteDataTable("EPM_P_ProjectCost_MonthProductValue", commandParameters);
        }

        public DataTable GetProductValueStat(Guid ProjectCode)
        {
            string str = "";
            object obj2 = str + " select a.ProjectCode,a.TaskName,a.TaskCode,a.ParentTaskCode,a.ChildNum,a.WorkLayer,b.Unit, ";
            object obj3 = string.Concat(new object[] { obj2, " dbo.f_GetQuantityFromProjectCostDetail('", ProjectCode, "',a.TaskCode,'Plan') as PlanQuantity, " });
            object obj4 = string.Concat(new object[] { obj3, " dbo.f_GetQuantityFromProjectCostDetail('", ProjectCode, "',a.TaskCode,'Report') as ReportQuantity, " });
            object obj5 = (string.Concat(new object[] { obj4, " dbo.f_GetQuantityFromProjectCostDetail('", ProjectCode, "',a.TaskCode,'Super') as SuperQuantity " }) + " from EPM_ProjectCost_MonthProductValueDetail b join ") + " EPM_Task_TaskList a on b.ProjectCode=a.ProjectCode and b.TaskCode=a.TaskCode " + " where ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj5, " b.ProjectCode='", ProjectCode, "' " }) + " group by a.ProjectCode,a.TaskName,b.TaskCode,a.TaskCode,a.ParentTaskCode,a.ChildNum,a.WorkLayer,b.Unit,a.ContractPrice  order by a.TaskCode");
        }

        public decimal GetStaticOverQuantity(Guid ProjectCode, string TaskCode, DateTime dtStartDate)
        {
            decimal num = 0M;
            string sqlString = string.Concat(new object[] { " select isnull(sum(isnull(TodayComplete,0.00)),0.00) as StaticOverQuantity from EPM_Book_ConstructTask where ProjectCode='", ProjectCode, "' and TaskCode='", TaskCode, "' and datediff(m,'", dtStartDate, "',ConstructDate)=0 " });
            try
            {
                DataTable table = publicDbOpClass.DataTableQuary(sqlString);
                if (table.Rows.Count > 0)
                {
                    num = Convert.ToDecimal(table.Rows[0]["StaticOverQuantity"].ToString());
                }
            }
            catch
            {
            }
            return num;
        }

        public DataTable GetTotalProductValueStat(Guid ProjectCode)
        {
            string str = "";
            object obj2 = str + " select isnull(sum(isnull(PlanOverQuantity,0.00)*isnull(SynthPrice,0)),0.00) as PlanOverQuantity,isnull(sum(isnull(ReportQuantity,0.00)*SynthPrice),0.00) as ReportQuantity,isnull(sum(isnull(SuperQuantity,0.00)*isnull(SynthPrice,0)),0.00) as SuperQuantity " + " from EPM_ProjectCost_MonthProductValueDetail ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where ProjectCode='", ProjectCode, "' " }));
        }

        public bool IsProductValueExamine(Guid guidProjectCode, DateTime dtDateTime)
        {
            bool flag = false;
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { " select * from EPM_ProjectCost_MonthProductValue where ProjectCode='", guidProjectCode, "'and (DateDiff(m,StartDate,'", dtDateTime, "')=0 or DateDiff(m,EndDate,'", dtDateTime, "')=0) " }));
            if ((table.Rows.Count > 0) && (table.Rows[0]["IsHavePlanExamine"].ToString().Trim() == "1"))
            {
                flag = true;
            }
            return flag;
        }

        public bool IsProductValueReport(Guid guidProjectCode, DateTime dtDateTime)
        {
            bool flag = false;
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { " select * from EPM_ProjectCost_MonthProductValue where ProjectCode='", guidProjectCode, "'and (DateDiff(m,StartDate,'", dtDateTime, "')=0 or DateDiff(m,EndDate,'", dtDateTime, "')=0) " }));
            if ((table.Rows.Count > 0) && (table.Rows[0]["IsHaveReprot"].ToString().Trim() == "1"))
            {
                flag = true;
            }
            return flag;
        }

        public bool ProductValueExamine(Guid guidProjectCode, DateTime dtDateTime)
        {
            bool flag = false;
            string sqlString = string.Concat(new object[] { " update EPM_ProjectCost_MonthProductValue set IsHavePlan='1' where ProjectCode='", guidProjectCode, "'and (DateDiff(m,StartDate,'", dtDateTime, "')=0 or DateDiff(m,EndDate,'", dtDateTime, "')=0) " });
            try
            {
                flag = publicDbOpClass.NonQuerySqlString(sqlString);
            }
            catch
            {
            }
            return flag;
        }

        public ArrayList ProductValueMonthStatWeave(Guid projectCode, DateTime dtStartDate)
        {
            string str = "";
            ArrayList list = new ArrayList();
            object obj2 = (((str + " select distinct a.TaskCode,a.ProjectCode,a.TaskName,a.ParentTaskCode,a.ChildNum,a.WorkLayer,b.Unit, " + " isnull(b.Quantity,0) as Quantity,isnull(b.MonthOverQuantity,0) as MonthOverQuantity, ") + " isnull(b.AccumulativeQuantity,0) as AccumulativeQuantity,isnull(b.SynthPrice,0) as SynthPrice, " + " isnull(b.MonthOverQuantity,0)*isnull(b.SynthPrice,0) as ProductValue,isnull(b.ReportQuantity,0) as ReportQuantity, ") + " isnull(b.SuperQuantity,0) as SuperQuantity,isnull(b.AccSuperQuantity,0) as AccSuperQuantity " + " from EPM_ProjectCost_MonthProductValueDetail b ") + " join EPM_Task_TaskList a on b.ProjectCode=a.ProjectCode and a.WbsType=1 and b.TaskCode=a.TaskCode " + " where ";
            using (DataTable table = publicDbOpClass.DataTableQuary((string.Concat(new object[] { obj2, " b.ProjectCode='", projectCode, "' and " }) + " year(b.StatDate)='" + dtStartDate.Year.ToString() + "' and ") + " month(b.StatDate)='" + dtStartDate.Month.ToString() + "' "))
            {
                if (table.Rows.Count <= 0)
                {
                    return list;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    list.Add(this.GetMonthProductValueDetailFromDataRow(table.Rows[i]));
                }
            }
            return list;
        }

        public ArrayList ProductValueMonthStatWeave(Guid projectCode, DateTime dtStartDate, string type)
        {
            string str = "";
            ArrayList list = new ArrayList();
            str = ((str + " select distinct a.TaskCode,a.ProjectCode,a.TaskName,a.ParentTaskCode,a.ChildNum,a.WorkLayer,b.Unit, ") + " isnull(b.Quantity,0) as Quantity,isnull(b.MonthOverQuantity,0) as MonthOverQuantity, " + " isnull(b.AccumulativeQuantity,0) as AccumulativeQuantity,a.ContractPrice as SynthPrice, ") + " isnull(b.ReportQuantity,0) as ReportQuantity, " + " isnull(b.SuperQuantity,0) as SuperQuantity,isnull(b.AccSuperQuantity,0) as AccSuperQuantity, ";
            if (type == "S")
            {
                str = str + " isnull(b.MonthOverQuantity,0)*a.ContractPrice as ProductValue, ";
            }
            else if (type == "F")
            {
                str = str + " isnull(b.SuperQuantity,0)*a.ContractPrice as ProductValue, ";
            }
            else
            {
                str = str + " 0 as ProductValue, ";
            }
            object obj2 = ((str + " isnull(b.PlanOverQuantity,0) as PlanOverQuantity,isnull(b.PlanAccumulativeQuantity,0) as PlanAccumulativeQuantity, ") + " isnull(b.PlanOverQuantity,0)*a.ContractPrice as PlanProductValue " + " from EPM_ProjectCost_MonthProductValueDetail b ") + " left join EPM_Task_TaskList a on b.ProjectCode=a.ProjectCode and b.TaskCode=a.TaskCode and a.WbsType=1 " + " where ";
            object obj3 = string.Concat(new object[] { obj2, " b.ProjectCode='", projectCode, "' and " });
            using (DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj3, " datediff(m,b.FillDate,'", dtStartDate, "')=0 " })))
            {
                if (table.Rows.Count <= 0)
                {
                    return list;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    list.Add(this.GetMonthProductValueDetailFromDataRow(table.Rows[i]));
                }
            }
            return list;
        }

        public int ProductValuePlanExamine(Guid ProjectCode, DateTime dtStartDate, string strYHDM)
        {
            object obj2 = string.Empty + " update EPM_ProjectCost_MonthProductValue set ";
            string sqlString = string.Concat(new object[] { obj2, " IsHavePlanExamine='1',PlanExaminePerson='", strYHDM, "' where ProjectCode='", ProjectCode, "' and datediff(m,StartDate,'", dtStartDate, "')=0 " });
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int ProductValueReportExamine(Guid ProjectCode, DateTime dtStartDate, string strYHDM)
        {
            object obj2 = string.Empty + " update EPM_ProjectCost_MonthProductValue set ";
            string sqlString = string.Concat(new object[] { obj2, " IsHaveExamine='1',ReportExaminePerson='", strYHDM, "' where ProjectCode='", ProjectCode, "' and datediff(m,StartDate,'", dtStartDate, "')=0 " });
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public int updateProductValueDetail(Guid ProjectCode, DateTime dtStartDate)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProjectCode", ProjectCode), new SqlParameter("@Date", dtStartDate) };
            publicDbOpClass.ExecuteProcedure("EPM_P_ProjectCost_InitMonthProductValueDetail", commandParameters);
            return 1;
        }
    }
}

