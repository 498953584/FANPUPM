namespace cn.justwin.stockBLL
{
    using cn.justwin.DAL;
    using cn.justwin.stockModel;
    using PmBusinessLogic;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class StocktakeSelf : ISelfEvent
    {
        private readonly StocktakeBll stocktakeBll = new StocktakeBll();
        private readonly StocktakeDetailBll stocktakeDetailBll = new StocktakeDetailBll();
        private TreasuryStockBll treasuryStockBll = new TreasuryStockBll();

        public void CommitEvent(object key)
        {
            StocktakeModel byId = this.stocktakeBll.GetById(key.ToString());
            List<StocktakeDetailModel> byStocktakeId = this.stocktakeDetailBll.GetByStocktakeId(byId.Id);
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                foreach (StocktakeDetailModel model2 in byStocktakeId)
                {
                    decimal num = model2.StocktakeNum - model2.BookNum;
                    if (num < 0M)
                    {
                        num = model2.BookNum - model2.StocktakeNum;
                        List<TreasuryStockModel> listArray = this.treasuryStockBll.GetListArray(string.Concat(new object[] { " where scode='", model2.ResourceCode, "' and sprice=", model2.Price, " and corp='", model2.SupplierId, "' and tcode='", byId.TreasuryCode, "'  order by intime asc" }));
                        decimal num2 = 0M;
                        foreach (TreasuryStockModel model3 in listArray)
                        {
                            num2 += model3.snumber;
                        }
                        foreach (TreasuryStockModel model4 in listArray)
                        {
                            if (model4.snumber >= num)
                            {
                                model4.snumber -= num;
                                if (model4.snumber == 0M)
                                {
                                    this.stocktakeDetailBll.Delete(trans, model4.tsid);
                                }
                                else
                                {
                                    this.stocktakeDetailBll.Update(trans, model4);
                                }
                                break;
                            }
                            if (model4.snumber < num)
                            {
                                num -= model4.snumber;
                                this.stocktakeDetailBll.Delete(trans, model4.tsid);
                            }
                        }
                    }
                    else if (num > 0M)
                    {
                        TreasuryStockModel model = new TreasuryStockModel {
                            tsid = Guid.NewGuid().ToString(),
                            scode = model2.ResourceCode,
                            tcode = byId.TreasuryCode,
                            sprice = model2.Price,
                            snumber = num,
                            isfirst = false,
                            corp = model2.SupplierId,
                            incode = byId.Code,
                            intime = DateTime.Today,
                            intype = 0
                        };
                        this.stocktakeDetailBll.AddTreasuryStock(trans, model);
                    }
                }
                byId.LockDate = DateTime.Now;
                byId.State = 2;
                this.stocktakeBll.LockStocktake(trans, byId);
                trans.Commit();
            }
        }

        public void RefuseEvent(object primarykey)
        {
        }

        public void RestatedEvent(object key)
        {
        }

        public void SuperDelete(object key)
        {
        }
    }
}

