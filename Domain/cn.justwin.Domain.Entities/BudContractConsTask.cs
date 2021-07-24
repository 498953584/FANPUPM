namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class BudContractConsTask
    {
        public BudContractConsTask()
        {
            this.Amount = 0M;
            this.ApproveAmount = 0M;
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
            return (this.ConsTaskId == ((BudContractConsTask) obj).ConsTaskId);
        }

        public override int GetHashCode()
        {
            return this.ConsTaskId.GetHashCode();
        }

        public override string ToString()
        {
            return this.ConsTaskId.ToString();
        }

        public virtual decimal Amount { get; set; }

        public virtual decimal ApproveAmount { get; set; }

        public virtual string ConsTaskId { get; set; }

        public virtual BudContractTask ContractTask { get; set; }

        public virtual string Note { get; set; }

        public virtual string RptId { get; set; }

        public virtual string TaskId { get; set; }
    }
}

