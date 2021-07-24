namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquShipDayReport
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
            return (this.DayId == ((EquShipDayReport) obj).DayId);
        }

        public override int GetHashCode()
        {
            return this.DayId.GetHashCode();
        }

        public override string ToString()
        {
            return this.DayId.ToString();
        }

        public virtual int? CalculateType { get; set; }

        public virtual DateTime? ConstructionDate { get; set; }

        public virtual string DayId { get; set; }

        public virtual decimal? DayOilWear { get; set; }

        public virtual decimal? DiscountRate { get; set; }

        public virtual string EquId { get; set; }

        public virtual int FlowState { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual int IsTeamInfo { get; set; }

        public virtual string Note { get; set; }

        public virtual decimal? NotWorkRestDurationT3 { get; set; }

        public virtual int OutputValueType { get; set; }

        public virtual string PrjId { get; set; }

        public virtual DateTime ReportDate { get; set; }

        public virtual decimal? WorkDurationT1 { get; set; }

        public virtual decimal? WorkRestDurationT2 { get; set; }
    }
}

