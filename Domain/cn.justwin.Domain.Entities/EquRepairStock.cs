namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquRepairStock
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
            return (this.StockId == ((EquRepairStock) obj).StockId);
        }

        public override int GetHashCode()
        {
            return this.StockId.GetHashCode();
        }

        public override string ToString()
        {
            return this.StockId.ToString();
        }

        public virtual string CorpId { get; set; }

        public virtual decimal Quantity { get; set; }

        public virtual DateTime ReceiveDate { get; set; }

        public virtual string ReceivePerson { get; set; }

        public virtual string ReportId { get; set; }

        public virtual string ResourceId { get; set; }

        public virtual string StockId { get; set; }

        public virtual decimal UnitPrice { get; set; }
    }
}

