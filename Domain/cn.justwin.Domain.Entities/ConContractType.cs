namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class ConContractType
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
            return (this.TypeID == ((ConContractType) obj).TypeID);
        }

        public override int GetHashCode()
        {
            return this.TypeID.GetHashCode();
        }

        public override string ToString()
        {
            return this.TypeID.ToString();
        }

        public virtual string CBSCode { get; set; }

        public virtual DateTime? InputDate { get; set; }

        public virtual string InputPerson { get; set; }

        public virtual bool? IsValid { get; set; }

        public virtual string Notes { get; set; }

        public virtual string TypeCode { get; set; }

        public virtual string TypeID { get; set; }

        public virtual string TypeName { get; set; }

        public virtual string TypeShort { get; set; }

        public virtual string UserCodes { get; set; }
    }
}

