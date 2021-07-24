namespace cn.justwin.Fund.Account
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class AccountService
    {
        public int Add(AccounModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Fund_Prj_Accoun(");
            builder.Append("AccountID,PrjGuid,accountNum,acountName,");
            builder.Append("creatDate,createMan,authorizer,Remark,initialFund,");
            builder.Append("AccountState,FlowState");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("@AccountID,@PrjGuid,@accountNum,");
            builder.Append("@acountName,@creatDate,@createMan,");
            builder.Append("@authorizer,@Remark,@initialFund, '0', '-1'");
            builder.Append(")");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@AccountID", model.AccountID), new SqlParameter("@PrjGuid", model.PrjGuid), new SqlParameter("@accountNum", model.accountNum), new SqlParameter("@acountName", model.acountName), new SqlParameter("@creatDate", model.creatDate), new SqlParameter("@createMan", model.createMan), new SqlParameter("@authorizer", model.authorizer), new SqlParameter("@Remark", model.Remark), new SqlParameter("@initialFund", model.initialFund) };
            object obj2 = SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (obj2 == null)
            {
                return 0;
            }
            return Convert.ToInt32(obj2);
        }

        public bool Delete(string accountID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_Prj_Accoun ");
            builder.Append(" where AccountID=@accountID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@accountID", accountID) };
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool DeleteList(string idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_Prj_Accoun ");
            builder.Append(" where id AccountID (" + idlist + ")  ");
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null) > 0);
        }

        public bool Exists(string accountCode, Guid accountID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from Fund_Prj_Accoun");
            builder.Append(" where accountNum not in (select accountNum from Fund_Prj_Accoun where accountid = @AccountID ) and accountNum=@accountNum");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@AccountID", accountID), new SqlParameter("@accountNum", accountCode) };
            return (SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters).Rows.Count > 0);
        }

        public DataTable getByList(string strwhere)
        {
            string str = "(SELECT SUM(ISNULL(Amount,0)) AS qtqk FROM Bud_IndirectDiaryDetails AS D  LEFT JOIN Bud_IndirectDiaryCost AS M ON D.InDiaryId=M.InDiaryId   WHERE account.PrjGuid like '%'+CONVERT(VARCHAR(50),ProjectId)+'%' AND FlowState='1') as QTQK";
            string str2 = "select *,  (isnull(initialFund,0)+jiekuan-lixi-qingkuan-isnull(QTQK,0)+shouruzhang) as AccountBalance  from (select account.*,(SELECT  isnull(sum(LoanFund),0) FROM  [Fund_Prj_Loan] where  FlowState =1  and DATEDIFF(d,getdate(),planReturnDate)>0  and account.PrjGuid like '%'+CONVERT(VARCHAR(50),PrjGuid)+'%') as jiekuan, (SELECT  isnull(sum(LoanFund*loanrate/100),0) FROM  [Fund_Prj_Loan] where  FlowState =1  and DATEDIFF(d,getdate(),planReturnDate)<=0  and account.PrjGuid like '%'+CONVERT(VARCHAR(50),PrjGuid)+'%') as lixi,     (select isnull(sum(d.RPayMoney),0.00) from  Fund_RequestPay_Detail as d left join dbo.Fund_RequestPay_Main as m on m.RPayMainID=d.RPayMainID where account.PrjGuid like '%'+CONVERT(VARCHAR(50),m.PrjGuid)+'%' and m.FlowState=1) as qingkuan,  " + str + " ,(select isnull(sum(GetMoney),0.00) from Fund_Prj_Accoun_Income where account.PrjGuid like '%'+CONVERT(VARCHAR(50),PrjGuid)+'%'  and FlowState=1) as shouru, (select isnull(sum(INMoney),0.00) from Fund_Prj_Accoun_Income where account.PrjGuid like '%'+CONVERT(VARCHAR(50),PrjGuid)+'%' and FlowState=1) as shouruzhang  from Fund_Prj_Accoun as account) as account where 1=1 ";
            if ((strwhere != null) && (strwhere != ""))
            {
                str2 = str2 + strwhere;
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, str2.ToString(), null);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select AccountID,PrjGuid,accountNum,");
            builder.Append("acountName,creatDate, ");
            builder.Append("(SELECT top 1 [v_xm]   FROM [PT_yhmc] where [v_yhdm]=fpa.createMan) AS createMan ");
            builder.Append(",activeDate,");
            builder.Append("(SELECT top 1 [v_xm]   FROM [PT_yhmc] where [v_yhdm]=fpa.activeMan) AS activeMan ");
            builder.Append(",authorizer,");
            builder.Append("closeDate,");
            builder.Append("(SELECT top 1 [v_xm]   FROM [PT_yhmc] where [v_yhdm]=fpa.closeMan) AS closeMan");
            builder.Append(",Remark,initialFund,");
            builder.Append("AccountState,FlowState,moneyRate,IncomeFund,PayoutFund,CurrentFund ");
            builder.Append(" FROM Fund_Prj_Accoun fpa ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where 1=1 " + strWhere);
            }
            builder.Append(" order by creatDate desc ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public AccounModel GetModel(string AccountID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 AccountID,PrjGuid,accountNum,acountName");
            builder.Append(",creatDate,createMan,activeDate,activeMan,authorizer,");
            builder.Append("closeDate,closeMan,Remark,initialFund,AccountState,");
            builder.Append("FlowState,moneyRate,IncomeFund,PayoutFund,CurrentFund from Fund_Prj_Accoun ");
            builder.Append(" where AccountID=@AccountID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@AccountID", AccountID) };
            AccounModel model = new AccounModel();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if ((table.Rows[0]["AccountID"] != null) && (table.Rows[0]["AccountID"].ToString() != ""))
            {
                model.AccountID = new Guid(table.Rows[0]["AccountID"].ToString());
            }
            if ((table.Rows[0]["PrjGuid"] != null) && (table.Rows[0]["PrjGuid"].ToString() != ""))
            {
                model.PrjGuid = table.Rows[0]["PrjGuid"].ToString();
            }
            if ((table.Rows[0]["accountNum"] != null) && (table.Rows[0]["accountNum"].ToString() != ""))
            {
                model.accountNum = table.Rows[0]["accountNum"].ToString();
            }
            if ((table.Rows[0]["acountName"] != null) && (table.Rows[0]["acountName"].ToString() != ""))
            {
                model.acountName = table.Rows[0]["acountName"].ToString();
            }
            if ((table.Rows[0]["creatDate"] != null) && (table.Rows[0]["creatDate"].ToString() != ""))
            {
                model.creatDate = new DateTime?(DateTime.Parse(table.Rows[0]["creatDate"].ToString()));
            }
            if ((table.Rows[0]["createMan"] != null) && (table.Rows[0]["createMan"].ToString() != ""))
            {
                model.createMan = table.Rows[0]["createMan"].ToString();
            }
            if ((table.Rows[0]["activeDate"] != null) && (table.Rows[0]["activeDate"].ToString() != ""))
            {
                model.activeDate = new DateTime?(DateTime.Parse(table.Rows[0]["activeDate"].ToString()));
            }
            if ((table.Rows[0]["activeMan"] != null) && (table.Rows[0]["activeMan"].ToString() != ""))
            {
                model.activeMan = table.Rows[0]["activeMan"].ToString();
            }
            if ((table.Rows[0]["authorizer"] != null) && (table.Rows[0]["authorizer"].ToString() != ""))
            {
                model.authorizer = table.Rows[0]["authorizer"].ToString();
            }
            if ((table.Rows[0]["closeDate"] != null) && (table.Rows[0]["closeDate"].ToString() != ""))
            {
                model.closeDate = new DateTime?(DateTime.Parse(table.Rows[0]["closeDate"].ToString()));
            }
            if ((table.Rows[0]["closeMan"] != null) && (table.Rows[0]["closeMan"].ToString() != ""))
            {
                model.closeMan = table.Rows[0]["closeMan"].ToString();
            }
            if ((table.Rows[0]["Remark"] != null) && (table.Rows[0]["Remark"].ToString() != ""))
            {
                model.Remark = table.Rows[0]["Remark"].ToString();
            }
            if ((table.Rows[0]["initialFund"] != null) && (table.Rows[0]["initialFund"].ToString() != ""))
            {
                model.initialFund = new decimal?(decimal.Parse(table.Rows[0]["initialFund"].ToString()));
            }
            if ((table.Rows[0]["AccountState"] != null) && (table.Rows[0]["AccountState"].ToString() != ""))
            {
                model.AccountState = new int?(int.Parse(table.Rows[0]["AccountState"].ToString()));
            }
            if ((table.Rows[0]["FlowState"] != null) && (table.Rows[0]["FlowState"].ToString() != ""))
            {
                model.FlowState = new int?(int.Parse(table.Rows[0]["FlowState"].ToString()));
            }
            if ((table.Rows[0]["moneyRate"] != null) && (table.Rows[0]["moneyRate"].ToString() != ""))
            {
                model.moneyRate = new decimal?(decimal.Parse(table.Rows[0]["moneyRate"].ToString()));
            }
            if ((table.Rows[0]["IncomeFund"] != null) && (table.Rows[0]["IncomeFund"].ToString() != ""))
            {
                model.IncomeFund = new decimal?(decimal.Parse(table.Rows[0]["IncomeFund"].ToString()));
            }
            if ((table.Rows[0]["PayoutFund"] != null) && (table.Rows[0]["PayoutFund"].ToString() != ""))
            {
                model.PayoutFund = new decimal?(decimal.Parse(table.Rows[0]["PayoutFund"].ToString()));
            }
            if ((table.Rows[0]["CurrentFund"] != null) && (table.Rows[0]["CurrentFund"].ToString() != ""))
            {
                model.CurrentFund = new decimal?(decimal.Parse(table.Rows[0]["CurrentFund"].ToString()));
            }
            return model;
        }

        public string GetUserNameByUserCode(string code)
        {
            if (code != "")
            {
                string cmdText = string.Format("SELECT top 1 [v_xm]   FROM [PT_yhmc] where [v_yhdm]='{0}'", code);
                return SqlHelper.ExecuteScalar(CommandType.Text, cmdText, null).ToString();
            }
            return "";
        }

        public bool Update(AccounModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Fund_Prj_Accoun set ");
            builder.Append("PrjGuid=@PrjGuid,");
            builder.Append("accountNum=@accountNum,");
            builder.Append("acountName=@acountName,");
            builder.Append("Remark=@Remark,");
            builder.Append("initialFund=@initialFund ");
            builder.Append(" where AccountID=@AccountID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@PrjGuid", model.PrjGuid), new SqlParameter("@accountNum", model.accountNum), new SqlParameter("@acountName", model.acountName), new SqlParameter("@Remark", model.Remark), new SqlParameter("@initialFund", model.initialFund), new SqlParameter("@AccountID", model.AccountID) };
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Update(string _authorizer, string _AccountID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Fund_Prj_Accoun set ");
            builder.Append("authorizer=@authorizer ");
            builder.Append(" where AccountID=@AccountID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@authorizer", _authorizer), new SqlParameter("@AccountID", _AccountID) };
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool UpdateisActivity(ActivityModel model, SqlTransaction trans)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Fund_Prj_Accoun set ");
            builder.Append("AccountState=@AccountState,");
            builder.Append("activeDate=@activeDate,");
            builder.Append("activeMan=@activeMan,");
            builder.Append("closeDate=@closeDate,");
            builder.Append("closeMan=@closeMan");
            builder.Append(" where AccountID=@accountID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@AccountState", SqlDbType.Int, 4), new SqlParameter("@activeDate", SqlDbType.DateTime), new SqlParameter("@activeMan", model.activeMan), new SqlParameter("@closeDate", SqlDbType.DateTime), new SqlParameter("@closeMan", model.closeMan), new SqlParameter("@accountID", model.AccountID) };
            commandParameters[0].Value = model.AccountState;
            if (!model.activeDate.HasValue)
            {
                commandParameters[1].Value = DBNull.Value;
            }
            else
            {
                commandParameters[1].Value = model.activeDate;
            }
            if (!model.closeDate.HasValue)
            {
                commandParameters[3].Value = DBNull.Value;
            }
            else
            {
                commandParameters[3].Value = model.closeDate;
            }
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
    }
}

