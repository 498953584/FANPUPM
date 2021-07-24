namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquGrapProReport
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
            return (this.ProductionId == ((EquGrapProReport) obj).ProductionId);
        }

        public override int GetHashCode()
        {
            return this.ProductionId.GetHashCode();
        }

        public override string ToString()
        {
            return this.ProductionId.ToString();
        }

        public virtual string DayId { get; set; }

        public virtual decimal? DayReportQty { get; set; }

        public virtual decimal? DeductQty { get; set; }

        public virtual string EquId { get; set; }

        public virtual string ProductionId { get; set; }

        public virtual decimal ShipCapaticy { get; set; }

        public virtual decimal? TotalDeductQty { get; set; }

        public virtual decimal? UnitPrice { get; set; }

        public virtual decimal? WorkCount { get; set; }
    }
}

