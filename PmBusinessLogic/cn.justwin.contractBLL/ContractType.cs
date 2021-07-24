namespace cn.justwin.contractBLL
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class ContractType
    {
        private readonly cn.justwin.contractDAL.ContractType dal = new cn.justwin.contractDAL.ContractType();

        public void Add(ContractTypeModel model)
        {
            this.dal.Add(null, model);
        }

        public void Add(SqlTransaction trans, ContractTypeModel model)
        {
            this.dal.Add(trans, model);
        }

        public void Delete(string TypeID)
        {
            this.dal.Delete(TypeID, null);
        }

        public void Delete(string[] typeIDs)
        {
            this.dal.Delete(typeIDs);
        }

        public DataTable GetCBSCode()
        {
            return this.dal.GetCBSCode();
        }

        public int GetCount(string TypeCode, string TypeName, string userCode)
        {
            return this.dal.GetCount(TypeCode, TypeName, userCode);
        }

        public int GetCount(string TypeCode, string TypeName, string userCode, bool? IsValid)
        {
            return this.dal.GetCount(TypeCode, TypeName, userCode, IsValid);
        }

        public List<ContractTypeModel> GetList(string TypeCode, string TypeName, int pageIndex, int pageSize, string userCode)
        {
            return this.dal.GetList(TypeCode, TypeName, pageIndex, pageSize, userCode);
        }

        public List<ContractTypeModel> GetList(string TypeCode, string TypeName, int pageIndex, int pageSize, string userCode, bool? IsValid)
        {
            return this.dal.GetList(TypeCode, TypeName, pageIndex, pageSize, userCode, IsValid);
        }

        public List<ContractTypeModel> GetListByCBSCode(string CBSCode)
        {
            return this.dal.GetListByCBSCode(CBSCode);
        }

        public ContractTypeModel GetModel(string TypeID)
        {
            return this.dal.GetModel(TypeID);
        }

        public void Update(ContractTypeModel model)
        {
            this.dal.Update(model);
        }

        public void UpdateCBSCode(SqlTransaction trans, ContractTypeModel model)
        {
            this.dal.UpdateCBSCode(trans, model);
        }

        public void UpdateTypeValid(string TypeIds, string IsValid)
        {
            this.dal.UpdateTypeValid(TypeIds, IsValid);
        }

        public void UpdateValid(string[] TypeIds, string IsValid)
        {
            this.dal.UpdateValid(TypeIds, IsValid);
        }
    }
}

