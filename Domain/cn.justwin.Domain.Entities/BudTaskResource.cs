namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class BudTaskResource
    {
        public virtual object Clone()
        {
            BudTaskResource resource = new BudTaskResource();
            this.TaskResourceId = this.TaskResourceId;
            this.TaskId = this.TaskId;
            this.ResourceId = this.ResourceId;
            this.ResourceQuantity = this.ResourceQuantity;
            this.InputUser = this.InputUser;
            this.InputDate = this.InputDate;
            this.ResourcePrice = this.ResourcePrice;
            this.PrjGuid = this.PrjGuid;
            this.Versions = this.Versions;
            return resource;
        }

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
            return (this.TaskId == ((BudTaskResource) obj).TaskId);
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

        public virtual decimal? LossCoefficient { get; set; }

        public virtual string PrjGuid { get; set; }

        public virtual string ResourceId { get; set; }

        public virtual decimal? ResourcePrice { get; set; }

        public virtual decimal? ResourceQuantity { get; set; }

        public virtual string TaskId { get; set; }

        public virtual string TaskResourceId { get; set; }

        public virtual int? Versions { get; set; }
    }
}

