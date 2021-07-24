namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquShipRefuelApply
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
            return (this.ApplyId == ((EquShipRefuelApply) obj).ApplyId);
        }

        public override int GetHashCode()
        {
            return this.ApplyId.GetHashCode();
        }

        public override string ToString()
        {
            return this.ApplyId.ToString();
        }

        public virtual string ApplyId { get; set; }

        public virtual decimal ApplyQuantity { get; set; }

        public virtual DateTime ApplyRefuelDate { get; set; }

        public virtual string EquId { get; set; }

        public virtual decimal? EstimatedQuantity { get; set; }

        public virtual int FlowState { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual bool IsEntrustPurchase { get; set; }

        public virtual decimal? NotCompletedQuantity { get; set; }

        public virtual string Note { get; set; }

        public virtual string PrjId { get; set; }

        public virtual DateTime RefuelEndDate { get; set; }

        public virtual string RefuelPlace { get; set; }

        public virtual DateTime RefuelStartDate { get; set; }

        public virtual DateTime? ReportDate { get; set; }

        public virtual string ReportUser { get; set; }

        public virtual decimal ResidualOil { get; set; }

        public virtual decimal? Sump { get; set; }
    }
}

