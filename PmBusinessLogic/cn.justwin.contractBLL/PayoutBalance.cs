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

    public class PayoutBalance
    {
        private cn.justwin.contractDAL.PayoutBalance balance = new cn.justwin.contractDAL.PayoutBalance();

        public void Add(PayoutBalanceModel model)
        {
            this.balance.Add(model, null);
        }

        public void AddBalanceAlarm(PayoutBalanceModel balanceInfo, bool isContainPending)
        {
            PayoutContractModel model = new cn.justwin.contractBLL.PayoutContract().GetModel(balanceInfo.ContractID);
            decimal num = model.ModifiedMoney.HasValue ? model.ModifiedMoney.Value : 0M;
            bool flag1 = this.GetBalancedAmount(balanceInfo.ContractID, isContainPending) > num;
        }

        public void Delete(List<string> balanceIds)
        {
            this.balance.Delete(balanceIds);
        }

        public void Delete(string BalanceID)
        {
            this.balance.Delete(BalanceID, null);
        }

        public bool Exists(string BalanceCode)
        {
            return this.balance.Exists(BalanceCode);
        }

        public string GetBalanceAmountState(decimal diffRate, string ContractId)
        {
            ConConfigContractService service = new ConConfigContractService();
            string str = "普通";
            if (!service.IsExist(ContractId))
            {
                return str;
            }
            ConConfigContract contract = (from p in service
                where p.ContractId == ContractId
                select p).FirstOrDefault<ConConfigContract>();
            decimal? nullable = new decimal?(diffRate * -100M);
            decimal? nullable2 = nullable;
            decimal? highBalanceAlarmLimit = contract.HighBalanceAlarmLimit;
            if ((nullable2.GetValueOrDefault() >= highBalanceAlarmLimit.GetValueOrDefault()) && (nullable2.HasValue & highBalanceAlarmLimit.HasValue))
            {
                return "高";
            }
            decimal? nullable4 = nullable;
            decimal? midBalanceAlarmLowerLimit = contract.MidBalanceAlarmLowerLimit;
            if ((nullable4.GetValueOrDefault() >= midBalanceAlarmLowerLimit.GetValueOrDefault()) && (nullable4.HasValue & midBalanceAlarmLowerLimit.HasValue))
            {
                return "中";
            }
            decimal? nullable6 = nullable;
            decimal? lowBalanceAlarmLowerLimit = contract.LowBalanceAlarmLowerLimit;
            if ((nullable6.GetValueOrDefault() >= lowBalanceAlarmLowerLimit.GetValueOrDefault()) && (nullable6.HasValue & lowBalanceAlarmLowerLimit.HasValue))
            {
                return "低";
            }
            return "普通";
        }

        public decimal GetBalancedAmount(string contractId, bool containPending)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Con_Payout_Balance.ContractID = '{0}' ", contractId);
            if (!containPending)
            {
                builder.Append("AND Con_Payout_Balance.FlowState = '1'");
            }
            else
            {
                builder.Append("AND Con_Payout_Balance.FlowState IN('1','0')");
            }
            List<PayoutBalanceModel> list = this.balance.GetList(builder.ToString());
            decimal? nullable = 0M;
            foreach (PayoutBalanceModel model in list)
            {
                nullable += model.BalanceMoney;
            }
            return nullable.Value;
        }

        public decimal GetBalancedAmountNotSelf(string contractId, bool containPending, string balanceId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Con_Payout_Balance.ContractID = '{0}' ", contractId);
            if (!containPending)
            {
                builder.Append("AND Con_Payout_Balance.FlowState = '1'");
            }
            else
            {
                builder.Append("AND Con_Payout_Balance.FlowState IN('1','0')");
            }
            builder.AppendFormat("  AND balanceId<>'{0}' ", balanceId);
            List<PayoutBalanceModel> list = this.balance.GetList(builder.ToString());
            decimal? nullable = 0M;
            foreach (PayoutBalanceModel model in list)
            {
                nullable += model.BalanceMoney;
            }
            return nullable.Value;
        }

        public List<PayoutBalanceModel> GetList(string strWhere)
        {
            return this.balance.GetList(strWhere);
        }

        public List<PayoutBalanceModel> GetList(string strWhere, string userCode)
        {
            return this.GetList(strWhere).FindAll(balanceInfo => JsonHelper.GetListFromJson(balanceInfo.UserCodes).Contains(userCode));
        }

        public PayoutBalanceModel GetModel(string BalanceID)
        {
            return this.balance.GetModel(BalanceID);
        }

        public DataTable GetReportData(string strWhere, string userCode)
        {
            return this.balance.GetReportDataSource(strWhere, userCode);
        }

        public DataTable GetReportData(string strWhere, string userCode, int pageIndex, int pageSize)
        {
            return this.balance.GetReportDataSource(strWhere, userCode, pageIndex, pageSize);
        }

        public string GetStateByBalanceAmount(decimal diffRate)
        {
            decimal num = diffRate * -100M;
            if (num >= ContractParameter.HighBalanceAlarmLimit)
            {
                return "高";
            }
            if (num >= ContractParameter.MidBalanceAlarmLowerLimit)
            {
                return "中";
            }
            if (num >= ContractParameter.LowBalanceAlarmLowerLimit)
            {
                return "低";
            }
            return "普通";
        }

        public bool GreaterModifiedMoney(PayoutBalanceModel balanceInfo)
        {
            decimal? nullable = 0M;
            string strWhere = string.Format(" Con_Payout_Balance.FlowState != '-2' \r\n                                               AND Con_Payout_Contract.ContractID = '{0}'", balanceInfo.ContractID);
            foreach (PayoutBalanceModel model in this.balance.GetList(strWhere))
            {
                nullable += model.BalanceMoney;
            }
            PayoutContractModel model2 = new cn.justwin.contractBLL.PayoutContract().GetModel(balanceInfo.ContractID);
            decimal? nullable5 = nullable;
            decimal? modifiedMoney = model2.ModifiedMoney;
            return ((nullable5.GetValueOrDefault() > modifiedMoney.GetValueOrDefault()) && (nullable5.HasValue & modifiedMoney.HasValue));
        }

        public bool IsExists(string BalanceCode, string ContractID)
        {
            return this.balance.IsExists(BalanceCode, ContractID);
        }

        public DataTable Select(string condition, string userCode)
        {
            return this.GetReportData(condition, userCode);
        }

        public DataTable SelectData(string condition, string userCode, int pageIndex, int pageSize)
        {
            return this.GetReportData(condition, userCode, pageIndex, pageSize);
        }

        public void Update(PayoutBalanceModel model)
        {
            this.balance.Update(model, null);
        }
    }
}

