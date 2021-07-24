namespace cn.justwin.Fund.MonthPlan
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class MonthDetailService
    {
        public bool Add(MonthDetailModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Fund_Plan_MonthDetail(");
            builder.Append("UID,MonthPlanID,ContractID,Plansubject,PlanMoney,OldBalance,OrderID,ReMark)");
            builder.Append(" values (");
            builder.Append("@UID,@MonthPlanID,@ContractID,@Plansubject,@PlanMoney,@OldBalance,@OrderID,@ReMark)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@MonthPlanID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@Plansubject", SqlDbType.VarChar, 20), new SqlParameter("@PlanMoney", SqlDbType.Money, 8), new SqlParameter("@OldBalance", SqlDbType.Money, 8), new SqlParameter("@OrderID", SqlDbType.Int, 4), new SqlParameter("@ReMark", SqlDbType.NVarChar, 50), new SqlParameter("@ThisBalance", SqlDbType.Money, 8) };
            commandParameters[0].Value = model.UID;
            commandParameters[1].Value = model.MonthPlanID;
            commandParameters[2].Value = model.ContractID;
            commandParameters[3].Value = model.Plansubject;
            commandParameters[4].Value = model.PlanMoney;
            commandParameters[5].Value = model.OldBalance;
            commandParameters[6].Value = model.OrderID;
            commandParameters[7].Value = model.ReMark;
            commandParameters[8].Value = model.ThisBalance;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Delete(Guid UID, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_Plan_MonthDetail ");
            builder.Append(" where UID=@UID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UID", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = UID;
            int num = 0;
            if (trans == null)
            {
                num = SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                num = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
            return (num > 0);
        }

        public bool DeleteList(string UIDlist, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_Plan_MonthDetail ");
            builder.Append(" where UID in (" + UIDlist + ")  ");
            int num = 0;
            if (trans == null)
            {
                num = SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
            }
            else
            {
                num = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), null);
            }
            return (num > 0);
        }

        public bool Exists(Guid UID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from Fund_Plan_MonthDetail");
            builder.Append(" where UID=@UID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UID", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = UID;
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), commandParameters);
            return ((obj2 != null) && (obj2.ToString() != ""));
        }

        public DataTable GetList(string table, string id, string rowName)
        {
            string str = "select * from " + table + "  where " + rowName + " ='" + id + "'";
            return SqlHelper.ExecuteQuery(CommandType.Text, str.ToString(), null);
        }

        public MonthDetailModel GetModel(Guid UID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 UID,MonthPlanID,ContractID,Plansubject,PlanMoney,OldBalance,OrderID,ReMark,ThisBalance from Fund_Plan_MonthDetail ");
            builder.Append(" where UID=@UID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@UID", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = UID;
            MonthDetailModel model = new MonthDetailModel();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if ((table.Rows[0]["UID"] != null) && (table.Rows[0]["UID"].ToString() != ""))
            {
                model.UID = new Guid(table.Rows[0]["UID"].ToString());
            }
            if ((table.Rows[0]["MonthPlanID"] != null) && (table.Rows[0]["MonthPlanID"].ToString() != ""))
            {
                model.MonthPlanID = new Guid(table.Rows[0]["MonthPlanID"].ToString());
            }
            if ((table.Rows[0]["ContractID"] != null) && (table.Rows[0]["ContractID"].ToString() != ""))
            {
                model.ContractID = table.Rows[0]["ContractID"].ToString();
            }
            if ((table.Rows[0]["Plansubject"] != null) && (table.Rows[0]["Plansubject"].ToString() != ""))
            {
                model.Plansubject = table.Rows[0]["Plansubject"].ToString();
            }
            if ((table.Rows[0]["PlanMoney"] != null) && (table.Rows[0]["PlanMoney"].ToString() != ""))
            {
                model.PlanMoney = new decimal?(decimal.Parse(table.Rows[0]["PlanMoney"].ToString()));
            }
            if ((table.Rows[0]["OldBalance"] != null) && (table.Rows[0]["OldBalance"].ToString() != ""))
            {
                model.OldBalance = new decimal?(decimal.Parse(table.Rows[0]["OldBalance"].ToString()));
            }
            if ((table.Rows[0]["OrderID"] != null) && (table.Rows[0]["OrderID"].ToString() != ""))
            {
                model.OrderID = new int?(int.Parse(table.Rows[0]["OrderID"].ToString()));
            }
            if ((table.Rows[0]["ReMark"] != null) && (table.Rows[0]["ReMark"].ToString() != ""))
            {
                model.ReMark = table.Rows[0]["ReMark"].ToString();
            }
            if ((table.Rows[0]["ThisBalance"] != null) && (table.Rows[0]["ThisBalance"].ToString() != ""))
            {
                model.ThisBalance = new decimal?(decimal.Parse(table.Rows[0]["ThisBalance"].ToString()));
            }
            return model;
        }

        public DataTable GetPlanDetailobject(string plantype)
        {
            StringBuilder builder = new StringBuilder();
            if (plantype == "payout")
            {
                builder.Append("SELECT  [Name] as Plansubject FROM  [Bud_CostAccounting] where cbscode like '001001%'and len(cbscode)=9");
            }
            else
            {
                builder.Append("select  distinct   Plansubject  ");
                builder.Append(" FROM Fund_Plan_MonthDetail ");
                builder.Append(" where   Plansubject is not null  and  MonthPlanID  in (SELECT     [MonthPlanID] FROM [Fund_Plan_MonthMain] WHERE  [PlanType] like'%" + plantype + "%')");
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public bool Update(MonthDetailModel model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Fund_Plan_MonthDetail set ");
            builder.Append("MonthPlanID=@MonthPlanID,");
            builder.Append("ContractID=@ContractID,");
            builder.Append("Plansubject=@Plansubject,");
            builder.Append("PlanMoney=@PlanMoney,");
            builder.Append("OldBalance=@OldBalance,");
            builder.Append("OrderID=@OrderID,");
            builder.Append("ReMark=@ReMark");
            builder.Append(" where UID=@UID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@MonthPlanID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40), new SqlParameter("@Plansubject", SqlDbType.VarChar, 20), new SqlParameter("@PlanMoney", SqlDbType.Money, 8), new SqlParameter("@OldBalance", SqlDbType.Money, 8), new SqlParameter("@OrderID", SqlDbType.Int, 4), new SqlParameter("@ReMark", SqlDbType.NVarChar, 50), new SqlParameter("@UID", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = model.MonthPlanID;
            commandParameters[1].Value = model.ContractID;
            commandParameters[2].Value = model.Plansubject;
            commandParameters[3].Value = model.PlanMoney;
            commandParameters[4].Value = model.OldBalance;
            commandParameters[5].Value = model.OrderID;
            commandParameters[6].Value = model.ReMark;
            commandParameters[7].Value = model.UID;
            int num = 0;
            if (trans == null)
            {
                num = SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                num = SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
            return (num > 0);
        }

        public bool verificationIsPlansubject(string _MonthPlanID, string _Plansubject)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * FROM Fund_Plan_MonthDetail frpd ");
            builder.Append(" WHERE ");
            builder.Append(" frpd.MonthPlanID='").Append(_MonthPlanID).Append("'  ");
            builder.Append("  AND frpd.Plansubject='").Append(_Plansubject).Append("'");
            return (SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null).Rows.Count > 0);
        }

        public bool verificationIsPrjGuid(string _MonthPlanID, string _ContractID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT * FROM Fund_Plan_MonthDetail frpd ");
            builder.Append(" WHERE ");
            builder.Append(" frpd.MonthPlanID='").Append(_MonthPlanID).Append("'  ");
            if (!string.IsNullOrEmpty(_ContractID))
            {
                builder.Append("  AND frpd.ContractID='").Append(_ContractID).Append("'");
            }
            return (SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null).Rows.Count > 0);
        }
    }
}

