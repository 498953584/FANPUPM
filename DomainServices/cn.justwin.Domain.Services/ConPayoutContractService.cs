namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Data;
    using System.Linq;

    public class ConPayoutContractService : Repository<ConPayoutContract>
    {
        public ConPayoutContract GetByContractCode(string contractCode)
        {
            return (from p in this
                where p.ContractCode == contractCode
                select p).FirstOrDefault<ConPayoutContract>();
        }

        public ConPayoutContract GetByContractID(string contractID)
        {
            return (from p in this
                where p.ContractID == contractID
                select p).FirstOrDefault<ConPayoutContract>();
        }

        public DataTable GetDtblByPayoutContractCode(string contractCode)
        {
            string cmdText = "SELECT * FROM Con_Payout_Contract WHERE ContractCode like '%" + contractCode + "%'";
            return base.ExecuteQuery(cmdText, null);
        }
    }
}

