namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class BackStockBll
    {
        private BackStock backStock = new BackStock();

        public int Add(SqlTransaction trans, BackStockModel model)
        {
            return this.backStock.Add(trans, model);
        }

        public int Delete(string rsid)
        {
            return this.backStock.Delete(rsid);
        }

        public int DeleteByWhere(SqlTransaction trans, string strWhere)
        {
            return this.backStock.DeleteByWhere(trans, strWhere);
        }

        public List<BackStockModel> GetListArray(string strWhere)
        {
            return this.backStock.GetListArray(strWhere);
        }

        public BackStockModel GetModel(string rsid)
        {
            return this.backStock.GetModel(rsid);
        }

        public string GetNumByParam(string ocorde, decimal sprice, string scode, string corp, string rcode, string flowstate)
        {
            return this.backStock.GetNumByParam(ocorde, sprice, scode, corp, rcode, flowstate);
        }

        public DataTable GetTableByParam(string startTime, string endTime, string rcode, string[] ResourceNames, string[] ResourceCodes, string prjName, string corpName, string category, string specification, string brand, string modelNumber)
        {
            return this.backStock.GetTableByParam(startTime, endTime, rcode, ResourceNames, ResourceCodes, prjName, corpName, category, specification, brand, modelNumber);
        }

        public DataTable GetTableByRcode(string rcode)
        {
            return this.backStock.GetTableByRcode(rcode);
        }

        public int Update(SqlTransaction trans, BackStockModel model)
        {
            return this.backStock.Update(trans, model);
        }
    }
}

