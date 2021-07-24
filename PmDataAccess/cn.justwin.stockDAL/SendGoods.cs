namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SendGoods
    {
        public int Add(SqlTransaction trans, SendGoodsModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into sm_sendGoods(");
            builder.Append("sgId,scode,snCode,number,suppyCode,price)");
            builder.Append(" values (");
            builder.Append("@sgId,@scode,@snCode,@number,@suppyCode,@price)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@sgId", SqlDbType.NVarChar, 0x40), new SqlParameter("@scode", SqlDbType.VarChar, 100), new SqlParameter("@snCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@number", SqlDbType.Decimal, 9), new SqlParameter("@suppyCode", SqlDbType.VarChar, 100), new SqlParameter("@price", SqlDbType.Decimal, 9) };
            commandParameters[0].Value = model.sgId;
            commandParameters[1].Value = model.scode;
            commandParameters[2].Value = model.snCode;
            commandParameters[3].Value = model.Number;
            commandParameters[4].Value = model.suppyCode;
            commandParameters[5].Value = model.Price;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string snId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from sm_sendGoods ");
            builder.Append(" where snCode=@snCode ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@snCode", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = snId;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteList(string sgIdlist)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from sm_sendGoods ");
            builder.Append(" where sgId in (" + sgIdlist + ")  ");
            return DBHelper.ExecuteNoQuery(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select sgId,scode,snCode,number,suppyCode,price ");
            builder.Append(" FROM sm_sendGoods ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return DBHelper.GetTable(builder.ToString());
        }

        public SendGoodsModel GetModel(string sgId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select sgId,scode,snCode,number,suppyCode,price from sm_sendGoods ");
            builder.Append(" where sgId=@sgId ");
            SendGoodsModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@sgId", sgId) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public DataTable GetResourceBySnids(string[] snId)
        {
            string str = string.Empty;
            if (snId.Length > 0)
            {
                StringBuilder builder = new StringBuilder();
                foreach (string str2 in snId)
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
            builder2.Append("select scode,Res_Resource.ResourceId, Res_Resource.ResourceCode ,ResourceName,Specification,Name, number, ").Append("suppyCode ,c.CorpName,price ").Append(",Res_Resource.Brand,ModelNumber,TechnicalParameter ").Append("from sm_sendGoods ").Append("inner join Res_Resource on sm_sendGoods.scode = Res_Resource.ResourceCode ").Append("left join Res_Unit on Res_Unit.UnitID = Res_Resource.Unit ").Append("left join XPM_Basic_ContactCorp c on sm_sendGoods.suppyCode=c.CorpID ").Append("where snCode in (").Append(str).Append(")  ORDER BY ResourceCode DESC");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder2.ToString(), null);
        }

        public SendGoodsModel ReaderBind(IDataReader dataReader)
        {
            SendGoodsModel model = new SendGoodsModel {
                sgId = dataReader["sgId"].ToString(),
                scode = dataReader["scode"].ToString(),
                snCode = dataReader["snCode"].ToString()
            };
            if (dataReader["number"].ToString() != "")
            {
                model.Number = new decimal?(decimal.Parse(dataReader["number"].ToString()));
            }
            model.suppyCode = dataReader["suppyCode"].ToString();
            if (dataReader["price"].ToString() != "")
            {
                model.Price = new decimal?(decimal.Parse(dataReader["price"].ToString()));
            }
            return model;
        }

        public int Update(SqlTransaction trans, SendGoodsModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update sm_sendGoods set ");
            builder.Append("scode=@scode,");
            builder.Append("snCode=@snCode,");
            builder.Append("number=@number,");
            builder.Append("suppyCode=@suppyCode,");
            builder.Append("price=@price");
            builder.Append(" where sgId=@sgId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@scode", SqlDbType.VarChar, 100), new SqlParameter("@snCode", SqlDbType.NVarChar, 0x40), new SqlParameter("@number", SqlDbType.Decimal, 9), new SqlParameter("@suppyCode", SqlDbType.VarChar, 100), new SqlParameter("@sgId", SqlDbType.NVarChar, 0x40), new SqlParameter("@price", SqlDbType.Decimal, 9) };
            commandParameters[0].Value = model.scode;
            commandParameters[1].Value = model.snCode;
            commandParameters[2].Value = model.Number;
            commandParameters[3].Value = model.suppyCode;
            commandParameters[4].Value = model.sgId;
            commandParameters[5].Value = model.Price;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }
    }
}

