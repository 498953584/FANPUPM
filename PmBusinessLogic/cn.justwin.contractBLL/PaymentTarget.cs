namespace cn.justwin.contractBLL
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class PaymentTarget
    {
        private cn.justwin.contractDAL.PaymentTarget paymentTarget = new cn.justwin.contractDAL.PaymentTarget();

        public void Add(List<PaymentTargetModel> listPaymentTarget, string paymentId)
        {
            this.paymentTarget.Add(listPaymentTarget, paymentId);
        }

        public DataTable GetConTarget(string contractId, string isWBSRelevance)
        {
            return this.paymentTarget.GetConTarget(contractId, isWBSRelevance);
        }

        public DataTable GetPaymentTarget(List<string> listTargetIds, string contractId, string paymentId, string isWBSRelevance)
        {
            return this.paymentTarget.GetPaymentTarget(listTargetIds, contractId, paymentId, isWBSRelevance);
        }

        public List<string> GetTargetIdsByConTargetId(string conTargetId)
        {
            return this.paymentTarget.GetTargetIdsByConTargetId(conTargetId);
        }
    }
}

