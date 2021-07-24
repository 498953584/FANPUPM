namespace cn.justwin.AccountManage
{
    using cn.justwin.AccountManage.accBaise;
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class fund_baise
    {
        public int Add(basieModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into fund_basic(");
            builder.Append("accAllot,fundRatio,isRate,borrowRate,authority,startMoney)");
            builder.Append(" values (");
            builder.Append("@accAllot,@fundRatio,@isRate,@borrowRate,@authority,@startMoney)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@accAllot", SqlDbType.Int, 4), new SqlParameter("@fundRatio", SqlDbType.Decimal, 9), new SqlParameter("@isRate", SqlDbType.Int, 4), new SqlParameter("@borrowRate", SqlDbType.Decimal, 9), new SqlParameter("@authority", SqlDbType.Int, 4), new SqlParameter("@startMoney", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.accAllot;
            commandParameters[1].Value = model.fundRatio;
            commandParameters[2].Value = model.isRate;
            commandParameters[3].Value = model.borrowRate;
            commandParameters[4].Value = model.authority;
            commandParameters[5].Value = model.startMoney;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from fund_basic ");
            builder.Append(" where id=@id");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            commandParameters[0].Value = id;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public basieModel GetModel(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,accAllot,fundRatio,isRate,borrowRate,authority,startMoney from fund_basic ");
            builder.Append(" where id=@id");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            commandParameters[0].Value = id;
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            basieModel model = new basieModel();
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if (table.Rows[0]["id"].ToString() != "")
            {
                model.id = int.Parse(table.Rows[0]["id"].ToString());
            }
            if (table.Rows[0]["accAllot"].ToString() != "")
            {
                model.accAllot = new int?(int.Parse(table.Rows[0]["accAllot"].ToString()));
            }
            if (table.Rows[0]["fundRatio"].ToString() != "")
            {
                model.fundRatio = new decimal?(decimal.Parse(table.Rows[0]["fundRatio"].ToString()));
            }
            if (table.Rows[0]["isRate"].ToString() != "")
            {
                model.isRate = new int?(int.Parse(table.Rows[0]["isRate"].ToString()));
            }
            if (table.Rows[0]["borrowRate"].ToString() != "")
            {
                model.borrowRate = new decimal?(decimal.Parse(table.Rows[0]["borrowRate"].ToString()));
            }
            if (table.Rows[0]["authority"].ToString() != "")
            {
                model.authority = new int?(int.Parse(table.Rows[0]["authority"].ToString()));
            }
            if (table.Rows[0]["startMoney"].ToString() != "")
            {
                model.startMoney = new int?(int.Parse(table.Rows[0]["startMoney"].ToString()));
            }
            return model;
        }

        public int Update(basieModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update fund_basic set ");
            builder.Append("accAllot=@accAllot,");
            builder.Append("fundRatio=@fundRatio,");
            builder.Append("isRate=@isRate,");
            builder.Append("borrowRate=@borrowRate,");
            builder.Append("authority=@authority,");
            builder.Append("startMoney=@startMoney");
            builder.Append(" where id=@id");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@accAllot", SqlDbType.Int, 4), new SqlParameter("@fundRatio", SqlDbType.Decimal, 9), new SqlParameter("@isRate", SqlDbType.Int, 4), new SqlParameter("@borrowRate", SqlDbType.Decimal, 9), new SqlParameter("@authority", SqlDbType.Int, 4), new SqlParameter("@id", SqlDbType.Int, 4), new SqlParameter("@startMoney", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.accAllot;
            commandParameters[1].Value = model.fundRatio;
            commandParameters[2].Value = model.isRate;
            commandParameters[3].Value = model.borrowRate;
            commandParameters[4].Value = model.authority;
            commandParameters[5].Value = model.id;
            commandParameters[6].Value = model.startMoney;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

