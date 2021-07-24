namespace cn.justwin.contractBLL
{
    using cn.justwin.BLL;
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PayoutInvoice
    {
        private cn.justwin.contractDAL.PayoutInvoice invoice = new cn.justwin.contractDAL.PayoutInvoice();

        public void Add(PayoutInvoiceInfo model)
        {
            this.invoice.Add(model, null);
        }

        public void Delete(string InvoiceID)
        {
            this.invoice.Delete(InvoiceID, null);
        }

        public void Delete(List<string> invoiceIds)
        {
            this.invoice.Delete(invoiceIds);
        }

        public bool Exists(string invoiceNo)
        {
            return this.invoice.Exists(invoiceNo);
        }

        public decimal? GetInvoiceSum(string contractId)
        {
            List<PayoutInvoiceInfo> listByContractId = this.GetListByContractId(contractId);
            decimal? nullable = 0M;
            foreach (PayoutInvoiceInfo info in listByContractId)
            {
                nullable += info.Amount;
            }
            return nullable;
        }

        public DataTable GetLedger(string strWhere)
        {
            return this.invoice.GetLedger(strWhere);
        }

        public DataTable GetLedger(string strWhere, string userCode)
        {
            DataTable ledger = this.GetLedger(strWhere);
            DataTableFilter filter = new DataTableFilter();
            return filter.Filter(ledger, "UserCodes", userCode);
        }

        private List<PayoutInvoiceInfo> GetList(string strWhere)
        {
            return this.invoice.GetList(strWhere);
        }

        public List<PayoutInvoiceInfo> GetList(string strWhere, string userCode)
        {
            return this.GetList(strWhere).FindAll(invoiceInfo => JsonHelper.GetListFromJson(invoiceInfo.UserCodes).Contains(userCode));
        }

        public List<PayoutInvoiceInfo> GetListByContractId(string contractId)
        {
            string strWhere = string.Format(" Con_Payout_Contract.ContractID = '{0}' ", contractId);
            return this.GetList(strWhere);
        }

        public PayoutInvoiceInfo GetModel(string InvoiceID)
        {
            return this.invoice.GetModel(InvoiceID);
        }

        public DataTable GetReportData(string strWhere, string userCode)
        {
            return this.invoice.GetReportData(strWhere, userCode);
        }

        public DataTable GetReportData(string strWhere, string userCode, int pageIndex, int pageSize)
        {
            return this.invoice.GetReportDataSource(strWhere, userCode, pageIndex, pageSize);
        }

        public DataTable Select(string condition, string userCode)
        {
            return this.GetReportData(condition, userCode);
        }

        public DataTable SelectData(string condition, string userCode, int pageIndex, int pageSize)
        {
            return this.GetReportData(condition, userCode, pageIndex, pageSize);
        }

        public void Update(PayoutInvoiceInfo model)
        {
            this.invoice.Update(model, null);
        }
    }
}

