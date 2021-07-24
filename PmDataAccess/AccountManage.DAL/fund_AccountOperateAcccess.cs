namespace AccountManage.DAL
{
    using AccountManage.Model;
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class fund_AccountOperateAcccess
    {
        public int Add(fund_AccountOperateModle model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into fund_AccountOperate(");
            builder.Append("AccountNum,Acredence,AccounType,AccountMony,RealMony,DepID,SumitMan,SumiTime,IsAccount,AccounTime,AccountMan,projnum,contracnum,AccountMark)");
            builder.Append(" values (");
            builder.Append("@AccountNum,@Acredence,@AccounType,@AccountMony,@RealMony,@DepID,@SumitMan,@SumiTime,@IsAccount,@AccounTime,@AccountMan,@projnum,@contracnum,@AccountMark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@AccountNum", SqlDbType.NVarChar, 50), new SqlParameter("@Acredence", SqlDbType.NVarChar, 50), new SqlParameter("@AccounType", SqlDbType.Int, 4), new SqlParameter("@AccountMony", SqlDbType.Decimal, 9), new SqlParameter("@RealMony", SqlDbType.Decimal, 9), new SqlParameter("@DepID", SqlDbType.NVarChar, 50), new SqlParameter("@SumitMan", SqlDbType.NVarChar, 50), new SqlParameter("@SumiTime", SqlDbType.DateTime), new SqlParameter("@IsAccount", SqlDbType.Int, 4), new SqlParameter("@AccounTime", SqlDbType.DateTime), new SqlParameter("@AccountMan", SqlDbType.NVarChar, 50), new SqlParameter("@projnum", SqlDbType.NVarChar, 50), new SqlParameter("@contracnum", SqlDbType.NVarChar, 50), new SqlParameter("@AccountMark", SqlDbType.Text) };
            commandParameters[0].Value = model.AccountNum;
            commandParameters[1].Value = model.Acredence;
            commandParameters[2].Value = model.AccounType;
            commandParameters[3].Value = model.AccountMony;
            commandParameters[4].Value = model.RealMony;
            commandParameters[5].Value = model.DepID;
            commandParameters[6].Value = model.SumitMan;
            commandParameters[7].Value = model.SumiTime;
            commandParameters[8].Value = model.IsAccount;
            commandParameters[9].Value = DBNull.Value;
            commandParameters[10].Value = model.AccountMan;
            commandParameters[11].Value = model.projnum;
            commandParameters[12].Value = model.contracnum;
            commandParameters[13].Value = model.AccountMark;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int AddList(SqlTransaction trans, fund_AccountOperateModle model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into fund_AccountOperate(");
            builder.Append("AccountNum,Acredence,AccounType,AccountMony,RealMony,DepID,SumitMan,SumiTime,IsAccount,AccounTime,AccountMan,projnum,contracnum,AccountMark)");
            builder.Append(" values (");
            builder.Append("@AccountNum,@Acredence,@AccounType,@AccountMony,@RealMony,@DepID,@SumitMan,@SumiTime,@IsAccount,@AccounTime,@AccountMan,@projnum,@contracnum,@AccountMark)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@AccountNum", SqlDbType.NVarChar, 50), new SqlParameter("@Acredence", SqlDbType.NVarChar, 50), new SqlParameter("@AccounType", SqlDbType.Int, 4), new SqlParameter("@AccountMony", SqlDbType.Decimal, 9), new SqlParameter("@RealMony", SqlDbType.Decimal, 9), new SqlParameter("@DepID", SqlDbType.NVarChar, 50), new SqlParameter("@SumitMan", SqlDbType.NVarChar, 50), new SqlParameter("@SumiTime", SqlDbType.DateTime), new SqlParameter("@IsAccount", SqlDbType.Int, 4), new SqlParameter("@AccounTime", SqlDbType.DateTime), new SqlParameter("@AccountMan", SqlDbType.NVarChar, 50), new SqlParameter("@projnum", SqlDbType.NVarChar, 50), new SqlParameter("@contracnum", SqlDbType.NVarChar, 50), new SqlParameter("@AccountMark", SqlDbType.Text) };
            commandParameters[0].Value = model.AccountNum;
            commandParameters[1].Value = model.Acredence;
            commandParameters[2].Value = model.AccounType;
            commandParameters[3].Value = model.AccountMony;
            commandParameters[4].Value = model.RealMony;
            commandParameters[5].Value = model.DepID;
            commandParameters[6].Value = model.SumitMan;
            commandParameters[7].Value = model.SumiTime;
            commandParameters[8].Value = model.IsAccount;
            commandParameters[9].Value = DBNull.Value;
            commandParameters[10].Value = model.AccountMan;
            commandParameters[11].Value = model.projnum;
            commandParameters[12].Value = model.contracnum;
            commandParameters[13].Value = model.AccountMark;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public bool Delete(int AoID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from fund_AccountOperate ");
            builder.Append(" where AoID=@AoID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@AoID", SqlDbType.Int, 4) };
            commandParameters[0].Value = AoID;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Exists(string Acredence)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from fund_AccountOperate");
            builder.Append(" where Acredence=@Acredence ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Acredence", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = Acredence;
            return (SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters).Rows.Count > 0);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select AoID,AccountNum,Acredence,AccounType,AccountMony,RealMony,DepID,SumitMan,SumiTime,IsAccount,AccounTime,AccountMan,projnum,contracnum,AccountMark ");
            builder.Append(" FROM fund_AccountOperate ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString() + " order by SumiTime desc", null);
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" AoID,AccountNum,Acredence,AccounType,AccountMony,RealMony,DepID,SumitMan,SumiTime,IsAccount,AccounTime,AccountMan,projnum,contracnum,AccountMark ");
            builder.Append(" FROM fund_AccountOperate ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public fund_AccountOperateModle GetModel(int AoID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 AoID,AccountNum,Acredence,AccounType,AccountMony,RealMony,DepID,SumitMan,SumiTime,IsAccount,AccounTime,AccountMan,projnum,contracnum,AccountMark  from fund_AccountOperate ");
            builder.Append(" where AoID=@AoID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@AoID", SqlDbType.Int, 4) };
            commandParameters[0].Value = AoID;
            fund_AccountOperateModle modle = new fund_AccountOperateModle();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if (table.Rows[0]["AoID"].ToString() != "")
            {
                modle.AoID = int.Parse(table.Rows[0]["AoID"].ToString());
            }
            modle.AccountNum = table.Rows[0]["AccountNum"].ToString();
            modle.Acredence = table.Rows[0]["Acredence"].ToString();
            if (table.Rows[0]["AccounType"].ToString() != "")
            {
                modle.AccounType = new int?(int.Parse(table.Rows[0]["AccounType"].ToString()));
            }
            if (table.Rows[0]["AccountMony"].ToString() != "")
            {
                modle.AccountMony = new decimal?(decimal.Parse(table.Rows[0]["AccountMony"].ToString()));
            }
            if (table.Rows[0]["RealMony"].ToString() != "")
            {
                modle.RealMony = new decimal?(decimal.Parse(table.Rows[0]["RealMony"].ToString()));
            }
            modle.DepID = table.Rows[0]["DepID"].ToString();
            modle.SumitMan = table.Rows[0]["SumitMan"].ToString();
            if (table.Rows[0]["SumiTime"].ToString() != "")
            {
                modle.SumiTime = new DateTime?(DateTime.Parse(table.Rows[0]["SumiTime"].ToString()));
            }
            if (table.Rows[0]["IsAccount"].ToString() != "")
            {
                modle.IsAccount = new int?(int.Parse(table.Rows[0]["IsAccount"].ToString()));
            }
            if (table.Rows[0]["AccounTime"].ToString() != "")
            {
                modle.AccounTime = new DateTime?(DateTime.Parse(table.Rows[0]["AccounTime"].ToString()));
            }
            modle.AccountMan = table.Rows[0]["AccountMan"].ToString();
            modle.projnum = table.Rows[0]["projnum"].ToString();
            modle.contracnum = table.Rows[0]["contracnum"].ToString();
            modle.AccountMark = table.Rows[0]["AccountMark"].ToString();
            return modle;
        }

        public bool Update(fund_AccountOperateModle model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update fund_AccountOperate set ");
            builder.Append("AccountNum=@AccountNum,");
            builder.Append("Acredence=@Acredence,");
            builder.Append("AccounType=@AccounType,");
            builder.Append("AccountMony=@AccountMony,");
            builder.Append("RealMony=@RealMony,");
            builder.Append("DepID=@DepID,");
            builder.Append("SumitMan=@SumitMan,");
            builder.Append("SumiTime=@SumiTime,");
            builder.Append("IsAccount=@IsAccount,");
            builder.Append("AccountMan=@AccountMan,");
            builder.Append("projnum=@projnum,");
            builder.Append("contracnum=@contracnum,");
            builder.Append("AccountMark=@AccountMark");
            builder.Append(" where AoID=@AoID");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@AccountNum", SqlDbType.NVarChar, 50), new SqlParameter("@Acredence", SqlDbType.NVarChar, 50), new SqlParameter("@AccounType", SqlDbType.Int, 4), new SqlParameter("@AccountMony", SqlDbType.Decimal, 9), new SqlParameter("@RealMony", SqlDbType.Decimal, 9), new SqlParameter("@DepID", SqlDbType.NVarChar, 50), new SqlParameter("@SumitMan", SqlDbType.NVarChar, 50), new SqlParameter("@SumiTime", SqlDbType.DateTime), new SqlParameter("@IsAccount", SqlDbType.Int, 4), new SqlParameter("@AccountMan", SqlDbType.NVarChar, 50), new SqlParameter("@projnum", SqlDbType.NVarChar, 50), new SqlParameter("@contracnum", SqlDbType.NVarChar, 50), new SqlParameter("@AccountMark", SqlDbType.Text), new SqlParameter("@AoID", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.AccountNum;
            commandParameters[1].Value = model.Acredence;
            commandParameters[2].Value = model.AccounType;
            commandParameters[3].Value = model.AccountMony;
            commandParameters[4].Value = model.RealMony;
            commandParameters[5].Value = model.DepID;
            commandParameters[6].Value = model.SumitMan;
            commandParameters[7].Value = model.SumiTime;
            commandParameters[8].Value = model.IsAccount;
            commandParameters[9].Value = model.AccountMan;
            commandParameters[10].Value = model.projnum;
            commandParameters[11].Value = model.contracnum;
            commandParameters[12].Value = model.AccountMark;
            commandParameters[13].Value = model.AoID;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool UpdateBoth(string usercode, DateTime nowtime, decimal monye, int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update fund_AccountOperate set ");
            builder.Append("RealMony='" + monye + "',");
            builder.Append("IsAccount='" + 1 + "',");
            builder.Append("AccounTime='" + nowtime + "',");
            builder.Append("AccountMan='" + usercode + "'");
            builder.Append(" where AoID='" + id + "'");
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null) > 0);
        }
    }
}

