namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class BudContractConsReport
    {
        public BudContractConsReport()
        {
            this.FlowState = -1;
            this.IsValid = false;
            this.InputDate = DateTime.Now;
        }

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
            return (this.RptId == ((BudContractConsReport) obj).RptId);
        }

        public override int GetHashCode()
        {
            return this.RptId.GetHashCode();
        }

        public override string ToString()
        {
            return this.RptId.ToString();
        }

        public virtual string BalanceId { get; set; }

        public virtual string ContractId { get; set; }

        public virtual int FlowState { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual bool IsValid { get; set; }

        public virtual string Note { get; set; }

        public virtual string PrjId { get; set; }

        public virtual string RptId { get; set; }

        public virtual string Type { get; set; }
    }
}

