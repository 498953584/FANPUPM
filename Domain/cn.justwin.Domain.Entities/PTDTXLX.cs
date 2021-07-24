namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTDTXLX
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
            return (this.V_LXBM == ((PTDTXLX) obj).V_LXBM);
        }

        public override int GetHashCode()
        {
            return this.V_LXBM.GetHashCode();
        }

        public override string ToString()
        {
            return this.V_LXBM.ToString();
        }

        public virtual string C_OPENFLAG { get; set; }

        public virtual string FilterField { get; set; }

        public virtual string V_DBLJ { get; set; }

        public virtual string V_LXBM { get; set; }

        public virtual string V_LXMC { get; set; }

        public virtual string V_TPLJ { get; set; }
    }
}

