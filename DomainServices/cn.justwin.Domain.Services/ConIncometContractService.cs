namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Data;
    using System.Linq;

    public class ConIncometContractService : Repository<ConIncometContract>
    {
        public ConIncometContract GetByContractCode(string contractCode)
        {
            return (from p in this
                where p.ContractCode == contractCode
                select p).FirstOrDefault<ConIncometContract>();
        }

        public ConIncometContract GetByContractId(string contractId)
        {
            return (from p in this
                where p.ContractID == contractId
                select p).FirstOrDefault<ConIncometContract>();
        }

        public DataTable GetDtblByContractCode(string contractCode)
        {
            string cmdText = "SELECT * FROM Con_Incomet_Contract WHERE ContractCode like '%" + contractCode + "%'";
            return base.ExecuteQuery(cmdText, null);
        }
    }
}

