namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class BudPreReimburseApply
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
            return (this.Id == ((BudPreReimburseApply) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual DateTime ApplyDate { get; set; }

        public virtual string Code { get; set; }

        public virtual string CostType { get; set; }

        public virtual int FlowState { get; set; }

        public virtual string Id { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string Name { get; set; }

        public virtual string PrjId { get; set; }

        public virtual string RptUser { get; set; }
    }
}

