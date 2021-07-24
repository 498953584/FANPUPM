namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquRepairPlan
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
            return (this.RepairId == ((EquRepairPlan) obj).RepairId);
        }

        public override int GetHashCode()
        {
            return this.RepairId.GetHashCode();
        }

        public override string ToString()
        {
            return this.RepairId.ToString();
        }

        public virtual string ApplyUser { get; set; }

        public virtual int? DepNo { get; set; }

        public virtual string EquId { get; set; }

        public virtual int FlowState { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual string Note { get; set; }

        public virtual string RepairCode { get; set; }

        public virtual string RepairContents { get; set; }

        public virtual DateTime? RepairEndDate { get; set; }

        public virtual int RepairFlag { get; set; }

        public virtual string RepairId { get; set; }

        public virtual decimal RepairPlanCosts { get; set; }

        public virtual DateTime? RepairStartDate { get; set; }

        public virtual int RepairType { get; set; }
    }
}

