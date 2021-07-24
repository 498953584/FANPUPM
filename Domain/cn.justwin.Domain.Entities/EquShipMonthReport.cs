namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquShipMonthReport
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
            return (this.MonthId == ((EquShipMonthReport) obj).MonthId);
        }

        public override int GetHashCode()
        {
            return this.MonthId.GetHashCode();
        }

        public override string ToString()
        {
            return this.MonthId.ToString();
        }

        public virtual DateTime? EndDate { get; set; }

        public virtual string EndMappingNo { get; set; }

        public virtual string EquId { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual string MonthId { get; set; }

        public virtual decimal? MonthMappingQty { get; set; }

        public virtual string Note { get; set; }

        public virtual string PrjId { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual string StartMappingNo { get; set; }

        public virtual decimal? UnitPrice { get; set; }
    }
}

