namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class BudModifyTaskRes
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
            return (this.ModifyTaskResId == ((BudModifyTaskRes) obj).ModifyTaskResId);
        }

        public override int GetHashCode()
        {
            return this.ModifyTaskResId.GetHashCode();
        }

        public override string ToString()
        {
            return this.ModifyTaskResId.ToString();
        }

        public virtual decimal? LossCoefficient { get; set; }

        public virtual string ModifyId { get; set; }

        public virtual string ModifyTaskId { get; set; }

        public virtual string ModifyTaskResId { get; set; }

        public virtual string ResourceId { get; set; }

        public virtual decimal ResourcePrice { get; set; }

        public virtual decimal ResourceQuantity { get; set; }
    }
}

