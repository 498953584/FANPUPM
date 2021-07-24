namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class BudConsTaskRes
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
            return (this.ConsTaskResId == ((BudConsTaskRes) obj).ConsTaskResId);
        }

        public override int GetHashCode()
        {
            return this.ConsTaskResId.GetHashCode();
        }

        public override string ToString()
        {
            return this.ConsTaskResId.ToString();
        }

        public virtual decimal? AccountingQuantity { get; set; }

        public virtual string CBSCode { get; set; }

        public virtual string ConsTaskId { get; set; }

        public virtual string ConsTaskResId { get; set; }

        public virtual decimal Quantity { get; set; }

        public virtual string ResourceId { get; set; }

        public virtual decimal UnitPrice { get; set; }
    }
}

