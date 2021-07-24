namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class OutStockBll
    {
        private OutStock outStock = new OutStock();

        public int Add(SqlTransaction trans, OutStockModel model)
        {
            return this.outStock.Add(trans, model);
        }

        public int Delete(string orsid)
        {
            return this.outStock.Delete(orsid);
        }

        public int DeleteByWhere(SqlTransaction trans, string strWhere)
        {
            return this.outStock.DeleteByWhere(trans, strWhere);
        }

        public List<OutStockModel> GetListArray(string strWhere)
        {
            return this.outStock.GetListArray(strWhere);
        }

        public DataTable GetListByOrcode(string orcode)
        {
            return this.outStock.GetListByOrcode(orcode);
        }

        public OutStockModel GetModel(string orsid)
        {
            return this.outStock.GetModel(orsid);
        }

        public OutStockModel GetModelByWhere(string strWhere)
        {
            return this.outStock.GetModelByWhere(strWhere);
        }

        public DataTable GetTableByOrcode(string orcode, string tcode)
        {
            return this.outStock.GetTableByOrcode(orcode, tcode);
        }

        public DataTable GetTableByParam(string startTime, string endTime, string orcode, string[] ResourceNames, string[] ResourceCodes, string prjName, string corpName, string category, string specification, string brand, string modelNumber)
        {
            return this.outStock.GetTableByParam(startTime, endTime, orcode, ResourceNames, ResourceCodes, prjName, corpName, category, specification, brand, modelNumber);
        }

        public int Update(SqlTransaction trans, OutStockModel model)
        {
            return this.outStock.Update(trans, model);
        }
    }
}

