namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquCutterProReport
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
            return (this.ProductionId == ((EquCutterProReport) obj).ProductionId);
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

        public virtual decimal? Height { get; set; }

        public virtual decimal? Location_X { get; set; }

        public virtual decimal? Location_Y { get; set; }

        public virtual decimal? Longth { get; set; }

        public virtual string ProductionId { get; set; }

        public virtual decimal? ReportQty { get; set; }

        public virtual decimal? UnitPrice { get; set; }

        public virtual decimal? Width { get; set; }
    }
}

