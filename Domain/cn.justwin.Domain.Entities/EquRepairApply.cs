namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquRepairApply
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
            return (this.ApplyId == ((EquRepairApply) obj).ApplyId);
        }

        public override int GetHashCode()
        {
            return this.ApplyId.GetHashCode();
        }

        public override string ToString()
        {
            return this.ApplyId.ToString();
        }

        public virtual DateTime? ApplyDate { get; set; }

        public virtual string ApplyId { get; set; }

        public virtual string ApplyUser { get; set; }

        public virtual string EquId { get; set; }

        public virtual int FlowState { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual string LastRepairContents { get; set; }

        public virtual decimal? LastRepairCosts { get; set; }

        public virtual DateTime? LastRepairDate { get; set; }

        public virtual string Note { get; set; }

        public virtual DateTime PlanEndDate { get; set; }

        public virtual decimal PlanRepairCosts { get; set; }

        public virtual string PlanRepareContents { get; set; }

        public virtual DateTime PlanStartDate { get; set; }

        public virtual int RepairFlag { get; set; }

        public virtual string RepairId { get; set; }

        public virtual int RepairPlaceFlag { get; set; }

        public virtual int RepairType { get; set; }
    }
}

