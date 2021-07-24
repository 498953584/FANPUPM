namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class sendGoodsBLL
    {
        private SendGoods sendgoods = new SendGoods();

        public void Add(SqlTransaction trans, SendGoodsModel modelGoods)
        {
            this.sendgoods.Add(trans, modelGoods);
        }

        public void DeleteBysnId(SqlTransaction trans, string snId)
        {
            this.sendgoods.Delete(trans, snId);
        }

        public DataTable GetResourceBypncode(string[] snId)
        {
            return this.sendgoods.GetResourceBySnids(snId);
        }
    }
}

