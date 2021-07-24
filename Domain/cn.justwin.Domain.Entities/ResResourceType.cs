namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class ResResourceType
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
            return (this.ResourceTypeId == ((ResResourceType) obj).ResourceTypeId);
        }

        public override int GetHashCode()
        {
            return this.ResourceTypeId.GetHashCode();
        }

        public override string ToString()
        {
            return this.ResourceTypeId.ToString();
        }

        public virtual string CBSCode { get; set; }

        public virtual DateTime? InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual bool? IsValid { get; set; }

        public virtual string ParentId { get; set; }

        public virtual string ResourceTypeCode { get; set; }

        public virtual string ResourceTypeId { get; set; }

        public virtual string ResourceTypeName { get; set; }
    }
}

