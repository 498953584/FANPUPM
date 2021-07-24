namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class PurchaseStock
    {
        private readonly cn.justwin.stockDAL.PurchaseStock dal = new cn.justwin.stockDAL.PurchaseStock();

        public void Add(PurchaseStockModel model)
        {
            this.dal.Add(null, model);
        }

        public void Add(List<PurchaseStockModel> lst)
        {
            foreach (PurchaseStockModel model in lst)
            {
                this.dal.Add(null, model);
            }
        }

        public void Add(SqlTransaction trans, PurchaseStockModel model)
        {
            this.dal.Add(trans, model);
        }

        public void Add(SqlTransaction trans, List<PurchaseStockModel> lst)
        {
            foreach (PurchaseStockModel model in lst)
            {
                this.dal.Add(trans, model);
            }
        }

        public void Delete(string psid)
        {
            this.dal.Delete(psid);
        }

        public void Delete(SqlTransaction trans, string psid)
        {
            this.dal.Delete(trans, psid);
        }

        public void Delete(SqlTransaction trans, List<string> psids)
        {
            this.dal.Delete(trans, psids);
        }

        public int DeleteByPscode(string pscode)
        {
            return this.dal.DeleteByPscode(null, pscode);
        }

        public int DeleteByPscode(SqlTransaction trans, string pscode)
        {
            return this.dal.DeleteByPscode(trans, pscode);
        }

        public DataTable GetBalanceStockByContractId(string contractId, string balanceId)
        {
            return this.dal.GetBalanceStockByContractId(contractId, balanceId);
        }

        public DataTable GetDiff(string contractId, string pscode, string isWBSRelevance)
        {
            return this.dal.GetDiff(contractId, pscode, isWBSRelevance);
        }

        public List<PurchaseStockModel> GetList()
        {
            return this.dal.GetList();
        }

        public PurchaseStockModel GetModel(string psid)
        {
            return this.dal.GetModel(psid);
        }

        public DataTable GetModifyStockByContractId(string contractId, string modifyId)
        {
            return this.dal.GetModifyStockByContractId(contractId, modifyId);
        }

        public DataTable GetPrjID(string pscode)
        {
            return this.dal.GetPrjID(pscode);
        }

        public DataTable GetPurchaseInfoByStorgeStock(string[] pscodes)
        {
            return this.dal.GetPurchaseInfoByStorgeStock(pscodes);
        }

        public DataTable GetPurchaseStockByContractId(string contractId, bool isConPurchase)
        {
            return this.dal.GetPurchaseStockByContractId(contractId, isConPurchase);
        }

        public DataTable GetPurchaseStockByPscode(string pscode)
        {
            return this.dal.GetPurchaseStockByPscode(pscode);
        }

        public DataTable GetPurchaseStockByPscode(string pscode, string strPrjId, string isWBSRelevance)
        {
            return this.dal.GetPurchaseStockByPscode(pscode, strPrjId, isWBSRelevance);
        }

        public DataTable GetPurchaseStockByResourceCodes(List<string> lstResourceCode, string purchaseCode)
        {
            return this.dal.GetPurchaseStockByResourceCodes(lstResourceCode.ToArray(), purchaseCode);
        }

        public DataTable GetReportDataSource(string condition)
        {
            return this.dal.GetReportDataSource(condition);
        }

        public DataTable GetReportDataSource(DateTime? startDate, DateTime? endDate, string resourceNames, string resourceCodes, string purchaseCode, string corpName, string projectName, string category, string specification, string brand, string modelNumber)
        {
            return this.dal.GetReportDataSource(startDate, endDate, resourceNames, resourceCodes, purchaseCode, corpName, projectName, category, specification, brand, modelNumber);
        }

        public DataTable GetTableByPurchaseCodes(string[] pscodes)
        {
            return this.dal.GetTableByPurchaseCodes(pscodes);
        }

        public void Update(PurchaseStockModel model)
        {
            this.dal.Update(null, model);
        }

        public void Update(SqlTransaction trans, PurchaseStockModel model)
        {
            this.dal.Update(trans, model);
        }
    }
}

