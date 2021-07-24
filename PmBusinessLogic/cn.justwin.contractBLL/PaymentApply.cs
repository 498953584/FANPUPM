namespace cn.justwin.contractBLL
{
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PaymentApply
    {
        private IncomentPaymentApply paymentApply = new IncomentPaymentApply();

        public void Add(IncomentPaymentApplyModel model)
        {
            this.paymentApply.Add(model);
        }

        public void Delete(string id)
        {
            this.paymentApply.Delete(id, null);
        }

        public void Delete(List<string> paymentIds)
        {
            this.paymentApply.Delete(paymentIds);
        }

        public List<IncomentPaymentApplyModel> GetByContractId(string contractId)
        {
            return this.paymentApply.GetByContractId(contractId);
        }

        public IncomentPaymentApplyModel GetById(string id)
        {
            return this.paymentApply.GetById(id);
        }

        public decimal GetPaySum(string contractId, bool containPending)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" Con_Incomet_Contract.ContractID = '{0}' ", contractId);
            if (!containPending)
            {
                builder.Append("AND Con_Income_PaymentApply.FlowState = '1'");
            }
            else
            {
                builder.Append("AND Con_Income_PaymentApply.FlowState IN('1','0')");
            }
            List<IncomentPaymentApplyModel> list = this.paymentApply.GetList(builder.ToString());
            decimal? nullable = 0M;
            foreach (IncomentPaymentApplyModel model in list)
            {
                decimal? nullable2 = nullable;
                decimal paymentAmount = model.PaymentAmount;
                nullable = nullable2.HasValue ? new decimal?(nullable2.GetValueOrDefault() + paymentAmount) : null;
            }
            return nullable.Value;
        }

        public bool GreaterBalanceMoney(IncomentPaymentApplyModel model)
        {
            List<IncometPaymentModel> listArray = new IncometPayment().GetListArray(string.Format(" WHERE Con_Incomet_Payment.ContractID = '{0}'", model.ContractId));
            decimal? nullable = 0M;
            foreach (IncometPaymentModel model2 in listArray)
            {
                nullable += model2.CllectionPrice;
            }
            string strWhere = string.Format(" Con_Income_PaymentApply.FlowState != '-2' AND Con_Income_PaymentApply.ContractID = '{0}' ", model.ContractId);
            List<IncomentPaymentApplyModel> list = this.paymentApply.GetList(strWhere);
            decimal? nullable2 = 0M;
            foreach (IncomentPaymentApplyModel model3 in list)
            {
                decimal? nullable6 = nullable2;
                decimal paymentAmount = model3.PaymentAmount;
                nullable2 = nullable6.HasValue ? new decimal?(nullable6.GetValueOrDefault() + paymentAmount) : null;
            }
            decimal? nullable8 = nullable2;
            decimal? nullable9 = nullable;
            return ((nullable8.GetValueOrDefault() > nullable9.GetValueOrDefault()) && (nullable8.HasValue & nullable9.HasValue));
        }

        public bool IsExists(string paymentCode, string contractID)
        {
            return this.paymentApply.IsExists(paymentCode, contractID);
        }

        public void Update(IncomentPaymentApplyModel model)
        {
            this.paymentApply.Update(model);
        }
    }
}

