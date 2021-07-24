namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class ResResource
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
            return (this.ResourceId == ((ResResource) obj).ResourceId);
        }

        public override int GetHashCode()
        {
            return this.ResourceId.GetHashCode();
        }

        public override string ToString()
        {
            return this.ResourceId.ToString();
        }

        public virtual string Attribute { get; set; }

        public virtual string Brand { get; set; }

        public virtual DateTime? InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual string ModelNumber { get; set; }

        public virtual string Note { get; set; }

        public virtual string ResourceCode { get; set; }

        public virtual string ResourceId { get; set; }

        public virtual string ResourceName { get; set; }

        public virtual string ResourceType { get; set; }

        public virtual string Series { get; set; }

        public virtual string Specification { get; set; }

        public virtual int? SupplierId { get; set; }

        public virtual decimal? TaxRate { get; set; }

        public virtual string TechnicalParameter { get; set; }

        public virtual string Unit { get; set; }
    }
}

