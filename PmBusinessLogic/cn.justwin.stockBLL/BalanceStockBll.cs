namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class BalanceStockBll
    {
        private readonly BalanceStock dal = new BalanceStock();

        public void Add(BalanceStockModel model)
        {
            this.dal.Add(null, model);
        }

        public void Add(SqlTransaction trans, BalanceStockModel model)
        {
            this.dal.Add(trans, model);
        }

        public int Delete(SqlTransaction trans, string balanceId)
        {
            return this.dal.Delete(trans, balanceId);
        }

        public DataTable GetInfoByPurchaseId(string purchaseId)
        {
            return this.dal.GetInfoByPurchaseId(purchaseId);
        }
    }
}

