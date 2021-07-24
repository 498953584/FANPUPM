namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquTrailingProReport
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
            return (this.ProductionId == ((EquTrailingProReport) obj).ProductionId);
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

        public virtual string ProductionId { get; set; }

        public virtual string SailNo { get; set; }

        public virtual decimal? ShipCapaticy { get; set; }

        public virtual decimal? UnitPrice { get; set; }
    }
}

