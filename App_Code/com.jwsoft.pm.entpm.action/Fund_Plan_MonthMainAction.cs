namespace com.jwsoft.pm.entpm.action
{
    using cn.justwin.BLL;
    using cn.justwin.DAL;
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Fund_Plan_MonthMainAction
    {
        public void AddPlanMainInfo(Fund_Plan_MonthMainInfo FI)
        {
            publicDbOpClass.ExecuteSQL(string.Format(string.Concat(new object[] { 
                "INSERT INTO Fund_Plan_MonthMain \r\n                                          (MonthPlanID,PrjGuid,PlanMonth,PlanYear,\r\n                                          FlowState,Remark,OperatorCode,OperateTime,PlanType)\r\n                                          VALUES('", FI.MonthPlanID, "','", FI.PrjGuid, "',", FI.PlanMonth, ",", FI.PlanYear, ",", FI.FlowState, ",'", FI.Remark, "','", FI.OperatorCode, "','", FI.OperateTime, 
                "','", FI.PlanType, "')"
             }), new object[0]));
        }

        public void DeleteDetailInfo(Guid UID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete Fund_Plan_MonthDetail ");
            builder.Append(" where UID=@UID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UID", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = UID;
            publicDbOpClass.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public void DeleteMainInfo(Guid MonthPlanID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete Fund_Plan_MonthMain ");
            builder.Append(" where MonthPlanID=@MonthPlanID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@MonthPlanID", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = MonthPlanID;
            publicDbOpClass.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public void DeleteMainInfoList(string ids)
        {
            publicDbOpClass.ExecuteSQL(string.Format("DELETE FROM Fund_Plan_MonthMain WHERE MonthPlanID IN({0}) ", ids));
        }

        public Fund_Plan_MonthMainInfo GetMainInfo(Fund_Plan_MonthMainInfo FI)
        {
            Fund_Plan_MonthMainInfo info = new Fund_Plan_MonthMainInfo {
                MonthPlanID = Guid.NewGuid(),
                FlowState = -1,
                OperateTime = FI.OperateTime,
                OperatorName = "",
                Remark = ""
            };
            StringBuilder builder = new StringBuilder();
            builder.Append("select MonthPlanID,PrjGuid,PlanMonth,PlanYear,FlowState,Remark,OperatorCode,OperateTime,PlanType ");
            builder.Append(" ,(SELECT top 1 [v_xm]   FROM [PT_yhmc] where [v_yhdm]=M.OperatorCode) as yhmc  ");
            builder.Append(" FROM Fund_Plan_MonthMain  M");
            builder.Append(string.Concat(new object[] { " where   PrjGuid='", FI.PrjGuid, "' AND PlanYear=", FI.PlanYear, " AND PlanMonth=", FI.PlanMonth, "  AND plantype= '", FI.PlanType, "'" }));
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count > 0)
            {
                info.MonthPlanID = new Guid(table.Rows[0]["MonthPlanID"].ToString());
                info.FlowState = Convert.ToInt32(table.Rows[0]["FlowState"].ToString());
                info.OperatorName = table.Rows[0]["yhmc"].ToString();
                info.OperateTime = Convert.ToDateTime(table.Rows[0]["OperateTime"]);
                info.Remark = table.Rows[0]["Remark"].ToString();
                if (info.FlowState == -2)
                {
                    builder = new StringBuilder();
                    builder.Append("Begin  ");
                    builder.Append("UPDATE WF_Instance  SET  auditresult=-3 ");
                    builder.Append("where  auditresult=-2 and id  in  (SELECT ID FROM   WF_Instance_Main  WHERE  InstanceCode='" + info.MonthPlanID + "'); ");
                    builder.Append("UPDATE [Fund_Plan_MonthMain]   SET  [FlowState] = -3 ");
                    builder.Append(" WHERE MonthPlanID ='" + info.MonthPlanID + "' and [FlowState]=-2; ");
                    builder.Append("End ");
                    publicDbOpClass.NonQuerySqlString(builder.ToString());
                }
                return info;
            }
            builder = new StringBuilder();
            builder.Append("INSERT INTO Fund_Plan_MonthMain ");
            builder.Append(" (MonthPlanID,PrjGuid,PlanMonth,PlanYear,FlowState,OperatorCode,OperateTime,PlanType) ");
            builder.Append(string.Concat(new object[] { " VALUES( '", info.MonthPlanID, "','", FI.PrjGuid, "',", FI.PlanMonth, ",", FI.PlanYear, ",-1,'", FI.OperatorCode, "','", FI.OperateTime, "','", FI.PlanType, "')" }));
            publicDbOpClass.NonQuerySqlString(builder.ToString());
            return info;
        }

        public Fund_Plan_MonthMainInfo GetMainInfo(string prjId, int Year, int Month, string Plantype)
        {
            Fund_Plan_MonthMainInfo info = new Fund_Plan_MonthMainInfo();
            DataTable table = publicDbOpClass.DataTableQuary(string.Format(" SELECT MonthPlanID,PrjGuid,PlanMonth,PlanYear,FlowState,Remark,\r\n                                             OperatorCode,OperateTime,PlanType ,(SELECT top 1 [v_xm]   \r\n                                             FROM [PT_yhmc] where [v_yhdm]=M.OperatorCode) as yhmc ,(\r\n                                             SELECT top 1 [PrjName]   FROM [PT_prjinfo] where [PrjGuid]=M.PrjGuid) as PrjName\r\n                                             FROM Fund_Plan_MonthMain  M WHERE PrjGuid='{0}' AND PlanYear={1} \r\n                                             AND PlanMonth={2} AND plantype='{3}'", new object[] { prjId, Year, Month, Plantype }));
            if (table.Rows.Count > 0)
            {
                info.MonthPlanID = new Guid(table.Rows[0]["MonthPlanID"].ToString());
                info.PrjGuid = new Guid(table.Rows[0]["PrjGuid"].ToString());
                info.PrjName = string.IsNullOrEmpty(table.Rows[0]["PrjName"].ToString()) ? string.Empty : table.Rows[0]["PrjName"].ToString();
                info.FlowState = Convert.ToInt32(table.Rows[0]["FlowState"].ToString());
                info.OperatorCode = string.IsNullOrEmpty(table.Rows[0]["OperatorCode"].ToString()) ? string.Empty : table.Rows[0]["OperatorCode"].ToString();
                info.OperatorName = string.IsNullOrEmpty(table.Rows[0]["yhmc"].ToString()) ? string.Empty : table.Rows[0]["yhmc"].ToString();
                info.OperateTime = Convert.ToDateTime(table.Rows[0]["OperateTime"]);
                info.Remark = table.Rows[0]["Remark"].ToString();
            }
            return info;
        }

        public Fund_Plan_MonthMainInfo GetMainInfoByMonthPlanID(string MonthPlanID)
        {
            Fund_Plan_MonthMainInfo info = new Fund_Plan_MonthMainInfo();
            StringBuilder builder = new StringBuilder();
            builder.Append("select MonthPlanID,PrjGuid,PlanMonth,PlanYear,FlowState,Remark,OperatorCode,OperateTime,PlanType ");
            builder.Append(" ,(SELECT top 1 [v_xm]   FROM [PT_yhmc] where [v_yhdm]=M.OperatorCode) as yhmc  ");
            builder.Append(" ,(SELECT top 1 [PrjName]   FROM [PT_prjinfo] where [PrjGuid]=M.PrjGuid) as PrjName  ");
            builder.Append(" FROM Fund_Plan_MonthMain  M");
            builder.Append(" where   MonthPlanID='" + MonthPlanID + "' ");
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count > 0)
            {
                info.MonthPlanID = new Guid(table.Rows[0]["MonthPlanID"].ToString());
                info.FlowState = Convert.ToInt32(table.Rows[0]["FlowState"].ToString());
                info.OperatorCode = table.Rows[0]["OperatorCode"].ToString();
                info.PlanType = table.Rows[0]["PlanType"].ToString();
                info.OperateTime = DateTime.Parse(table.Rows[0]["OperateTime"].ToString());
                info.PlanMonth = int.Parse(table.Rows[0]["PlanMonth"].ToString());
                info.PlanYear = int.Parse(table.Rows[0]["PlanYear"].ToString());
                info.PrjGuid = new Guid(table.Rows[0]["PrjGuid"].ToString());
                info.PrjName = table.Rows[0]["PrjName"].ToString();
                info.OperatorName = table.Rows[0]["yhmc"].ToString();
                info.Remark = table.Rows[0]["Remark"].ToString();
            }
            return info;
        }

        public DataTable GetMainList(Fund_Plan_MonthMainInfo FI)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select MonthPlanID,PrjGuid,PlanMonth,PlanYear,FlowState,Remark,OperatorCode,OperateTime,PlanType, PlanDate,(SELECT top 1 [v_xm]   FROM [PT_yhmc] where [v_yhdm]=M.OperatorCode) as yhmc  ");
            builder.Append(", (SELECT SUM([PlanMoney])  FROM [Fund_Plan_MonthDetail] WHERE [MonthPlanID]=M.MonthPlanID) AS  PlanMoney  ");
            builder.Append(" ,(SELECT SUM([OldBalance])  FROM [Fund_Plan_MonthDetail] WHERE [MonthPlanID]=M.MonthPlanID) AS  OldBalance  ");
            builder.Append(" ,(SELECT SUM([thisBalance])  FROM [Fund_Plan_MonthDetail] WHERE [MonthPlanID]=M.MonthPlanID) AS  thisBalance  ");
            if (FI.PlanType == "payout")
            {
                builder.Append(",( SELECT SUM(cpp.PaymentMoney) FROM   Con_Payout_Payment cpp LEFT JOIN Fund_Plan_MonthDetail fpmd ON cpp.MonthPlanUID=fpmd.UID ");
                builder.Append("WHERE fpmd.MonthPlanID=M.MonthPlanID AND cpp.FlowState=1) as PaymentMoney ");
            }
            else if (FI.PlanType == "income")
            {
                builder.Append(",( SELECT SUM(cip.CllectionPrice) FROM Con_Incomet_Payment cip  LEFT JOIN Fund_Plan_MonthDetail fpmd ON  cip.MonthPlanUID=fpmd.UID ");
                builder.Append("WHERE fpmd.MonthPlanID=M.MonthPlanID ) as PaymentMoney ");
            }
            builder.Append(" FROM Fund_Plan_MonthMain  M");
            builder.Append(string.Concat(new object[] { " where PrjGuid='", FI.PrjGuid, "' AND PlanYear=", FI.PlanYear, " AND plantype= '", FI.PlanType, "'" }));
            builder.Append(" Order by  PlanDate Desc ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetMainList(int year, int month, string type, string guid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" Order by  PlanDate Desc ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public List<string> getMonthDetailId(List<string> Ids)
        {
            List<string> list = new List<string>();
            if (Ids.Count > 0)
            {
                string arrayToInStr = StringUtility.GetArrayToInStr(Ids.ToArray());
                string cmdText = string.Format("SELECT UID FROM Fund_Plan_MonthDetail WHERE MonthPlanID IN({0}) ", arrayToInStr);
                foreach (DataRow row in SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]).Rows)
                {
                    if (!list.Contains(row["UID"].ToString()))
                    {
                        list.Add(row["UID"].ToString());
                    }
                }
            }
            return list;
        }

        public DataTable GetPlanDetailAboutIncomet(string MonthPlanID, string prjguid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT epm.*,cic.ContractName,cic.Second AS corpName FROM Fund_Plan_MonthDetail epm ");
            builder.Append("LEFT JOIN Con_Incomet_Contract cic ON cic.ContractID = epm.ContractID ");
            builder.Append("LEFT JOIN Fund_Plan_MonthMain fpmm ON fpmm.MonthPlanID = epm.MonthPlanID ");
            builder.Append(" WHERE epm.MonthPlanID='" + MonthPlanID + "'");
            if (!string.IsNullOrEmpty(prjguid))
            {
                builder.Append(" AND fpmm.PrjGuid='" + prjguid + "'");
            }
            builder.Append(" Order by  OrderID ASC ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetPlanDetailAboutPayout(string MonthPlanID, string prjguid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT epm.*,cpc.ContractName,b.corpName FROM Fund_Plan_MonthDetail epm ");
            builder.Append("LEFT JOIN Con_Payout_Contract cpc ON cpc.ContractID = epm.ContractID ");
            builder.Append("LEFT JOIN Fund_Plan_MonthMain fpmm ON fpmm.MonthPlanID = epm.MonthPlanID ");
            builder.Append("LEFT JOIN XPM_Basic_ContactCorp b ON cpc.bname=b.corpId ");
            builder.Append(" WHERE epm.MonthPlanID='" + MonthPlanID + "'");
            if (!string.IsNullOrEmpty(prjguid))
            {
                builder.Append(" AND fpmm.PrjGuid='" + prjguid + "'");
            }
            builder.Append(" Order by  OrderID ASC ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetPlanDetailList(string MonthPlanID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select D.*  ");
            builder.Append(" FROM Fund_Plan_MonthDetail D ");
            builder.Append(" where MonthPlanID='" + MonthPlanID + "' ");
            builder.Append(" Order by  OrderID ASC ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetPlanDetailList(string MonthPlanID, string type)
        {
            StringBuilder builder = new StringBuilder();
            if (type == "income")
            {
                builder.Append("SELECT dbo.Fund_Plan_MonthDetail.*, Con_Incomet_Contract.Party as BName, dbo.cont_sum_income.contMoney+ISNULL(modfiy.ChangePrice,0) contMoney, dbo.cont_sum_income.PaymentMoney, ");
                builder.Append("dbo.cont_sum_income.BalanceMoney ");
                builder.Append(" FROM  dbo.cont_sum_income ");
                builder.Append(" LEFT JOIN Con_Incomet_Contract ON dbo.Cont_Sum_Income.Contractid=Con_Incomet_Contract.Contractid");
                builder.Append(" RIGHT OUTER JOIN ");
                builder.Append(" dbo.Fund_Plan_MonthDetail ON  dbo.cont_sum_income.ContractID = dbo.Fund_Plan_MonthDetail.ContractID ");
                builder.Append("  LEFT JOIN (SELECT SUM(ChangePrice) as ChangePrice,ContractID from Con_Incomet_Modify group by ContractID)");
                builder.Append("  AS modfiy ON modfiy.ContractID=dbo.cont_sum_income.ContractID");
                builder.Append(" where MonthPlanID='" + MonthPlanID + "' ");
                builder.Append(" Order by  OrderID ASC ");
            }
            else
            {
                builder.Append("SELECT dbo.Fund_Plan_MonthDetail.*, ");
                builder.Append("  xbcc.CorpName AS BName, ");
                builder.Append("dbo.cont_sum_paid.contMoney, ");
                builder.Append("dbo.cont_sum_paid.PaymentMoney,  ");
                builder.Append("dbo.cont_sum_paid.BalanceMoney  ");
                builder.Append("FROM         dbo.cont_sum_paid INNER JOIN ");
                builder.Append("dbo.Fund_Plan_MonthDetail ON ");
                builder.Append("dbo.cont_sum_paid.ContractID =dbo.Fund_Plan_MonthDetail.ContractID  ");
                builder.Append(" LEFT JOIN XPM_Basic_ContactCorp xbcc ON BName=CAST(CorpId AS NVARCHAR(1024))   ");
                builder.Append(" where MonthPlanID='" + MonthPlanID + "' ");
                builder.Append(" Order by  OrderID ASC ");
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable getSelectPlan(string Prjguid, string type)
        {
            string sqlString = "select * from dbo.V_Fund_SelectPlan where FlowState=1 and PlanYear=year(getdate()) and planMonth=Month(getdate()) and Prjguid='" + Prjguid + "' and planType='" + type + "'";
            DataTable table = new DataTable();
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable getTableByPlan(string[] planUid)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select * from dbo.V_Fund_SelectPlan where UID IN({0})", DBHelper.GetInParameterSql(planUid));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public DataTable getTableByPlanid(string planUid)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("select * from dbo.V_Fund_SelectPlan where UID ='{0}'", planUid);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public bool IsExitFundPlanMainInfo(string PlanType, string MonthPlanID, string PrjId)
        {
            string cmdText = string.Format(" SELECT COUNT(*) FROM Fund_Plan_MonthMain WHERE PlanType='{0}' AND PrjGuid='{1}' \r\n                                             AND MonthPlanID='{2}'", PlanType, PrjId, MonthPlanID);
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, cmdText, null);
            return ((obj2 != null) && (Convert.ToInt32(obj2) > 0));
        }

        public bool updateMainInfo(Fund_Plan_MonthMainInfo MI)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Begin  ");
            builder.Append("UPDATE [Fund_Plan_MonthMain]   SET  [Remark] = '" + MI.Remark + "' ,OperateTime=getdate(),OperatorCode='" + MI.OperatorCode + "' ");
            builder.Append(" WHERE MonthPlanID ='" + MI.MonthPlanID + "' ; ");
            builder.Append("End ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }
    }
}

