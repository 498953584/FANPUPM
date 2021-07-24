namespace AccountManage.DAL
{
    using AccountManage.Model;
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class fund_accountInfoAccess
    {
        public int Add(fund_accountInfoModle model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into fund_accountInfo(");
            builder.Append("accountNum,opMoney,opType,opTime,opMan,isValid,sysPeop,opClass)");
            builder.Append(" values (");
            builder.Append("@accountNum,@opMoney,@opType,@opTime,@opMan,@isValid,@sysPeop,@opClass)");
            builder.Append(";select @@IDENTITY");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@accountNum", SqlDbType.NVarChar, 50), new SqlParameter("@opMoney", SqlDbType.Decimal, 9), new SqlParameter("@opType", SqlDbType.Int, 4), new SqlParameter("@opTime", SqlDbType.DateTime), new SqlParameter("@opMan", SqlDbType.NVarChar, 50), new SqlParameter("@isValid", SqlDbType.Int, 4), new SqlParameter("@sysPeop", SqlDbType.NVarChar, 50), new SqlParameter("@opClass", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.accountNum;
            commandParameters[1].Value = model.opMoney;
            commandParameters[2].Value = model.opType;
            commandParameters[3].Value = model.opTime;
            commandParameters[4].Value = model.opMan;
            commandParameters[5].Value = model.isValid;
            commandParameters[6].Value = model.sysPeop;
            commandParameters[7].Value = model.opClass;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
        }

        public bool Delete(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from fund_accountInfo ");
            builder.Append(" where id=@id");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            commandParameters[0].Value = id;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }

        public bool Exists(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from fund_accountInfo");
            builder.Append(" where id=@id ");
            SqlParameter[] parameterArray = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            parameterArray[0].Value = id;
            return (SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null).Rows.Count > 0);
        }

        public DataTable GetDistinct(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select distinct accountNum ");
            builder.Append(" FROM fund_accountInfo ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where 1=1 " + strWhere);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetDistinctNumber(string usercode, string andwhere)
        {
            DataTable table = new DataTable();
            string strWhere = string.Empty;
            if ((usercode != "") && (usercode != null))
            {
                strWhere = "and  accountNum in (select accountID from fund_Prjaccount where authorizer like '%" + usercode + "%') " + andwhere;
                return this.GetDistinct(strWhere);
            }
            return this.GetDistinct(andwhere);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select id,accountNum,opMoney,opType,opTime,opMan,isValid,sysPeop,opClass ");
            builder.Append(" FROM fund_accountInfo ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where 1=1 " + strWhere);
            }
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString() + " order by opTime asc", null);
        }

        public fund_accountInfoModle GetModel(int id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1 id,accountNum,opMoney,opType,opTime,opMan,isValid,sysPeop,opClass from fund_accountInfo ");
            builder.Append(" where id=@id");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.Int, 4) };
            commandParameters[0].Value = id;
            fund_accountInfoModle modle = new fund_accountInfoModle();
            DataTable table = SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), commandParameters);
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if (table.Rows[0]["id"].ToString() != "")
            {
                modle.id = int.Parse(table.Rows[0]["id"].ToString());
            }
            modle.accountNum = table.Rows[0]["accountNum"].ToString();
            if (table.Rows[0]["opMoney"].ToString() != "")
            {
                modle.opMoney = new decimal?(decimal.Parse(table.Rows[0]["opMoney"].ToString()));
            }
            if (table.Rows[0]["opType"].ToString() != "")
            {
                modle.opType = new int?(int.Parse(table.Rows[0]["opType"].ToString()));
            }
            if (table.Rows[0]["opTime"].ToString() != "")
            {
                modle.opTime = new DateTime?(DateTime.Parse(table.Rows[0]["opTime"].ToString()));
            }
            modle.opMan = table.Rows[0]["opMan"].ToString();
            if (table.Rows[0]["isValid"].ToString() != "")
            {
                modle.isValid = new int?(int.Parse(table.Rows[0]["isValid"].ToString()));
            }
            modle.sysPeop = table.Rows[0]["sysPeop"].ToString();
            if (table.Rows[0]["opClass"].ToString() != "")
            {
                modle.opClass = new int?(int.Parse(table.Rows[0]["opClass"].ToString()));
            }
            return modle;
        }

        public string GetMonye(string accouid)
        {
            DataTable list = this.GetList(" and accountNum='" + accouid + "'");
            decimal num = 0M;
            if (list.Rows.Count <= 0)
            {
                return "0.000";
            }
            for (int i = 0; i < list.Rows.Count; i++)
            {
                if (list.Rows[i]["opClass"].ToString() == "0")
                {
                    num += Convert.ToDecimal(list.Rows[i]["opMoney"].ToString());
                }
                else
                {
                    num -= Convert.ToDecimal(list.Rows[i]["opMoney"].ToString());
                }
            }
            return num.ToString();
        }

        public DataTable GetNumberTable(string usercode, string andwhere)
        {
            DataTable table = new DataTable();
            string strWhere = string.Empty;
            if ((usercode != "") && (usercode != null))
            {
                strWhere = "and  accountNum in (select accountNum from fund_Prjaccount where authorizer like '%" + usercode + "%') " + andwhere;
                return this.GetList(strWhere);
            }
            return this.GetList(andwhere);
        }

        public bool Update(fund_accountInfoModle model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update fund_accountInfo set ");
            builder.Append("accountNum=@accountNum,");
            builder.Append("opMoney=@opMoney,");
            builder.Append("opType=@opType,");
            builder.Append("opTime=@opTime,");
            builder.Append("opMan=@opMan,");
            builder.Append("isValid=@isValid,");
            builder.Append("sysPeop=@sysPeop,");
            builder.Append("opClass=@opClass");
            builder.Append(" where id=@id");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@accountNum", SqlDbType.NVarChar, 50), new SqlParameter("@opMoney", SqlDbType.Decimal, 9), new SqlParameter("@opType", SqlDbType.Int, 4), new SqlParameter("@opTime", SqlDbType.DateTime), new SqlParameter("@opMan", SqlDbType.NVarChar, 50), new SqlParameter("@isValid", SqlDbType.Int, 4), new SqlParameter("@sysPeop", SqlDbType.NVarChar, 50), new SqlParameter("@opClass", SqlDbType.Int, 4), new SqlParameter("@id", SqlDbType.Int, 4) };
            commandParameters[0].Value = model.accountNum;
            commandParameters[1].Value = model.opMoney;
            commandParameters[2].Value = model.opType;
            commandParameters[3].Value = model.opTime;
            commandParameters[4].Value = model.opMan;
            commandParameters[5].Value = model.isValid;
            commandParameters[6].Value = model.sysPeop;
            commandParameters[7].Value = model.opClass;
            commandParameters[8].Value = model.id;
            return (SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters) > 0);
        }
    }
}

