namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class SmPurchaseplanStockBll
    {
        private SmPurchaseplanStock smPurchaseplanStock = new SmPurchaseplanStock();

        public int Add(SqlTransaction trans, SmPurchaseplanStockModel model)
        {
            return this.smPurchaseplanStock.Add(trans, model);
        }

        public int Delete(string wpsid)
        {
            return this.smPurchaseplanStock.Delete(wpsid);
        }

        public int DeleteByWhere(SqlTransaction trans, string strWhere)
        {
            return this.smPurchaseplanStock.DeleteByWhere(trans, strWhere);
        }

        public List<SmPurchaseplanStockModel> GetListArray(string strWhere)
        {
            return this.smPurchaseplanStock.GetListArray(strWhere);
        }

        public string GetMinArrivalDate(string resourceCode, string ppcodes)
        {
            return this.smPurchaseplanStock.GetMinArrivalDate(resourceCode, ppcodes);
        }

        public SmPurchaseplanStockModel GetModel(string wpsid)
        {
            return this.smPurchaseplanStock.GetModel(wpsid);
        }

        public DataTable GetResourceByPpcodes(string[] arrPpcode)
        {
            return this.smPurchaseplanStock.GetResourceByPpcodes(arrPpcode);
        }

        public DataTable GetResourceByPpcodes(string[] arrPpcode, string strPrjId, string isWBSRelevance)
        {
            return this.smPurchaseplanStock.GetResourceByPpcodes(arrPpcode, strPrjId, isWBSRelevance);
        }

        public DataTable GetResourceByPurchasePcodes(string[] ppcodes)
        {
            return this.smPurchaseplanStock.GetResourceByPurchasePcodes(ppcodes);
        }

        public DataTable GetTableByParam(string startTime, string endTime, string ppcode, string[] ResourceNames, string[] ResourceCodes, string prjName, string category, string specification, string brand, string modelNumber)
        {
            return this.smPurchaseplanStock.GetTableByParam(startTime, endTime, ppcode, ResourceNames, ResourceCodes, prjName, category, specification, brand, modelNumber);
        }

        public int Update(SqlTransaction trans, SmPurchaseplanStockModel model)
        {
            return this.smPurchaseplanStock.Update(trans, model);
        }
    }
}

