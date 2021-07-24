namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BudContractConsReportService : Repository<BudContractConsReport>
    {
        public void DelByContractId(string contractId)
        {
            string sql = "DELETE FROM Bud_ContractConsReport WHERE contractId = '" + contractId + "'";
            base.ExcuteSql(sql);
        }

        public void DeleteInvalid()
        {
            string sql = "DELETE FROM Bud_ContractConsReport WHERE IsValid = '0'";
            base.ExcuteSql(sql);
        }

        public BudContractConsReport getByBalanceIdAndContractIDAndType(string balanceId, string ContractId, string type)
        {
            return (from bc in this
                where ((bc.BalanceId == balanceId) && (bc.ContractId == ContractId)) && (bc.Type == type)
                select bc).FirstOrDefault<BudContractConsReport>();
        }

        public BudContractConsReport GetById(string id)
        {
            return (from r in this
                where r.RptId == id
                select r).FirstOrDefault<BudContractConsReport>();
        }

        public List<BudContractConsReport> GetContractReportByConIDAndBalID(string contractID, string BalanceID)
        {
            return (from r in this
                where (((r.ContractId == contractID) && (r.BalanceId == BalanceID)) && (r.FlowState == 1)) && r.IsValid
                orderby r.InputDate descending
                select r).ToList<BudContractConsReport>();
        }

        public List<BudContractConsReport> GetContractReportByConIDAndBalIDEmpty(string contractID, string BalanceID)
        {
            return (from r in this
                where (((r.ContractId == contractID) && ((r.BalanceId == BalanceID) || (r.BalanceId == null))) && (r.FlowState == 1)) && r.IsValid
                orderby r.InputDate descending
                select r).ToList<BudContractConsReport>();
        }
    }
}

