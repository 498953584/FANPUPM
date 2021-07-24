namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class Purchase
    {
        private readonly cn.justwin.stockDAL.Purchase dal = new cn.justwin.stockDAL.Purchase();

        public void Add(PurchaseModel model)
        {
            this.dal.Add(null, model);
        }

        public void Add(SqlTransaction trans, PurchaseModel model)
        {
            this.dal.Add(trans, model);
        }

        public void AddInContract(SqlTransaction trans, PurchaseModel model)
        {
            this.dal.AddInContract(trans, model);
        }

        public int Delete(List<string> lstPcode)
        {
            return this.dal.Delete(lstPcode);
        }

        public int Delete(SqlTransaction trans, string pCode)
        {
            return this.dal.Delete(trans, pCode);
        }

        public List<PurchaseModel> GetAllList()
        {
            return this.dal.GetList(string.Empty);
        }

        public DataTable GetAllPurchase(string prjId, int purchaseType)
        {
            string condition = string.Empty;
            if (!string.IsNullOrEmpty(prjId))
            {
                condition = string.Format(" AND Project = '{0}' AND PurchaseType = {1}", prjId, purchaseType);
                return this.dal.GetTable(condition);
            }
            condition = string.Format("AND PurchaseType = {0}", purchaseType);
            return this.dal.GetTable(condition);
        }

        public string GetCodeByGuid(string guid)
        {
            return this.dal.GetCodeByGuid(guid);
        }

        public string GetConPurchasePcode(string contractId)
        {
            return this.dal.GetConPurchasePcode(contractId);
        }

        public List<PurchaseModel> GetList(string condition)
        {
            return this.dal.GetList(condition);
        }

        public PurchaseModel GetModel(string pcode)
        {
            return this.dal.GetModel(pcode);
        }

        public PurchaseModel GetModelByContractId(string contractId)
        {
            return this.dal.GetModelByContractId(contractId);
        }

        public DataTable GetTable(DateTime? startDate, DateTime? endDate, string pcode, string person, string PrjId, string ConName, int type)
        {
            StringBuilder builder = new StringBuilder();
            if (startDate.HasValue)
            {
                builder.AppendFormat(" AND intime >= '{0}' ", startDate.Value.ToShortDateString());
            }
            if (endDate.HasValue)
            {
                builder.AppendFormat(" AND intime < '{0}' ", endDate.Value.AddDays(1.0).ToShortDateString());
            }
            if (!string.IsNullOrEmpty(pcode))
            {
                builder.AppendFormat("and pcode like '%{0}%' ", pcode);
            }
            if (!string.IsNullOrEmpty(person))
            {
                builder.AppendFormat("and person like '%{0}%' ", person);
            }
            if (!string.IsNullOrEmpty(PrjId))
            {
                builder.AppendFormat("and Project like '%{0}%' ", PrjId);
            }
            if (!string.IsNullOrEmpty(ConName))
            {
                builder.AppendFormat("and Con_Payout_Contract.ContractName like '%{0}%'", ConName);
            }
            if (type == 0)
            {
                return this.dal.GetTable(builder.ToString());
            }
            return this.dal.GetEquTable(builder.ToString());
        }

        public List<PurchaseModel> Select(DateTime startDate, DateTime endDate, string pcode, string prjId, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" Sm_Purchase.intime between '{0}' and '{1}' ", startDate.ToShortDateString(), endDate.ToShortDateString());
            if (!string.IsNullOrEmpty(pcode))
            {
                builder.AppendFormat(" and Sm_Purchase.pcode like '%{0}%' ", pcode);
            }
            if (!string.IsNullOrEmpty(prjId))
            {
                builder.AppendFormat(" AND Project='{0}' ", prjId);
            }
            builder.AppendFormat(" AND UserCodes  like '%{0}%' ", userCode);
            builder.Append(" and Sm_Purchase.flowstate = 1 ");
            return this.dal.GetList(builder.ToString());
        }

        public List<PurchaseModel> Select(DateTime startDate, DateTime endDate, string pcode, string prjId, string userCode, int purchaseType)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" Sm_Purchase.intime between '{0}' and '{1}' ", startDate.ToShortDateString(), endDate.ToShortDateString());
            if (!string.IsNullOrEmpty(pcode))
            {
                builder.AppendFormat(" and Sm_Purchase.pcode like '%{0}%' ", pcode);
            }
            if (!string.IsNullOrEmpty(prjId))
            {
                builder.AppendFormat(" AND Project='{0}' ", prjId);
            }
            builder.AppendFormat(" AND UserCodes  like '%{0}%' ", userCode);
            builder.Append(" and Sm_Purchase.flowstate = 1 ");
            builder.Append(" and Sm_Purchase.PurchaseType = 1 ");
            return this.dal.GetList(builder.ToString());
        }

        public void Update(PurchaseModel model)
        {
            this.dal.Update(null, model);
        }

        public void Update(SqlTransaction trans, PurchaseModel model)
        {
            this.dal.Update(trans, model);
        }

        public void UpdateAcceptState(string purchaseCode)
        {
            PurchaseModel model = this.dal.GetModel(purchaseCode);
            model.acceptstate++;
            this.dal.Update(null, model);
        }

        public void UpdateAcceptState(SqlTransaction trans, string purchaseCode)
        {
            PurchaseModel model = this.dal.GetModel(purchaseCode);
            model.acceptstate++;
            this.dal.Update(trans, model);
        }

        public void UpdateAcceptState(SqlTransaction trans, string[] purchaseCodes)
        {
            foreach (string str in purchaseCodes)
            {
                this.UpdateAcceptState(trans, str);
            }
        }
    }
}

