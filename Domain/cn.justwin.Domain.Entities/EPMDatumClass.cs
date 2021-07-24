namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EPMDatumClass
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
            return (this.TypeId == ((EPMDatumClass) obj).TypeId);
        }

        public override int GetHashCode()
        {
            return this.TypeId.GetHashCode();
        }

        public override string ToString()
        {
            return this.TypeId.ToString();
        }

        public virtual bool isDelete { get; set; }

        public virtual bool IsFixup { get; set; }

        public virtual bool IsValid { get; set; }

        public virtual bool IsVisible { get; set; }

        public virtual int ParentId { get; set; }

        public virtual string Remark { get; set; }

        public virtual int TypeId { get; set; }

        public virtual string TypeName { get; set; }
    }
}

