namespace cn.justwin.contractBLL
{
    using cn.justwin.BLL;
    using cn.justwin.contractDAL;
    using cn.justwin.contractModel;
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Services;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    public class PayoutPayment
    {
        private cn.justwin.contractDAL.PayoutPayment payment = new cn.justwin.contractDAL.PayoutPayment();

        public void Add(PayoutPaymentModel model)
        {
            this.payment.Add(model, null);
        }

        public void Delete(string ID)
        {
            this.payment.Delete(ID, null);
        }

        public void Delete(List<string> paymentIds)
        {
            this.payment.Delete(paymentIds);
        }

        public bool Exists(string paymentId)
        {
            return this.payment.Exists(paymentId);
        }

        public string GetCurrentMonthUID(string contractId)
        {
            return this.payment.GetCurrentMonthUID(contractId);
        }

        public DataTable GetFundPlan(string contractId)
        {
            return this.payment.GetFundPlan(contractId);
        }

        public List<string> GetFundPlanByMonthPlanUID(string monthPlanUID)
        {
            return this.payment.GetFundPlanByMonthPlanUID(monthPlanUID);
        }

        public List<PayoutPaymentModel> GetList()
        {
            return this.payment.GetList();
        }

        public List<PayoutPaymentModel> GetList(string strWhere)
        {
            return this.payment.GetList(strWhere);
        }

        public List<PayoutPaymentModel> GetList(string strWhere, string userCode)
        {
            return this.GetList(strWhere).FindAll(paymentInfo => JsonHelper.GetListFromJson(paymentInfo.UserCodes).Contains(userCode));
        }

        public PayoutPaymentModel GetModel(string ID)
        {
            return this.payment.GetModel(ID);
        }

        public string GetPayoutPayAmount(decimal diffRate, string ContractId)
        {
            ConConfigContractService service = new ConConfigContractService();
            string str = "普通";
            decimal? nullable = new decimal?(diffRate * 100M);
            if (!service.IsExist(ContractId))
            {
                return str;
            }
            ConConfigContract contract = (from p in service
                where p.ContractId == ContractId
                select p).FirstOrDefault<ConConfigContract>();
            decimal? nullable2 = nullable;
            decimal? highPayAlarmLimit = contract.HighPayAlarmLimit;
            if ((nullable2.GetValueOrDefault() >= highPayAlarmLimit.GetValueOrDefault()) && (nullable2.HasValue & highPayAlarmLimit.HasValue))
            {
                return "高";
            }
            decimal? nullable4 = nullable;
            decimal? midPayAlarmLowerLimit = contract.MidPayAlarmLowerLimit;
            if ((nullable4.GetValueOrDefault() >= midPayAlarmLowerLimit.GetValueOrDefault()) && (nullable4.HasValue & midPayAlarmLowerLimit.HasValue))
            {
                return "中";
            }
            decimal? nullable6 = nullable;
            decimal? lowPayAlarmLowerLimit = contract.LowPayAlarmLowerLimit;
            if ((nullable6.GetValueOrDefault() >= lowPayAlarmLowerLimit.GetValueOrDefault()) && (nullable6.HasValue & lowPayAlarmLowerLimit.HasValue))
            {
                return "低";
            }
            return "普通";
        }

        public decimal? GetPaySum(string contractId)
        {
            return new decimal?(this.GetPaySum(contractId, false));
        }

        public decimal GetPaySum(string contractId, bool containPending)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" Con_Payout_Contract.ContractID = '{0}' ", contractId);
            if (!containPending)
            {
                builder.Append("AND Con_Payout_Payment.FlowState = '1'");
            }
            else
            {
                builder.Append("AND Con_Payout_Payment.FlowState IN('1','0')");
            }
            List<PayoutPaymentModel> list = this.GetList(builder.ToString());
            decimal? nullable = 0M;
            foreach (PayoutPaymentModel model in list)
            {
                nullable += model.PaymentMoney;
            }
            return nullable.Value;
        }

        public decimal GetPaySumNotSelf(string contractId, bool containPending, string paymentId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(" Con_Payout_Contract.ContractID = '{0}' ", contractId);
            if (!containPending)
            {
                builder.Append("AND Con_Payout_Payment.FlowState = '1'");
            }
            else
            {
                builder.Append("AND Con_Payout_Payment.FlowState IN('1','0')");
            }
            builder.AppendFormat(" AND Con_Payout_Payment.ID<> '{0}' ", paymentId);
            List<PayoutPaymentModel> list = this.GetList(builder.ToString());
            decimal? nullable = 0M;
            foreach (PayoutPaymentModel model in list)
            {
                nullable += model.PaymentMoney;
            }
            return nullable.Value;
        }

        public DataTable GetReportData(string strWhere, string userCode)
        {
            return this.payment.GetReportData(strWhere, userCode);
        }

        public DataTable GetReportData(string strWhere, string userCode, int pageIndex, int pageSize)
        {
            return this.payment.GetReportDataSource(strWhere, userCode, pageIndex, pageSize);
        }

        public string GetStateByBalanceAmount(decimal diffRate)
        {
            decimal num = diffRate * 100M;
            if (num >= ContractParameter.HighPayAlarmLimit)
            {
                return "高";
            }
            if (num >= ContractParameter.MidPayAlarmLowerLimit)
            {
                return "中";
            }
            if (num >= ContractParameter.LowPayAlarmLowerLimit)
            {
                return "低";
            }
            return "普通";
        }

        public bool GreaterBalanceMoney(PayoutPaymentModel model)
        {
            List<PayoutBalanceModel> list = new cn.justwin.contractBLL.PayoutBalance().GetList(string.Format(" Con_Payout_Balance.ContractID = '{0}'", model.ContractID));
            decimal? nullable = 0M;
            foreach (PayoutBalanceModel model2 in list)
            {
                nullable += model2.BalanceMoney;
            }
            string strWhere = string.Format(" Con_Payout_Payment.FlowState != '-2' AND Con_Payout_Payment.ContractID = '{0}' ", model.ContractID);
            List<PayoutPaymentModel> list2 = this.payment.GetList(strWhere);
            decimal? nullable2 = 0M;
            foreach (PayoutPaymentModel model3 in list2)
            {
                nullable2 += model3.PaymentMoney;
            }
            decimal? nullable9 = nullable2;
            decimal? nullable10 = nullable;
            return ((nullable9.GetValueOrDefault() > nullable10.GetValueOrDefault()) && (nullable9.HasValue & nullable10.HasValue));
        }

        public bool IsExists(string PaymentCode, string ContractID)
        {
            return this.payment.IsExists(PaymentCode, ContractID);
        }

        public DataTable Select(string condition, string userCode)
        {
            return this.GetReportData(condition, userCode);
        }

        public DataTable SelectData(string condition, string userCode, int pageIndex, int pageSize)
        {
            return this.GetReportData(condition, userCode, pageIndex, pageSize);
        }

        public void Update(PayoutPaymentModel model)
        {
            this.payment.Update(model, null);
        }
    }
}

