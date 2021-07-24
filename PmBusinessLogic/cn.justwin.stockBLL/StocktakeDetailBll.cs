namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class StocktakeDetailBll
    {
        private StocktakeDetail stocktakeDetail = new StocktakeDetail();

        public void Add(SqlTransaction trans, List<StocktakeDetailModel> listStocktakeDetailModel)
        {
            this.stocktakeDetail.Add(trans, listStocktakeDetailModel);
        }

        public void AddTreasuryStock(SqlTransaction trans, TreasuryStockModel model)
        {
            this.stocktakeDetail.AddTreasuryStock(trans, model);
        }

        public void Delete(SqlTransaction trans, string id)
        {
            this.stocktakeDetail.DeleteTreasuryStock(trans, id);
        }

        public List<StocktakeDetailModel> GetByStocktakeId(string StocktakeId)
        {
            return this.stocktakeDetail.GetByStocktakeId(StocktakeId);
        }

        public List<StocktakeDetailModel> GetByTreasuryCode(string treasuryCode, bool isFirst, DateTime endTime)
        {
            return this.stocktakeDetail.GetByTreasuryCode(treasuryCode, isFirst, endTime);
        }

        public void Update(SqlTransaction trans, TreasuryStockModel model)
        {
            this.stocktakeDetail.Update(trans, model);
        }
    }
}

