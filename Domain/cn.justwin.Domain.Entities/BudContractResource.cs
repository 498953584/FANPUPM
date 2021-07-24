namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class BudContractResource
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
            return (this.TaskResourceId == ((BudContractResource) obj).TaskResourceId);
        }

        public override int GetHashCode()
        {
            return this.TaskResourceId.GetHashCode();
        }

        public override string ToString()
        {
            return this.TaskResourceId.ToString();
        }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual string PrjGuid { get; set; }

        public virtual string ResourceId { get; set; }

        public virtual decimal? ResourcePrice { get; set; }

        public virtual decimal? ResourceQuantity { get; set; }

        public virtual string TaskId { get; set; }

        public virtual string TaskResourceId { get; set; }
    }
}

