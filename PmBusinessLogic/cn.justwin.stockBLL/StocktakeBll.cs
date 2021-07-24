namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class StocktakeBll
    {
        private Stocktake stocktake = new Stocktake();

        public void Add(SqlTransaction trans, StocktakeModel model)
        {
            this.stocktake.Add(trans, model);
        }

        public DateTime GetAllocationDate(string treasuryCode)
        {
            return this.stocktake.GetAllocationDate(treasuryCode);
        }

        public StocktakeModel GetById(string id)
        {
            return this.stocktake.GetById(id);
        }

        public List<StocktakeModel> GetByTreasuryCode(string treasuryCode)
        {
            return this.stocktake.GetByTreasuryCode(treasuryCode);
        }

        public int GetCountByMoreStDate(string treasuryCode, string stocktakeDate)
        {
            return this.stocktake.GetCountByMoreStDate(treasuryCode, stocktakeDate);
        }

        public int GetCountByStDate(string treasuryCode, string stocktakeDate)
        {
            return this.stocktake.GetCountByStDate(treasuryCode, stocktakeDate);
        }

        public StocktakeModel GetEditModel(string treasuryCode)
        {
            return this.stocktake.GetEditModel(treasuryCode);
        }

        public DateTime GetInitializeDate(string treasuryCode)
        {
            return this.stocktake.GetInitializeDate(treasuryCode);
        }

        public StocktakeModel GetLastStocktakeModel(string treasuryCode)
        {
            return this.stocktake.GetLastStocktakeModel(treasuryCode);
        }

        public DateTime GetStorageDate(string treasuryCode)
        {
            return this.stocktake.GetStorageDate(treasuryCode);
        }

        public bool IsAdd(string treasuryCode)
        {
            return this.stocktake.IsAdd(treasuryCode);
        }

        public bool IsFirst(string treasuryCode)
        {
            return this.stocktake.IsFirst(treasuryCode);
        }

        public void LockStocktake(SqlTransaction trans, StocktakeModel model)
        {
            this.stocktake.LockStocktake(trans, model);
        }

        public void Update(SqlTransaction trans, StocktakeModel model)
        {
            this.stocktake.UpdateState(trans, model);
        }
    }
}

