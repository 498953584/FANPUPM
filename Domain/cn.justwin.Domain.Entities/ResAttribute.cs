namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class ResAttribute
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
            return (this.AttributeId == ((ResAttribute) obj).AttributeId);
        }

        public override int GetHashCode()
        {
            return this.AttributeId.GetHashCode();
        }

        public override string ToString()
        {
            return this.AttributeId.ToString();
        }

        public virtual string AttributeId { get; set; }

        public virtual string AttributeName { get; set; }

        public virtual DateTime? InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual string ResourceTypeId { get; set; }
    }
}

