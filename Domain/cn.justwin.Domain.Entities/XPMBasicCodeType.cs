namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class XPMBasicCodeType
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
            return (this.TypeID == ((XPMBasicCodeType) obj).TypeID);
        }

        public override int GetHashCode()
        {
            return this.TypeID.GetHashCode();
        }

        public override string ToString()
        {
            return this.TypeID.ToString();
        }

        public virtual Guid ContractCropType { get; set; }

        public virtual bool IsValid { get; set; }

        public virtual bool IsVisible { get; set; }

        public virtual string Owner { get; set; }

        public virtual string Remark { get; set; }

        public virtual string SignCode { get; set; }

        public virtual int TypeID { get; set; }

        public virtual string TypeName { get; set; }

        public virtual DateTime VersionTime { get; set; }
    }
}

