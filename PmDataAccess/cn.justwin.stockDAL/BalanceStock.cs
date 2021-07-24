namespace cn.justwin.stockDAL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class BalanceStock
    {
        public int Add(SqlTransaction trans, BalanceStockModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_Balance_Stock(");
            builder.Append("BalanceStockId,BalanceId,PurchaseId,ArrivaledQuantity)");
            builder.Append(" values (");
            builder.Append("@id,@BalanceId,@PurchaseId,@ArrivaledQuantity)");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@id", SqlDbType.NVarChar, 70), new SqlParameter("@BalanceId", SqlDbType.NVarChar, 0x40), new SqlParameter("@PurchaseId", SqlDbType.NVarChar, 70), new SqlParameter("@ArrivaledQuantity", SqlDbType.Decimal) };
            commandParameters[0].Value = model.Id;
            commandParameters[1].Value = model.BalanceId;
            commandParameters[2].Value = model.PurchaseId;
            commandParameters[3].Value = model.ArrivaledQuantity;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int Delete(SqlTransaction trans, string balanceId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_Balance_Stock ");
            builder.Append(" where BalanceId=@BalanceId ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@BalanceId", SqlDbType.NVarChar, 0x40) };
            commandParameters[0].Value = balanceId;
            if (trans == null)
            {
                return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), commandParameters);
            }
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public DataTable GetInfoByPurchaseId(string purchaseId)
        {
            string cmdText = string.Format("SELECT * FROM Con_Balance_Stock WHERE PurchaseId='{0}'", purchaseId);
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }
    }
}

