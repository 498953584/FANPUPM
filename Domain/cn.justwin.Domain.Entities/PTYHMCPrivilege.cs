namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTYHMCPrivilege
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
            return (this.Id == ((PTYHMCPrivilege) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual string C_MKDM { get; set; }

        public virtual string Id { get; set; }

        public virtual string IsBasic { get; set; }

        public virtual string IsHaveOp { get; set; }

        public virtual string V_YHDM { get; set; }
    }
}

