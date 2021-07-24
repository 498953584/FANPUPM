namespace cn.justwin.contractBLL
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PayoutTarget
    {
        private cn.justwin.contractDAL.PayoutTarget payment = new cn.justwin.contractDAL.PayoutTarget();

        public void Add(List<PayoutTargetModel> listmodel)
        {
            this.payment.Add(listmodel);
        }

        public void Add(List<PayoutTargetModel> listmodel, string contractId)
        {
            this.payment.Add(listmodel, contractId);
        }

        public void DelByContractId(List<string> ids)
        {
            this.payment.DelByContractId(ids);
        }

        public void DelByContractId(string contractId)
        {
            this.payment.DelByContractId(contractId);
        }

        public void DelByIds(List<string> ids)
        {
            this.payment.DelByIds(ids);
        }

        public void DelByTaskId(List<string> ids)
        {
            this.payment.DelByTaskId(ids);
        }

        public DataTable GetContractInfoByTaskId(string TaskId)
        {
            return this.payment.GetContInfoByTaskId(TaskId);
        }

        public bool GetIsUsedByTaskId(string taskId)
        {
            return this.payment.GetIsUsedByTaskId(taskId);
        }

        public List<PayoutTargetModel> GetList(string strWhere)
        {
            return this.payment.GetListArray(strWhere);
        }

        public PayoutTargetModel GetModel(string ID)
        {
            return this.payment.GetModel(ID);
        }

        public DataTable GetTarget(string contractId, string taskIds, bool isPendingApprove, string isWBSRelevance)
        {
            return this.payment.GetTarget(contractId, taskIds, isPendingApprove, isWBSRelevance);
        }

        public List<string> GetTaskIdsByContractId(string contractId)
        {
            return this.payment.GetTaskIdsByContId(contractId);
        }

        public void Update(PayoutTargetModel model)
        {
            this.payment.Update(model);
        }
    }
}

