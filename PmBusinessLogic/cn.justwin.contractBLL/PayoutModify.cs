namespace cn.justwin.contractBLL
{
    using cn.justwin.BLL;
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class PayoutModify
    {
        private cn.justwin.contractDAL.PayoutModify modify = new cn.justwin.contractDAL.PayoutModify();

        public void Add(PayoutModifyModel model)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    this.modify.Add(model, trans);
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw new Exception("添加失败");
                }
            }
        }

        public void Delete(string ModifyID)
        {
            this.modify.Delete(ModifyID, null);
        }

        public void Delete(List<string> modifyIds)
        {
            this.modify.Delete(modifyIds);
        }

        public bool Exists(string ModifyCode)
        {
            return this.modify.Exists(ModifyCode);
        }

        private List<PayoutModifyModel> GetList(string strWhere)
        {
            return this.modify.GetList(strWhere);
        }

        public List<PayoutModifyModel> GetList(string strWhere, string userCode)
        {
            return this.GetList(strWhere).FindAll(modifyInfo => JsonHelper.GetListFromJson(modifyInfo.UserCodes).Contains(userCode));
        }

        public PayoutModifyModel GetModel(string ModifyID)
        {
            return this.modify.GetModel(ModifyID);
        }

        public bool IsExists(string ModifyCode, string ContractID)
        {
            return this.modify.IsExists(ModifyCode, ContractID);
        }

        public void Update(PayoutModifyModel model, decimal? oldModifyMoney)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionString))
            {
                connection.Open();
                SqlTransaction trans = connection.BeginTransaction();
                try
                {
                    this.modify.Update(model, trans);
                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw new Exception("添加失败");
                }
            }
        }

        private void UpdateModifiedMoney(PayoutModifyModel model, decimal? oldModifyMoney, SqlTransaction trans)
        {
            cn.justwin.contractBLL.PayoutContract contract = new cn.justwin.contractBLL.PayoutContract();
            PayoutContractModel model2 = contract.GetModel(model.ContractID);
            if (!oldModifyMoney.HasValue)
            {
                model2.ModifiedMoney += model.ModifyMoney;
            }
            else
            {
                model2.ModifiedMoney = (model2.ModifiedMoney - oldModifyMoney) + model.ModifyMoney;
            }
            contract.Update(model2, trans);
        }
    }
}

