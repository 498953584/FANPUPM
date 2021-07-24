namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;

    public class CBSAction
    {
        public int AddBudgetMoney(ArrayList arr)
        {
            int num = 0;
            string sqlString = string.Empty;
            decimal num2 = 0M;
            object obj2 = sqlString;
            sqlString = string.Concat(new object[] { obj2, " delete from EPM_Cost_Cbs_HypotaxisTable where ProjectCode='", ((CBSHypotaxisTableInfo) arr[0]).ProjectCode, "' and NodeCode='", ((CBSHypotaxisTableInfo) arr[0]).NodeCode, "' " });
            for (int i = 0; i < arr.Count; i++)
            {
                object obj3 = sqlString + " insert into EPM_Cost_Cbs_HypotaxisTable(ProjectCode,NodeCode,BudgetMoney,BudgetMonth,BudgetYear) ";
                sqlString = string.Concat(new object[] { obj3, " values('", ((CBSHypotaxisTableInfo) arr[i]).ProjectCode, "','", ((CBSHypotaxisTableInfo) arr[i]).NodeCode, "','", ((CBSHypotaxisTableInfo) arr[i]).BudgetMoney, "','", ((CBSHypotaxisTableInfo) arr[i]).BudgetMonth, "','", ((CBSHypotaxisTableInfo) arr[i]).BudgetYear, "') " });
                num2 += ((CBSHypotaxisTableInfo) arr[i]).BudgetMoney;
            }
            try
            {
                publicDbOpClass.ExecSqlString(sqlString);
                num = 1;
            }
            catch
            {
            }
            return num;
        }

        public int AddCBSNodeInfo(CBSFeeNodeInfo CBSNode)
        {
            object obj2 = (("" + " insert into EPM_CostApprove_CBS_Analyze(NodeCode,NodeName,NodeLayer,CostType,IsValid,Remark) " + " values( ") + " '" + CBSNode.NodeCode + "', ") + " '" + CBSNode.NodeName + "', ";
            object obj3 = string.Concat(new object[] { obj2, " '", CBSNode.NodeLayer, "', " }) + " '" + CBSNode.CostType + "', ";
            string sqlString = (string.Concat(new object[] { obj3, " '", CBSNode.IsValid, "', " }) + " '" + CBSNode.Remark + "' ") + " ) ";
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

        public bool CBSNodeIsHaveFee(string nodeCode, string costType)
        {
            decimal num = 0M;
            bool flag = false;
            if (costType == "1")
            {
                string sqlString = " select sum(isnull(Quantity,0.00)*isnull(UnitPrice,0.00)) as TotalMoney from EPM_Book_Resource where CostType='" + nodeCode + "' ";
                try
                {
                    using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
                    {
                        if (table.Rows.Count > 0)
                        {
                            num += Convert.ToDecimal(table.Rows[0]["TotalMoney"].ToString());
                        }
                    }
                }
                catch
                {
                }
            }
            if (costType == "2")
            {
                string str2 = " select sum(isnull(a.Price,0.00)) as TotalMoney from EPM_CostImportChild a join Prj_CostImport b on a.ID=b.ID and AuditResult=1 where CostCode='" + nodeCode + "' ";
                try
                {
                    using (DataTable table2 = publicDbOpClass.DataTableQuary(str2))
                    {
                        if (table2.Rows.Count > 0)
                        {
                            num += Convert.ToDecimal(table2.Rows[0]["TotalMoney"].ToString());
                        }
                    }
                }
                catch
                {
                }
            }
            if (num > 0M)
            {
                flag = true;
            }
            return flag;
        }

        public int DeleteCBSNode(string nodeCode)
        {
            string sqlString = "";
            sqlString = sqlString + " delete from EPM_CostApprove_CBS_Analyze where NodeCode='" + nodeCode + "' ";
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

        public ArrayList GetAllCbs(string prjGuidCode, string Type)
        {
            ArrayList list = new ArrayList();
            DataTable table = new DataTable();
            if (Type == "budget")
            {
                string str = "";
                string str2 = str + " select PrjCode as ProjectCode,left(nodecode,len(nodecode)-3) as PNode,NodeCode, ";
                table = publicDbOpClass.DataTableQuary(((str2 + " replicate('&nbsp;',(Len(NodeCode)/3-1)*4)+NodeName as NodeName,dbo.f_IsHaveChildFromCBS('" + prjGuidCode + "',NodeCode) as IsHaveChild,dbo.f_GetFeeFromCBS('" + prjGuidCode + "',NodeCode,dbo.f_IsHaveChildFromCBS('" + prjGuidCode + "',NodeCode)) as Money,CostType,NodeLayer,IsValid,Remark ") + " from EPM_Cost_Cbs where IsValid=1 and PrjCode='" + prjGuidCode + "' and ") + " CostType='2' order by NodeCode asc ");
            }
            else if (Type == "plan")
            {
                SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ProjectCode", prjGuidCode) };
                table = publicDbOpClass.ExecuteDataTable("EPM_P_Cost_CbsBuget", commandParameters);
            }
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(this.GetCBSFeeNodeFromDataRow(table.Rows[i]));
            }
            return list;
        }

        public ArrayList GetAllCBSApprotionFee(string prjGuidCode, DateTime startDate)
        {
            ArrayList list = new ArrayList();
            string str = "";
            string str2 = str + " select PrjCode as ProjectCode,left(nodecode,len(nodecode)-3) as PNode,NodeCode, ";
            DataTable table = publicDbOpClass.DataTableQuary((((str2 + " replicate('&nbsp;',(Len(NodeCode)/3-1)*4)+NodeName as NodeName,CostType,NodeLayer,IsValid,Remark,dbo.f_GetApportionFeeFromCBSFee('" + prjGuidCode + "',NodeCode,'" + startDate.ToShortDateString() + "',dbo.f_IsHaveChildFromApprotionFeeTab('" + prjGuidCode + "',NodeCode,'" + startDate.ToShortDateString() + "')) as Money ") + " from EPM_CostApprove_CBS_Fee where IsValid=1 and PrjCode='" + prjGuidCode + "' and ") + " Year(StartDate)='" + startDate.Year.ToString() + "' and ") + " Month(StartDate)='" + startDate.Month.ToString() + "' and CostType='2' order by NodeCode asc ");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(this.GetCBSFeeNodeFromDataRow(table.Rows[i]));
            }
            return list;
        }

        public ArrayList GetAllCBSApproveList()
        {
            ArrayList list = new ArrayList();
            string sqlString = "select left(nodecode,len(nodecode)-3) as PNode,NodeCode,NodeName,CostType,NodeLayer,IsValid,Remark  from EPM_CostApprove_CBS_Analyze where IsValid=1 and CostType='1' ";
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(this.GetCBSFeeNodeFromDataRow(table.Rows[i]));
            }
            return list;
        }

        public ArrayList GetAllCBSApproveList2()
        {
            ArrayList list = new ArrayList();
            string sqlString = "select left(nodecode,len(nodecode)-3) as PNode,NodeCode,NodeName,CostType,NodeLayer,IsValid,Remark  from EPM_CostApprove_CBS_Analyze where IsValid=1 and CostType='2' ";
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(this.GetCBSFeeNodeFromDataRow(table.Rows[i]));
            }
            return list;
        }

        public ArrayList GetAllCBSFee(string prjGuidCode, DateTime startDate)
        {
            ArrayList list = new ArrayList();
            string sqlString = "";
            sqlString = "update EPM_CostApprove_CBS_Fee set Money = b.Money from EPM_CostApprove_CBS_Fee a,";
            string str2 = sqlString + "(SELECT SUM(ISNULL(Quantity, 0) * ISNULL(UnitPrice, 0)) AS Money, CostType FROM EPM_V_Cost_ApproveCost ";
            publicDbOpClass.NonQuerySqlString((str2 + " WHERE (Projectcode = '" + prjGuidCode + "') AND (YEAR(ConstructDate) = '" + startDate.Year.ToString() + "')") + " AND (MONTH(ConstructDate) = '" + startDate.Month.ToString() + "') GROUP BY CostType) b where a.NodeCode = b.CostType");
            publicDbOpClass.DataTableQuary("select * from EPM_V_Cost_Overhead where prjcode ='" + prjGuidCode + "' and AuditResult='1' ");
            sqlString = "";
            string str3 = "update EPM_CostApprove_CBS_Fee set Money = (select Sum(CurrentSumMoney) as Money from f_Cost_MonthJobOutCost('" + prjGuidCode + "','" + startDate.Year.ToString() + "','" + startDate.Month.ToString() + "'))";
            sqlString = str3 + " where (Prjcode = '" + prjGuidCode + "') AND YEAR(StartDate) = '" + startDate.Year.ToString() + "' AND (MONTH(StartDate) = '" + startDate.Month.ToString() + "') and len(NodeCode)=3 and CostType = 3";
            publicDbOpClass.ExecSqlString(sqlString);
            object obj2 = ((sqlString + " select a.PrjCode as ProjectCode,left(nodecode,len(nodecode)-3) as PNode,a.NodeCode,") + " replicate('&nbsp;',(Len(a.NodeCode)/3-1)*4)+a.NodeName as NodeName,a.CostType,a.NodeLayer,a.IsValid,a.Remark, " + " (case  when a.Money=0  then (select isnull(sum(Money),0) from EPM_CostApprove_CBS_Fee b where b.prjcode=a.PrjCode and (SUBSTRING(NodeCode, 1, LEN(NodeCode) - 3)= a.NodeCODE) and datediff(m,a.StartDate,StartDate)=0 ) else a.Money end ) AS Money ") + " \tfrom EPM_CostApprove_CBS_Fee a " + " where ";
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " a.PrjCode='", prjGuidCode, "' and datediff(m,a.StartDate,'", startDate, "')=0 " }));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(this.GetCBSFeeNodeFromDataRow(table.Rows[i]));
            }
            return list;
        }

        public ArrayList GetAllCBSFeeList()
        {
            ArrayList list = new ArrayList();
            string sqlString = "select left(nodecode,len(nodecode)-3) as PNode,NodeCode,NodeName,CostType,NodeLayer,IsValid,Remark  from EPM_CostApprove_CBS_Analyze where IsValid=1 ";
            DataTable table = publicDbOpClass.DataTableQuary(sqlString);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(this.GetCBSFeeNodeFromDataRow(table.Rows[i]));
            }
            return list;
        }

        private CBSFeeNodeInfo GetCBSFeeNodeFromDataRow(DataRow dr)
        {
            CBSFeeNodeInfo info = new CBSFeeNodeInfo {
                NodeCode = dr["NodeCode"].ToString(),
                NodeName = dr["NodeName"].ToString(),
                NodeLayer = (dr["NodeLayer"].ToString() == "") ? -2147483648 : ((int) dr["NodeLayer"]),
                IsValid = (dr["IsValid"].ToString() == "") ? -2147483648 : ((int) dr["IsValid"]),
                PNode = dr["PNode"].ToString(),
                CostType = dr["CostType"].ToString()
            };
            try
            {
                info.Money = (dr["Money"].ToString() == "") ? 0M : ((decimal) dr["Money"]);
                info.StartDate = (dr["StartDate"].ToString() == "") ? DateTime.Now : ((DateTime) dr["StartDate"]);
            }
            catch
            {
            }
            try
            {
                info.IsHaveChild = int.Parse(dr["IsHaveChild"].ToString()) >= 1;
            }
            catch
            {
            }
            try
            {
                info.TargetMoney = (dr["TargetMoney"].ToString() == "") ? 0M : ((decimal) dr["TargetMoney"]);
            }
            catch
            {
            }
            try
            {
                info.PlanMoney = (dr["PlanMoney"].ToString() == "") ? 0M : ((decimal) dr["PlanMoney"]);
            }
            catch
            {
            }
            info.Remark = dr["Remark"].ToString();
            return info;
        }

        private CBSHypotaxisTableInfo GetCBSFromDataRow(DataRow dr)
        {
            return new CBSHypotaxisTableInfo { ProjectCode = (Guid) dr["ProjectCode"], NodeCode = dr["NodeCode"].ToString(), NodeName = dr["NodeName"].ToString(), BudgetMonth = dr["BudgetMonth"].ToString(), BudgetMoney = (decimal) dr["BudgetMoney"], BudgetYear = dr["BudgetYear"].ToString() };
        }

        public ArrayList GetCBSHypotaxisTable(Guid ProjectCode, string NodeCode)
        {
            DataTable table = new DataTable();
            ArrayList list = new ArrayList();
            string sqlString = "select a.ProjectCode,a.NodeCode,b.NodeName,a.BudgetMoney,a.BudgetYear,a.BudgetMonth ";
            sqlString = ((sqlString + "from EPM_Cost_Cbs_HypotaxisTable a left join EPM_Cost_Cbs b on a.ProjectCode=b.PrjCode ") + " and a.NodeCode=b.NodeCode where ProjectCode='" + ProjectCode.ToString() + "'") + " and a.NodeCode='" + NodeCode.ToString() + "' order by BudgetYear,BudgetMonth ASC";
            try
            {
                table = publicDbOpClass.DataTableQuary(sqlString);
                if (table.Rows.Count <= 0)
                {
                    return list;
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    list.Add(this.GetCBSFromDataRow(table.Rows[i]));
                }
            }
            catch
            {
            }
            return list;
        }

        public CBSFeeNodeInfo GetCBSNodeByCode(string nodeCode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select *,left(nodecode,len(nodecode)-3) as PNode from EPM_CostApprove_CBS_Analyze where NodeCode = '" + nodeCode + "'");
            if (table.Rows.Count == 1)
            {
                return this.GetCBSFeeNodeFromDataRow(table.Rows[0]);
            }
            return new CBSFeeNodeInfo();
        }

        public string GetcbsNodeNameByNodedoe(string strCBSCode)
        {
            return publicDbOpClass.ExecuteScalar("select NodeName from EPM_CostApprove_CBS_Analyze where NodeCode='" + strCBSCode + "'").ToString();
        }

        public CostCbsInfo GetCostCbsRowInfo(DataRow dr)
        {
            CostCbsInfo info = new CostCbsInfo {
                PrjCode = dr["ProjectCode"].ToString(),
                NodeCode = dr["NodeCode"].ToString(),
                NodeName = dr["NodeName"].ToString(),
                NodeLayer = (dr["NodeLayer"].ToString() == "") ? -2147483648 : ((int) dr["NodeLayer"]),
                IsValid = (dr["IsValid"].ToString() == "") ? -2147483648 : ((int) dr["IsValid"]),
                PNode = dr["PNode"].ToString(),
                CostType = dr["CostType"].ToString()
            };
            try
            {
                info.Money = (dr["Money"].ToString() == "") ? 0M : ((decimal) dr["Money"]);
            }
            catch
            {
            }
            info.Remark = dr["Remark"].ToString();
            return info;
        }

        public int GetCountCbs(string prjcode, int type)
        {
            return Convert.ToInt32(publicDbOpClass.ExecuteScalar("select count(1) from EPM_Cost_Cbs where PrjCode ='" + prjcode + "'"));
        }

        public string InitCBSCode(string parentNode)
        {
            int num = parentNode.Length + 3;
            DataTable table = publicDbOpClass.DataTableQuary("select max(NodeCode) from EPM_CostApprove_CBS_Analyze where (NodeCode like '" + parentNode + "%' and len(NodeCode) = " + num.ToString() + ")");
            string str3 = table.Rows[0][0].ToString().Trim();
            if (parentNode.Trim().Length == 0)
            {
                if (str3.Length == 0)
                {
                    return "001";
                }
                return Convert.ToString((int) (Convert.ToInt32(str3) + 1)).PadLeft(3, '0');
            }
            if (str3.Length == 0)
            {
                return (parentNode + "001");
            }
            string str4 = Convert.ToString((int) (Convert.ToInt32(str3.Substring(str3.Length - 3, 3)) + 1));
            return (parentNode + str4.PadLeft(3, '0'));
        }

        public void InitialzeCBS(Guid guidPrjCode)
        {
            string sqlString = "select NodeCode,NodeName from EPM_CostApprove_CBS_Analyze where IsValid='1' ";
            using (DataTable table = publicDbOpClass.DataTableQuary(sqlString))
            {
                if (table.Rows.Count > 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        object obj2 = string.Empty;
                        object obj3 = string.Concat(new object[] { obj2, " if not exists(select top 1 NodeCode from EPM_Cost_Cbs where PrjCode ='", guidPrjCode, "' and NodeCode='", table.Rows[i]["NodeCode"].ToString(), "' and NodeName='", table.Rows[i]["NodeName"].ToString(), "') " });
                        publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj3, "delete from epm_cost_cbs where prjcode='", guidPrjCode, "' and  NodeCode='", table.Rows[i]["NodeCode"].ToString(), "'" }));
                    }
                }
                if (table.Rows.Count > 0)
                {
                    for (int j = 0; j < table.Rows.Count; j++)
                    {
                        object obj4 = string.Empty;
                        object obj5 = string.Concat(new object[] { obj4, " if not exists(select top 1 NodeCode from EPM_Cost_Cbs where PrjCode ='", guidPrjCode, "' and NodeCode='", table.Rows[j]["NodeCode"].ToString(), "') " }) + " insert into EPM_Cost_Cbs (PrjCode,NodeCode,NodeName,NodeLayer,Money,TargetMoney,CostType,IsValid,Remark) ";
                        publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj5, " select '", guidPrjCode, "' as PrjCode,NodeCode,NodeName,NodeLayer,0 as Money,0 as TargetMoney,CostType,IsValid,Remark from EPM_CostApprove_CBS_Analyze where NodeCode='", table.Rows[j]["NodeCode"].ToString(), "' and IsValid=1 " }));
                    }
                }
            }
            using (DataTable table2 = publicDbOpClass.DataTableQuary("select NodeCode from epm_cost_cbs where prjcode='" + guidPrjCode + "'"))
            {
                if (table2.Rows.Count > 0)
                {
                    for (int k = 0; k < table2.Rows.Count; k++)
                    {
                        object obj6 = string.Empty + " if not exists(select top 1 NodeCode from EPM_CostApprove_CBS_Analyze where NodeCode='" + table2.Rows[k]["NodeCode"].ToString() + "') ";
                        publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj6, " delete from epm_cost_cbs where prjcode='", guidPrjCode, "' and  NodeCode='", table2.Rows[k]["NodeCode"].ToString(), "'" }));
                    }
                }
            }
        }

        public int InitialzeCBS(string prjGuidCode, DateTime startDate)
        {
            object obj2 = string.Concat(new object[] { "delete EPM_CostApprove_CBS_Fee where Prjcode ='", prjGuidCode, "' and StartDate>='", startDate, "' and StartDate<='", startDate.AddMonths(1).AddDays(-1.0), "' " }) + " insert into EPM_CostApprove_CBS_Fee(PrjCode,NodeCode,NodeName,NodeLayer,StartDate,Money,CostType,IsValid,RowState,Remark) ";
            publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " select '", prjGuidCode, "' as PrjCode,NodeCode,NodeName,NodeLayer,'", startDate, "' as StartDate,0 as Money,CostType,IsValid,'new' as RowState,Remark from EPM_CostApprove_CBS_Analyze where IsValid=1 " }));
            DataTable table = publicDbOpClass.DataTableQuary("select * from EPM_V_Cost_Overhead where prjcode ='" + prjGuidCode + "' and AuditResult='1' ");
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    object obj3 = "update EPM_CostApprove_CBS_Fee set Money = Money + " + decimal.Parse(table.Rows[i]["price"].ToString());
                    publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj3, " where PrjCode ='", table.Rows[i]["prjcode"], "' and nodecode ='", table.Rows[i]["costcode"], "' and ", DateTime.Parse(table.Rows[i]["HappenDate"].ToString()).Year, " = ", startDate.Year, " and ", DateTime.Parse(table.Rows[i]["HappenDate"].ToString()).Month, "=", startDate.Month }));
                }
            }
            return 1;
        }

        public int InitialzeCBSAnaylze()
        {
            string sqlString = "select * from EPM_CostApprove_CBS_Analyze ";
            if (publicDbOpClass.DataTableQuary(sqlString).Rows.Count < 1)
            {
                return publicDbOpClass.ExecSqlString(sqlString + " insert into EPM_CostApprove_CBS_Analyze(NodeCode,NodeName,NodeLayer,CostType,IsValid,Remark) " + " select NodeCode,NodeName,NodeLayer,cast(left(NodeCode,3) as int),1,'' from EPM_CostApprove_CBS_BaseNode");
            }
            return 1;
        }

        public int UpdateCBSNodeInfo(CBSFeeNodeInfo CBSNode)
        {
            string sqlString = "";
            sqlString = ((((sqlString + " update EPM_CostApprove_CBS_Analyze set ") + " NodeName = '" + CBSNode.NodeName + "', ") + " CostType = '" + CBSNode.CostType + "', ") + " Remark = '" + CBSNode.Remark + "' ") + " where NodeCode = '" + CBSNode.NodeCode + "'";
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

        public int UpdCBS(ArrayList arr)
        {
            string sqlString = "";
            if (arr.Count > 0)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    object obj2 = sqlString;
                    object obj3 = string.Concat(new object[] { obj2, " update EPM_Cost_Cbs set Money='", ((CostCbsInfo) arr[i]).Money, "', " });
                    sqlString = (((string.Concat(new object[] { obj3, " TargetMoney='", ((CostCbsInfo) arr[i]).TargetMoney, "', " }) + " Remark='" + ((CostCbsInfo) arr[i]).Remark + "' ") + " where ") + " PrjCode='" + ((CostCbsInfo) arr[i]).PrjCode + "' and ") + " NodeCode='" + ((CostCbsInfo) arr[i]).NodeCode + "'";
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

