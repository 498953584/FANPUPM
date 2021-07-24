namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class receiveGoodsBLL
    {
        private sm_receiveGoods receiveGood = new sm_receiveGoods();

        public int Add(SqlTransaction trans, receiveGoods receiveGoodsModel)
        {
            return this.receiveGood.Add(trans, receiveGoodsModel);
        }

        public int Delete(SqlTransaction trans, string rnID)
        {
            return this.receiveGood.Delete(trans, rnID);
        }

        public DataTable getResourceByRnid(string[] rnid)
        {
            return this.receiveGood.GetResourceBySnids(rnid);
        }
    }
}

