namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class SAPersonalTax
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
            return (this.Id == ((SAPersonalTax) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual decimal Deduct { get; set; }

        public virtual decimal FloorLevel { get; set; }

        public virtual string Id { get; set; }

        public virtual decimal TaxRate { get; set; }

        public virtual decimal TopLevel { get; set; }
    }
}

