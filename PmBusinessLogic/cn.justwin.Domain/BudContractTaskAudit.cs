namespace cn.justwin.Domain
{
    using cn.justwin.DAL;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class BudContractTaskAudit
    {
        public static BudContractTaskAudit GetModelById(string Id)
        {
            BudContractTaskAudit audit = new BudContractTaskAudit();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from b in entities.Bud_ContractTaskAudit
                    where b.ContractTaskAuditId == Id
                    select new BudContractTaskAudit { Id = b.ContractTaskAuditId, PrjId = b.PrjId, PrjName = b.PrjName, FlowState = b.FlowState, InputDate = b.InputDate }).FirstOrDefault<BudContractTaskAudit>();
            }
        }

        public static BudContractTaskAudit GetModelByPrjId(string prjId)
        {
            BudContractTaskAudit audit = new BudContractTaskAudit();
            using (pm2Entities entities = new pm2Entities())
            {
                return (from b in entities.Bud_ContractTaskAudit
                    where b.PrjId == prjId
                    select new BudContractTaskAudit { Id = b.ContractTaskAuditId, PrjId = b.PrjId, PrjName = b.PrjName, FlowState = b.FlowState, InputDate = b.InputDate }).FirstOrDefault<BudContractTaskAudit>();
            }
        }

        public int? FlowState { get; set; }

        public string Id { get; set; }

        public DateTime? InputDate { get; set; }

        public string PrjId { get; set; }

        public string PrjName { get; set; }
    }
}

