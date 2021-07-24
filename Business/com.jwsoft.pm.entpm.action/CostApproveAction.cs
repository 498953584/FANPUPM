namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;

    public class CostApproveAction
    {
        public int DeleteCostApprotion(Guid projectCode, DateTime startDate)
        {
            string sqlString = "";
            object obj2 = sqlString + " update EPM_Stuff_ApplyBillDetails set CostType='' where ApplyBillCode in " + " (select ApplyBillCode from EPM_Stuff_ApplyBillMain ";
            object obj3 = (string.Concat(new object[] { obj2, " where ProjectCode='", projectCode, "' and year(ApplyDate)='", startDate.Year.ToString(), "' and Month(ApplyDate)='", startDate.Month.ToString(), "') " }) + " and CostType<>'' ") + " update EPM_Book_Resource set CostType='' where TaskBookCode in " + " (select TaskBookCode from EPM_Book_ConstructTask ";
            sqlString = string.Concat(new object[] { obj3, " where ProjectCode='", projectCode, "' and year(ConstructDate)='", startDate.Year.ToString(), "' and Month(ConstructDate)='", startDate.Month.ToString(), "') " }) + " and CostType<>'' ";
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

        public int DeleteCostApprove(Guid projectCode, DateTime startDate)
        {
            string sqlString = "";
            object obj2 = sqlString + " update EPM_Book_Resource set CostType='0' where TaskBookCode in " + " (select TaskBookCode from EPM_Book_ConstructTask ";
            sqlString = string.Concat(new object[] { obj2, " where ProjectCode='", projectCode, "' and year(ConstructDate)='", startDate.Year.ToString(), "' and Month(ConstructDate)='", startDate.Month.ToString(), "') " }) + " and CostType<>'0' ";
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

        public DataTable GetCostApproveList(Guid projectCode, DateTime dtStartDate)
        {
            object obj2 = ("" + " select a.ConstructDate,a.TaskBookCode,b.TaskName,a.ConstructUnit,b.TaskCode from EPM_Book_ConstructTask a ") + " join EPM_Task_TaskList b on a.ProjectCode=b.ProjectCode and a.TaskCode=b.TaskCode and b.WbsType=1 " + " where ";
            object obj3 = string.Concat(new object[] { obj2, " a.ProjectCode='", projectCode, "' and  " });
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj3, " datediff(m,a.ConstructDate,'", dtStartDate, "')=0 " }));
        }

        public static DataTable getCostPassMonth(Guid ProjectCode, string year)
        {
            string str2 = "select distinct(month(StartDate)) as [month] from EPM_Cost_MonthCostApprove ";
            return publicDbOpClass.DataTableQuary(str2 + " where ProjectCode = '" + ProjectCode.ToString() + "' and year(StartDate)='" + year.ToString() + "'");
        }

        public static DataTable getCostPassYear(Guid ProjectCode)
        {
            return publicDbOpClass.DataTableQuary("select distinct(year(StartDate)) as [year] from EPM_Cost_MonthCostApprove where ProjectCode = '" + ProjectCode.ToString() + "'");
        }

        public string GetCostType(Guid projectCode, string costType, DateTime startDate)
        {
            string str = "";
            using (DataTable table = publicDbOpClass.DataTableQuary(" select * from EPM_CostApprove_CBS_Analyze where NodeCode='" + costType + "' and CostType='1' "))
            {
                if (table.Rows.Count > 0)
                {
                    str = table.Rows[0]["NodeName"].ToString();
                }
            }
            return str;
        }

        public DataTable GetMonthCostApprove(Guid projectCode)
        {
            new ArrayList();
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProjectCode", projectCode) };
            return publicDbOpClass.ExecuteDataTable("EPM_P_ProjectCost_MonthCostApprove", commandParameters);
        }

        public static DataTable getMonthJobOutCost(Guid ProjectCode, string Year, string Month)
        {
            return publicDbOpClass.DataTableQuary("select * from f_Cost_MonthJobOutCost('" + ProjectCode.ToString() + "','" + Year + "','" + Month + "')");
        }

        public DataTable GetTaskBookResourceList(string strTaskBookCode, string strTaskCode)
        {
            string str2 = ("" + " select b.ResourceName,b.Specification,b.UnitName,a.UnitPrice,a.Quantity,a.ResourceStyle,a.CostType,a.NoteID ") + " from EPM_Book_Resource a left outer join EPM_V_Res_ResourceBasic b on a.ResourceCode=b.ResourceCode " + " where ";
            return publicDbOpClass.DataTableQuary(str2 + " a.TaskBookCode='" + strTaskBookCode + "' and a.TaskCode='" + strTaskCode + "'");
        }

        public int UpdateCostApprove(ArrayList arr)
        {
            string sqlString = "";
            if (arr.Count > 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    if (((CostApproveInfo) arr[i]).IsSelected)
                    {
                        object obj2 = sqlString;
                        sqlString = string.Concat(new object[] { obj2, " update EPM_Book_Resource set CostType='", ((CostApproveInfo) arr[i]).CostType, "',Quantity=", ((CostApproveInfo) arr[i]).Quantity, ",UnitPrice=", ((CostApproveInfo) arr[i]).UnitPrice, " where NoteID='", ((CostApproveInfo) arr[i]).NoteID, "' " });
                    }
                    else
                    {
                        object obj3 = sqlString;
                        sqlString = string.Concat(new object[] { obj3, " update EPM_Book_Resource set CostType='0' where NoteID='", ((CostApproveInfo) arr[i]).NoteID, "' " });
                    }
                }
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
    }
}

