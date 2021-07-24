namespace cn.justwin.contractBLL
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using System;
    using System.Collections.Generic;

    public class ContractPayendBll
    {
        private ContractPayend contractPayend = new ContractPayend();

        public int Add(ContractPayendModel model)
        {
            return this.contractPayend.Add(model);
        }

        public int Delete(string PayendID)
        {
            return this.contractPayend.Delete(PayendID);
        }

        public int DeleteByWhere(string where)
        {
            return this.contractPayend.DeleteByWhere(where);
        }

        public List<ContractPayendModel> GetListArray(string strWhere)
        {
            return this.contractPayend.GetListArray(strWhere);
        }

        public ContractPayendModel GetModel(string PayendID)
        {
            return this.contractPayend.GetModel(PayendID);
        }

        public int Update(ContractPayendModel model)
        {
            return this.contractPayend.Update(model);
        }
    }
}

