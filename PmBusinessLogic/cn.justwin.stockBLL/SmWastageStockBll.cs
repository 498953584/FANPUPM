namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class SmWastageStockBll
    {
        private SmWastageStock smWastageStock = new SmWastageStock();

        public int Add(SqlTransaction trans, SmWastageStockModel model)
        {
            return this.smWastageStock.Add(trans, model);
        }

        public int Delete(string wastageStockId)
        {
            return this.smWastageStock.Delete(wastageStockId);
        }

        public int DeleteByWhere(SqlTransaction trans, string strWhere)
        {
            return this.smWastageStock.DeleteByWhere(trans, strWhere);
        }

        public List<SmWastageStockModel> GetListArray(string strWhere)
        {
            return this.smWastageStock.GetListArray(strWhere);
        }

        public DataTable GetListByWastageCode(string wastageCode)
        {
            return this.smWastageStock.GetListByWastageCode(wastageCode);
        }

        public SmWastageStockModel GetModel(string wastageStockId)
        {
            return this.smWastageStock.GetModel(wastageStockId);
        }

        public SmWastageStockModel GetModelByWhere(string strWhere)
        {
            return this.smWastageStock.GetModelByWhere(strWhere);
        }

        public DataTable GetTableByParam(string startTime, string endTime, string wastageCode, string[] ResourceNames, string[] ResourceCodes, string corpName, string category)
        {
            return this.smWastageStock.GetTableByParam(startTime, endTime, wastageCode, ResourceNames, ResourceCodes, corpName, category);
        }

        public DataTable GetTableByWastageCode(string wastageCode, string tcode)
        {
            return this.smWastageStock.GetTableByWastageCode(wastageCode, tcode);
        }

        public int Update(SqlTransaction trans, SmWastageStockModel model)
        {
            return this.smWastageStock.Update(trans, model);
        }
    }
}

