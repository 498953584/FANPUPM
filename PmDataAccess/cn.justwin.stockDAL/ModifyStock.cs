namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class ModifyStock
    {
        public int Add(SqlTransaction trans, ModifyStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_Modify_Stock(");
            builder.Append("ModifyStockId,ModifyId,PurchaseId,Scode,Pscode,Sprice,Quantity,corp,ArrivalDate)");
            builder.Append(" values (");
            builder.Append("@Id,@ModifyId,@PurchaseId,@Scode,@Pscode,@Sprice,@Quantity,@Corp,@ArrivalDate)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.NVarChar, 70), new SqlParameter("@ModifyId", SqlDbType.NVarChar, 0x40), new SqlParameter("@PurchaseId", SqlDbType.NVarChar, 70), new SqlParameter("@Scode", SqlDbType.NVarChar, 50), new SqlParameter("@Pscode", SqlDbType.NVarChar, 0x40), new SqlParameter("@Sprice", SqlDbType.Decimal), new SqlParameter("@Quantity", SqlDbType.Decimal), new SqlParameter("@Corp", SqlDbType.NVarChar, 0x40), new SqlParameter("@ArrivalDate", SqlDbType.DateTime) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.ModifyId;
            commandParameters[2].Value = model.PurchaseId;
            commandParameters[3].Value = model.Scode;
            commandParameters[4].Value = model.Pscode;
            commandParameters[5].Value = model.Sprice;
            commandParameters[6].Value = model.Quantity;
            commandParameters[7].Value = model.Corp;
            if (!model.ArrivalDate.HasValue)
            {
                commandParameters[8].Value = DBNull.Value;
            }
            else
            {
                commandParameters[8].Value = DBHelper.GetDateTime(model.ArrivalDate.Value);
            }
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public void Delete(SqlTransaction trans, List<string> ids)
        {
            foreach (string str in ids)
            {
                this.Delete(trans, str);
            }
        }

        private int Delete(SqlTransaction trans, string id)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_Modify_Stock ");
            builder.Append(" where ModifyStockId=@id ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = id;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int DeleteByModifyId(SqlTransaction trans, string modifyId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_Modify_Stock ");
            builder.Append(" where ModifyId=@ModifyId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ModifyId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = modifyId;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable GetInfoByModifyId(string modifyId)
        {
            string cmdText = string.Format("SELECT * FROM Con_Modify_Stock WHERE ModifyId='{0}'", modifyId);
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public DataTable GetModelById(string id)
        {
            string cmdText = string.Format("SELECT * FROM Con_Modify_Stock WHERE ModifyStockId='{0}'", id);
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public void Update(SqlTransaction trans, ModifyStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update Con_Modify_Stock ");
            builder.Append("SET Sprice=@Sprice,Quantity=@Quantity,corp=@Corp");
            builder.Append(" WHERE ModifyStockId=@Id ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@Id", SqlDbType.NVarChar, 70), new SqlParameter("@Sprice", SqlDbType.Decimal), new SqlParameter("@Quantity", SqlDbType.Decimal), new SqlParameter("@Corp", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.Sprice;
            commandParameters[2].Value = model.Quantity;
            commandParameters[3].Value = model.Corp;
            if (trans == null)
            {
                SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            else
            {
                SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
            }
        }
    }
}

