namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ConConfigContractService : Repository<ConConfigContract>
    {
        public void Deltes(List<string> Ids)
        {
            List<ConConfigContract> list = new List<ConConfigContract>();
            if (Ids.Count<string>() > 0)
            {
                list = (from p in this
                    where Ids.Contains(p.ContractId)
                    select p).ToList<ConConfigContract>();
            }
            foreach (ConConfigContract contract in list)
            {
                base.Delete(contract);
            }
        }

        public ConConfigContract GetByContractID(string contractID)
        {
            return (from p in this
                where p.ContractId == contractID
                select p).FirstOrDefault<ConConfigContract>();
        }

        public ConConfigContract GetById(string Id)
        {
            return (from p in this
                where p.Id == Id
                select p).FirstOrDefault<ConConfigContract>();
        }

        public bool IsExist(string ContractId)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(ContractId))
            {
                flag = (from p in this
                    where p.ContractId == ContractId
                    select p).Count<ConConfigContract>() > 0;
            }
            return flag;
        }
    }
}

