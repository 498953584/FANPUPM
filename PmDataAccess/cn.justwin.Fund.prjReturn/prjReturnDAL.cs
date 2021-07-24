namespace cn.justwin.Fund.prjReturn
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class prjReturnDAL
    {
        public int Add(SqlTransaction trans, PrjReturnModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Fund_Prj_Loan_Return(");
            builder.Append("FR_id,LoanID,FR_name,FR_data,FR_Money,FR_interest,FR_deduct,FR_remark,FR_flowState,FR_user,FR_Time,FR_Code)");
            builder.Append(" values (");
            builder.Append("@FR_id,@LoanID,@FR_name,@FR_data,@FR_Money,@FR_interest,@FR_deduct,@FR_remark,@FR_flowState,@FR_user,@FR_Time,@FR_Code)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@FR_id", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@LoanID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@FR_name", SqlDbType.VarChar, 8), new SqlParameter("@FR_data", SqlDbType.DateTime), new SqlParameter("@FR_Money", SqlDbType.Decimal, 9), new SqlParameter("@FR_interest", SqlDbType.Decimal, 9), new SqlParameter("@FR_deduct", SqlDbType.Decimal, 9), new SqlParameter("@FR_remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@FR_flowState", SqlDbType.Int, 4), new SqlParameter("@FR_user", SqlDbType.VarChar, 8), new SqlParameter("@FR_Time", SqlDbType.DateTime), new SqlParameter("@FR_Code", SqlDbType.NChar, 14) };
            commandParameters[0].Value = model.FR_id;
            commandParameters[1].Value = model.LoanID;
            commandParameters[2].Value = model.FR_name;
            commandParameters[3].Value = model.FR_data;
            commandParameters[4].Value = model.FR_Money;
            commandParameters[5].Value = model.FR_interest;
            commandParameters[6].Value = model.FR_deduct;
            commandParameters[7].Value = model.FR_remark;
            commandParameters[8].Value = model.FR_flowState;
            commandParameters[9].Value = model.FR_user;
            commandParameters[10].Value = model.FR_Time;
            commandParameters[11].Value = model.FR_Code;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, Guid FR_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_Prj_Loan_Return ");
            builder.Append(" where FR_id=@FR_id ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@FR_id", SqlDbType.UniqueIdentifier) };
            commandParameters[0].Value = FR_id;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteList(string FR_idlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Fund_Prj_Loan_Return ");
            builder.Append(" where FR_id in (" + FR_idlist + ")  ");
            return DBHelper.ExecuteNoQuery(builder.ToString());
        }

        public DataTable GetInfoByWhe(string strWhere)
        {
            string cmdText = "select r.*,(select v_xm from pt_yhmc where v_yhdm=r.fr_name) as runPersonnel ,(SELECT fpl.PrjGuid FROM Fund_Prj_Loan fpl WHERE fpl.LoanID=r.LoanID) AS PrjGuid  from Fund_Prj_Loan_Return as r ";
            if (strWhere.Trim() != "")
            {
                cmdText = cmdText + " where " + strWhere;
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public DataTable GetInfoByWhe(string strWhere, bool isvall)
        {
            string cmdText = "select r.*,(select v_xm from pt_yhmc where v_yhdm=r.fr_name) as runPersonnel , fpl.PrjGuid AS PrjGuid  from Fund_Prj_Loan_Return as r,Fund_Prj_Loan fpl ";
            if (strWhere.Trim() != "")
            {
                cmdText = cmdText + " where fpl.LoanID=r.LoanID and " + strWhere;
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public List<PrjReturnModel> GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select FR_id,LoanID,FR_name,FR_data,FR_Money,FR_interest,FR_deduct,FR_remark,FR_flowState,FR_user,FR_Time,FR_Code ");
            builder.Append(" FROM Fund_Prj_Loan_Return ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            List<PrjReturnModel> list = new List<PrjReturnModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public DataTable GetLoadInfo(string userCode, string sql, string AccounID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select l.*,p.prjCode,p.prjName,");
            builder.Append(" (CONVERT(varchar(20),convert(decimal(18,2),(l.LoanRate*100)))+'%')as rate,");
            builder.Append(" (select top(1) v_xm from pt_yhmc where pt_yhmc.v_yhdm=l.LoanMan) as v_xm,");
            builder.Append(" (select top(1) accountNum from Fund_Prj_Accoun as f where f.prjGuid like '%'+CONVERT(VARCHAR(40),p.prjGuid)+'%') as accountNum,");
            builder.Append(" (select top(1) acountName from Fund_Prj_Accoun as f where f.prjGuid like '%'+CONVERT(VARCHAR(40),p.prjGuid)+'%')as acountName");
            builder.Append(" from dbo.Fund_Prj_Loan as l left join pt_prjInfo as p on l.prjGuid=p.prjGuid");
            builder.Append(" where FlowState=1  ");
            if (AccounID != "")
            {
                builder.Append(" and (select prjGuid as AprjGuid from Fund_Prj_Accoun where accountid='" + AccounID + "') like '%'+CONVERT(VARCHAR(40),p.prjGuid)+'%'");
            }
            if (sql != "")
            {
                builder.Append(sql);
            }
            builder.Append(" order by LoanDate desc");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetLoanReturnInfo(string selWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select a.acountName,p.prjName,L.LoanID,L.LoanCode,L.LoanDate,L.LoanFund,case when L.ReturnState=0 then '未还清' when L.ReturnState=1 then '已还清' else '未还清' end as LoanReturnState,");
            builder.Append(" (select sum(FR_Money) from dbo.Fund_Prj_Loan_Return as r where r.LoanID=L.LoanID and r.FR_flowState=1 )as returnMoney,");
            builder.Append(" (select sum(FR_interest) from dbo.Fund_Prj_Loan_Return as r where r.LoanID=L.LoanID and r.FR_flowState=1 )as returnInterest,");
            builder.Append(" (select sum(FR_deduct) from dbo.Fund_Prj_Loan_Return as r where r.LoanID=L.LoanID and r.FR_flowState=1 )as returnDeduct,");
            builder.Append(" L.Remark");
            builder.Append(" from Fund_Prj_Loan as L Left join pt_prjinfo as p on L.prjGuid=p.prjGuid");
            builder.Append(" left join Fund_Prj_Accoun as a on a.prjGuid like '%'+CONVERT(VARCHAR(40),L.prjGuid)+'%'");
            builder.Append(" where L.FlowState=1");
            if (selWhere != "")
            {
                builder.Append(selWhere);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public PrjReturnModel GetModel(Guid FR_id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 FR_id,LoanID,FR_name,FR_data,FR_Money,FR_interest,FR_deduct,FR_remark,FR_flowState,FR_user,FR_Time,FR_Code from Fund_Prj_Loan_Return ");
            builder.Append(" where FR_id=@FR_id ");
            PrjReturnModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@FR_id", FR_id) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public DataTable GetSumByLoanid(Guid id)
        {
            string cmdText = "select isnull(sum(FR_Money),0) as FR_Money,isnull(sum(FR_interest),0) as Interest,isnull(sum(FR_deduct),0) as Deduct from dbo.Fund_Prj_Loan_Return where LoanID='" + id + "' and fr_flowState=1 ";
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public PrjReturnModel ReaderBind(IDataReader dataReader)
        {
            PrjReturnModel model = new PrjReturnModel();
            if (dataReader["FR_Code"].ToString() != "")
            {
                model.FR_Code = dataReader["FR_Code"].ToString();
            }
            if (dataReader["FR_id"].ToString() != "")
            {
                model.FR_id = new Guid(dataReader["FR_id"].ToString());
            }
            if (dataReader["LoanID"].ToString() != "")
            {
                model.LoanID = new Guid(dataReader["LoanID"].ToString());
            }
            model.FR_name = dataReader["FR_name"].ToString();
            if (dataReader["FR_data"].ToString() != "")
            {
                model.FR_data = new DateTime?(DateTime.Parse(dataReader["FR_data"].ToString()));
            }
            if (dataReader["FR_Money"].ToString() != "")
            {
                model.FR_Money = new decimal?(decimal.Parse(dataReader["FR_Money"].ToString()));
            }
            if (dataReader["FR_interest"].ToString() != "")
            {
                model.FR_interest = new decimal?(decimal.Parse(dataReader["FR_interest"].ToString()));
            }
            if (dataReader["FR_deduct"].ToString() != "")
            {
                model.FR_deduct = new decimal?(decimal.Parse(dataReader["FR_deduct"].ToString()));
            }
            model.FR_remark = dataReader["FR_remark"].ToString();
            if (dataReader["FR_flowState"].ToString() != "")
            {
                model.FR_flowState = new int?(int.Parse(dataReader["FR_flowState"].ToString()));
            }
            model.FR_user = dataReader["FR_user"].ToString();
            if (dataReader["FR_Time"].ToString() != "")
            {
                model.FR_Time = new DateTime?(DateTime.Parse(dataReader["FR_Time"].ToString()));
            }
            return model;
        }

        public int Update(SqlTransaction trans, PrjReturnModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Fund_Prj_Loan_Return set ");
            builder.Append("LoanID=@LoanID,");
            builder.Append("FR_name=@FR_name,");
            builder.Append("FR_data=@FR_data,");
            builder.Append("FR_Money=@FR_Money,");
            builder.Append("FR_interest=@FR_interest,");
            builder.Append("FR_deduct=@FR_deduct,");
            builder.Append("FR_remark=@FR_remark,");
            builder.Append("FR_flowState=@FR_flowState,");
            builder.Append("FR_user=@FR_user,");
            builder.Append("FR_Time=@FR_Time,");
            builder.Append("FR_Code=@FR_Code");
            builder.Append(" where FR_id=@FR_id ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@LoanID", SqlDbType.UniqueIdentifier, 0x10), new SqlParameter("@FR_name", SqlDbType.VarChar, 8), new SqlParameter("@FR_data", SqlDbType.DateTime), new SqlParameter("@FR_Money", SqlDbType.Decimal, 9), new SqlParameter("@FR_interest", SqlDbType.Decimal, 9), new SqlParameter("@FR_deduct", SqlDbType.Decimal, 9), new SqlParameter("@FR_remark", SqlDbType.VarChar, 0x3e8), new SqlParameter("@FR_flowState", SqlDbType.Int, 4), new SqlParameter("@FR_user", SqlDbType.VarChar, 8), new SqlParameter("@FR_Time", SqlDbType.DateTime), new SqlParameter("@FR_Code", SqlDbType.NChar, 14), new SqlParameter("@FR_id", SqlDbType.UniqueIdentifier, 0x10) };
            commandParameters[0].Value = model.LoanID;
            commandParameters[1].Value = model.FR_name;
            commandParameters[2].Value = model.FR_data;
            commandParameters[3].Value = model.FR_Money;
            commandParameters[4].Value = model.FR_interest;
            commandParameters[5].Value = model.FR_deduct;
            commandParameters[6].Value = model.FR_remark;
            commandParameters[7].Value = model.FR_flowState;
            commandParameters[8].Value = model.FR_user;
            commandParameters[9].Value = model.FR_Time;
            commandParameters[10].Value = model.FR_Code;
            commandParameters[11].Value = model.FR_id;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

