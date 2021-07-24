namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Linq;

    public class BudContractTaskAuditService : Repository<BudContractTaskAudit>
    {
        public BudContractTaskAudit GetById(string id)
        {
            return (from a in this
                where a.ContractTaskAuditId == id
                select a).FirstOrDefault<BudContractTaskAudit>();
        }

        public BudContractTaskAudit GetByProject(string prjId)
        {
            return (from a in this
                where a.PrjId == prjId
                select a).FirstOrDefault<BudContractTaskAudit>();
        }
    }
}

