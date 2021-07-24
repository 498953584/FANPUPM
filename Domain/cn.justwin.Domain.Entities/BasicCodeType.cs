namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class BasicCodeType
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
            return (this.TypeCode == ((BasicCodeType) obj).TypeCode);
        }

        public override int GetHashCode()
        {
            return this.TypeCode.GetHashCode();
        }

        public override string ToString()
        {
            return this.TypeCode.ToString();
        }

        public virtual string TypeCode { get; set; }

        public virtual string TypeName { get; set; }
    }
}

