namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class BudContractTaskAudit
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (base.GetType() != obj.GetType())
            {
                return false;
            }
            return (this.ContractTaskAuditId == ((BudContractTaskAudit) obj).ContractTaskAuditId);
        }

        public override int GetHashCode()
        {
            return this.ContractTaskAuditId.GetHashCode();
        }

        public override string ToString()
        {
            return this.ContractTaskAuditId.ToString();
        }

        public virtual string ContractTaskAuditId { get; set; }

        public virtual int? FlowState { get; set; }

        public virtual DateTime? InputDate { get; set; }

        public virtual string PrjId { get; set; }

        public virtual string PrjName { get; set; }
    }
}

