namespace cn.justwin.Fund.AccounPayout.Dal
{
    using cn.justwin.DAL;
    using cn.justwin.Fund.AccounPayout.Model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class AccounPayoutDAL
    {
        public bool Add(AccounPayoutModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Fund_Prj_Accoun_Payout(");
            builder.Append("PayOutGuid,PayOutCode,prjGuid,RPGuid,PayOutMoney,PayOutPeople,PayOutTime,Handler,FloeState,Remark,UpdateUser,UpdateTime,Type)");
            builder.Append(" values (");
            builder.Append("@PayOutGuid,@PayOutCode,@prjGuid,@RPGuid,@PayOutMoney,@PayOutPeople,@PayOutTime,@Handler,@FloeState,@Remark,@UpdateUser,@UpdateTime,@Type)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PayOutGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PayOutCode", SqlDbType.VarChar, 100), new SqlParameter("@prjGuid", SqlDbType.VarChar, 100), new SqlParameter("@RPGuid", SqlDbType.VarChar, 100), new SqlParameter("@PayOutMoney", SqlDbType.Decimal, 9), new SqlParameter("@PayOutPeople", SqlDbType.VarChar, 100), new SqlParameter("@PayOutTime", SqlDbType.DateTime), new SqlParameter("@Handler", SqlDbType.VarChar, 100), new SqlParameter("@FloeState", SqlDbType.Int, 4), new SqlParameter("@Remark", SqlDbType.VarChar), new SqlParameter("@UpdateUser", SqlDbType.VarChar, 100), new SqlParameter("@UpdateTime", SqlDbType.DateTime), new SqlParameter("@Type", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.PayOutGuid;
            commandParameters[1].Value = model.PayOutCode;
            commandParameters[2].Value = model.prjGuid;
            commandParameters[3].Value = model.RPGuid;
            commandParameters[4].Value = model.PayOutMoney;
            commandParameters[5].Value = model.PayOutPeople;
            commandParameters[6].Value = model.PayOutTime;
            commandParameters[7].Value = model.Handler;
            commandParameters[8].Value = model.FloeState;
            commandParameters[9].Value = model.Remark;
            commandParameters[10].Value = model.UpdateUser;
            commandParameters[11].Value = model.UpdateTime;
            commandParameters[12].Value = model.Type;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Delete(Guid PayOutGuid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_Prj_Accoun_Payout ");
            builder.Append(" where PayOutGuid=@PayOutGuid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PayOutGuid", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = PayOutGuid;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Exists(string PayOutCode, Guid PayOutGuid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Fund_Prj_Accoun_Payout");
            builder.Append(" where PayOutCode not in (select PayOutCode from Fund_Prj_Accoun_Payout where PayOutGuid = @PayOutGuid ) and PayOutCode=@PayOutCode");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PayOutGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PayOutCode", SqlDbType.VarChar, 100) };
            commandParameters[0].Value = PayOutGuid;
            commandParameters[1].Value = PayOutCode;
            return (SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters).Rows.Count > 0);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * ");
            builder.Append(" FROM Fund_Prj_Accoun_Payout ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" *,(select PaymentCode from Con_Payout_Payment where ID=RpGuid) as PaymentCode,(select Name from  Bud_IndirectDiaryCost where InDiaryid=RpGuid) as BudName ,ContractName ");
            builder.Append(" FROM Fund_Prj_Accoun_Payout ");
            builder.Append(" left join   Con_Payout_Payment on Fund_Prj_Accoun_Payout.RPGuid=Con_Payout_Payment.ID ");
            builder.Append(" left join   Con_Payout_Contract on Con_Payout_Payment.ContractID=Con_Payout_Contract.ContractID ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where 1=1 " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public AccounPayoutModel GetModel(Guid PayOutGuid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 *,ContractName from Fund_Prj_Accoun_Payout ");
            builder.Append(" left join   Con_Payout_Payment on Fund_Prj_Accoun_Payout.RPGuid=Con_Payout_Payment.ID ");
            builder.Append(" left join   Con_Payout_Contract on Con_Payout_Payment.ContractID=Con_Payout_Contract.ContractID ");
            builder.Append(" where PayOutGuid=@PayOutGuid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PayOutGuid", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = PayOutGuid;
            AccounPayoutModel model = new AccounPayoutModel();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if (table.Rows[0]["PayOutGuid"].ToString() != "")
            {
                model.PayOutGuid = new Guid(table.Rows[0]["PayOutGuid"].ToString());
            }
            model.PayOutCode = table.Rows[0]["PayOutCode"].ToString();
            model.prjGuid = table.Rows[0]["prjGuid"].ToString();
            model.RPGuid = table.Rows[0]["RPGuid"].ToString();
            if (table.Rows[0]["PayOutMoney"].ToString() != "")
            {
                model.PayOutMoney = new decimal?(decimal.Parse(table.Rows[0]["PayOutMoney"].ToString()));
            }
            model.PayOutPeople = table.Rows[0]["PayOutPeople"].ToString();
            if (table.Rows[0]["PayOutTime"].ToString() != "")
            {
                model.PayOutTime = new DateTime?(DateTime.Parse(table.Rows[0]["PayOutTime"].ToString()));
            }
            model.Handler = table.Rows[0]["Handler"].ToString();
            if (table.Rows[0]["FloeState"].ToString() != "")
            {
                model.FloeState = new int?(int.Parse(table.Rows[0]["FloeState"].ToString()));
            }
            model.Remark = table.Rows[0]["Remark"].ToString();
            model.UpdateUser = table.Rows[0]["UpdateUser"].ToString();
            if (table.Rows[0]["UpdateTime"].ToString() != "")
            {
                model.UpdateTime = new DateTime?(DateTime.Parse(table.Rows[0]["UpdateTime"].ToString()));
            }
            if (table.Rows[0]["Type"].ToString() != "")
            {
                model.Type = new int?(int.Parse(table.Rows[0]["Type"].ToString()));
            }
            model.ContractName = table.Rows[0]["ContractName"].ToString();
            return model;
        }

        public decimal getMoneyByPayCode(string PayCode)
        {
            decimal num = 0.00M;
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, ("select sum(isnull(PayOutMoney,0.00)) as PayMoney  from  Fund_Prj_Accoun_Payout where FloeState=1 and RPGuid='" + PayCode + "' group by RPGuid").ToString(), null);
            if (table.Rows.Count > 0)
            {
                num = Convert.ToDecimal(table.Rows[0][0].ToString());
            }
            return num;
        }

        public DataTable GetPaymentMoney(string paymentId)
        {
            string str = "\r\n                --DECLARE @paymentId NVARCHAR(50);\r\n                --SET @paymentId='aa30f1d4-d6ed-49b6-b772-a5331a8059a7';\r\n                WITH PayoutContract AS\r\n                (\r\n\t                SELECT Id,ModifiedMoney,payoutContract.ContractId FROM Con_Payout_Payment Payment\r\n\t                INNER JOIN Con_Payout_Contract payoutContract ON Payment.ContractId=payoutContract.ContractId\r\n\t                WHERE ID=@paymentId\r\n                )\r\n                SELECT PayoutContract.ContractId,ModifiedMoney,SUM(ISNULL(PayoutMoney,0)) payoutMoney  FROM  Con_Payout_Payment payment \r\n                INNER JOIN PayoutContract ON payment.ContractId=PayoutContract.ContractId AND payment.FlowState=1 \r\n                LEFT JOIN Fund_Prj_Accoun_Payout ON payment.Id=RPGuid\r\n                WHERE Type=0 AND FloeState=1\r\n                GROUP BY PayoutContract.ContractId,ModifiedMoney\r\n            ";
            SqlParameter parameter = new SqlParameter("@paymentId", paymentId);
            return SqlHelper.ExecuteQuery(CommandType.Text, str.ToString(), new SqlParameter[] { parameter });
        }

        public DataTable getReportByWhere(string Typestring, string strwhere)
        {
            string str = " select *,(PayMoney-AccountMoney) as Money,CAST(CAST(ROUND(ISNULL(AccountMoney/NULLIF(PayMoney,0)*100,0),2) AS DECIMAL(18,2))AS VARCHAR(15))+'%' as BL from (";
            if (Typestring == "1")
            {
                str = (str + " select payTab.indiaryid as ID,payTab.ProjectId as PrjGuid, (select prjName from Pt_prjinfo where PrjGuid=payTab.ProjectId)as PrjName, PayTab.Name as PayCode," + " PayTab.InputDate as PayTime,( select isnull(sum(de.Amount),0) from dbo.Bud_IndirectDiaryDetails as de where de.InDiaryId=payTab.InDiaryId) as PayMoney,") + "(select isnull(sum(ac.PayOutMoney),0) from Fund_Prj_Accoun_Payout as ac where ac.RPGuid=payTab.InDiaryid and ac.FloeState=1) as AccountMoney" + " from  Bud_IndirectDiaryCost as payTab where CostType='P' AND PayTab.FlowState=1 ";
            }
            else
            {
                str = (str + " select pay.ID as ID,con.PrjGuid as PrjGuid, (select prjName from Pt_prjinfo where PrjGuid=con.PrjGuid)as PrjName," + "pay.PaymentCode as PayCode,Pay.InputDate as PayTime,Pay.PaymentMoney as PayMoney,") + "(select isnull(sum(ac.PayOutMoney),0) from Fund_Prj_Accoun_Payout as ac where ac.RPGuid=pay.ID and ac.FloeState=1) as AccountMoney" + " from Con_Payout_Payment as Pay left join dbo.Con_Payout_Contract as con on pay.ContractId=con.ContractId where pay.FlowState=1";
            }
            str = str + ") as tab where 1=1 " + strwhere;
            return SqlHelper.ExecuteQuery(CommandType.Text, str.ToString(), null);
        }

        public bool Update(AccounPayoutModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Fund_Prj_Accoun_Payout set ");
            builder.Append("PayOutCode=@PayOutCode,");
            builder.Append("prjGuid=@prjGuid,");
            builder.Append("RPGuid=@RPGuid,");
            builder.Append("PayOutMoney=@PayOutMoney,");
            builder.Append("PayOutPeople=@PayOutPeople,");
            builder.Append("PayOutTime=@PayOutTime,");
            builder.Append("Handler=@Handler,");
            builder.Append("Remark=@Remark,");
            builder.Append("UpdateUser=@UpdateUser,");
            builder.Append("UpdateTime=@UpdateTime");
            builder.Append(" where PayOutGuid=@PayOutGuid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PayOutGuid", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@PayOutCode", SqlDbType.VarChar, 100), new SqlParameter("@prjGuid", SqlDbType.VarChar, 100), new SqlParameter("@RPGuid", SqlDbType.VarChar, 100), new SqlParameter("@PayOutMoney", SqlDbType.Decimal, 9), new SqlParameter("@PayOutPeople", SqlDbType.VarChar, 100), new SqlParameter("@PayOutTime", SqlDbType.DateTime), new SqlParameter("@Handler", SqlDbType.VarChar, 100), new SqlParameter("@Remark", SqlDbType.VarChar), new SqlParameter("@UpdateUser", SqlDbType.VarChar, 100), new SqlParameter("@UpdateTime", SqlDbType.DateTime) };
            commandParameters[0].Value = model.PayOutGuid;
            commandParameters[1].Value = model.PayOutCode;
            commandParameters[2].Value = model.prjGuid;
            commandParameters[3].Value = model.RPGuid;
            commandParameters[4].Value = model.PayOutMoney;
            commandParameters[5].Value = model.PayOutPeople;
            commandParameters[6].Value = model.PayOutTime;
            commandParameters[7].Value = model.Handler;
            commandParameters[8].Value = model.Remark;
            commandParameters[9].Value = model.UpdateUser;
            commandParameters[10].Value = model.UpdateTime;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }
    }
}

