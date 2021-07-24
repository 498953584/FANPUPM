namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class sm_receiveGoods
    {
        public int Add(SqlTransaction trans, receiveGoods model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into sm_receiveGoods(");
            builder.Append("sgId,scode,rnID,number,price,suppyCode)");
            builder.Append(" values (");
            builder.Append("@sgId,@scode,@rnID,@number,@price,@suppyCode)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@sgId", SqlDbType.NVarChar, 0x40), new SqlParameter("@scode", SqlDbType.NVarChar, 0x40), new SqlParameter("@rnID", SqlDbType.NVarChar, 0x40), new SqlParameter("@number", SqlDbType.Decimal, 9), new SqlParameter("@price", SqlDbType.Decimal, 9), new SqlParameter("@suppyCode", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = model.sgId;
            commandParameters[1].Value = model.scode;
            commandParameters[2].Value = model.rnID;
            commandParameters[3].Value = model.number;
            commandParameters[4].Value = model.price;
            commandParameters[5].Value = model.suppyCode;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string rnid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from sm_receiveGoods ");
            builder.Append(" where rnid=@rnid ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@rnid", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = rnid;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteList(SqlTransaction trans, string sgIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from sm_receiveGoods ");
            builder.Append(" where sgId in (" + sgIdlist + ")  ");
            return DBHelper.ExecuteNoQuery(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select sgId,scode,rnID,number,price,suppyCode ");
            builder.Append(" FROM sm_receiveGoods ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DBHelper.GetTable(builder.ToString());
        }

        public receiveGoods GetModel(string sgId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select sgId,scode,rnID,number,price,suppyCode from sm_receiveGoods ");
            builder.Append(" where sgId=@sgId ");
            receiveGoods goods = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@sgId", sgId) }))
            {
                if (reader.Read())
                {
                    goods = this.ReaderBind(reader);
                }
            }
            return goods;
        }

        public DataTable GetResourceBySnids(string[] rnid)
        {
            string str = string.Empty;
            if (rnid.Length > 0)
            {
                StringBuilder builder = new StringBuilder();
                foreach (string str2 in rnid)
                {
                    builder.Append("','").Append(str2);
                }
                builder.Append("'");
                str = builder.ToString().Substring(2);
            }
            else
            {
                str = "''";
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.Append("select scode,Res_Resource.ResourceId, Res_Resource.ResourceCode ,ResourceName,Specification,Name, number, ").Append("suppyCode ,c.CorpName,price ").Append(",Res_Resource.Brand,ModelNumber,TechnicalParameter ").Append("from sm_receiveGoods ").Append("inner join Res_Resource on sm_receiveGoods.scode = Res_Resource.ResourceCode ").Append("left join Res_Unit on Res_Unit.UnitID = Res_Resource.Unit ").Append("left join XPM_Basic_ContactCorp c on sm_receiveGoods.suppyCode=c.CorpID ").Append("where rnid in (").Append(str).Append(") ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder2.ToString(), null);
        }

        public receiveGoods ReaderBind(IDataReader dataReader)
        {
            receiveGoods goods = new receiveGoods {
                sgId = dataReader["sgId"].ToString(),
                scode = dataReader["scode"].ToString(),
                rnID = dataReader["rnID"].ToString()
            };
            if (dataReader["number"].ToString() != "")
            {
                goods.number = new decimal?(decimal.Parse(dataReader["number"].ToString()));
            }
            if (dataReader["price"].ToString() != "")
            {
                goods.price = new decimal?(decimal.Parse(dataReader["price"].ToString()));
            }
            goods.suppyCode = dataReader["suppyCode"].ToString();
            return goods;
        }

        public int Update(SqlTransaction trans, receiveGoods model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update sm_receiveGoods set ");
            builder.Append("scode=@scode,");
            builder.Append("rnID=@rnID,");
            builder.Append("number=@number,");
            builder.Append("price=@price,");
            builder.Append("suppyCode=@suppyCode");
            builder.Append(" where sgId=@sgId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@scode", SqlDbType.NVarChar, 0x40), new SqlParameter("@rnID", SqlDbType.NVarChar, 0x40), new SqlParameter("@number", SqlDbType.Decimal, 9), new SqlParameter("@price", SqlDbType.Decimal, 9), new SqlParameter("@suppyCode", SqlDbType.NVarChar, 50), new SqlParameter("@sgId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.scode;
            commandParameters[1].Value = model.rnID;
            commandParameters[2].Value = model.number;
            commandParameters[3].Value = model.price;
            commandParameters[4].Value = model.suppyCode;
            commandParameters[5].Value = model.sgId;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

