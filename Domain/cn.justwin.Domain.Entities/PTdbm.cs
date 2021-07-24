namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTdbm
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
            return (this.I_bmdm == ((PTdbm) obj).I_bmdm);
        }

        public override int GetHashCode()
        {
            return this.I_bmdm.GetHashCode();
        }

        public override string ToString()
        {
            return this.I_bmdm.ToString();
        }

        public virtual string Adss { get; set; }

        public virtual string C_sfyx { get; set; }

        public virtual string CorpCode { get; set; }

        public virtual string Fx { get; set; }

        public virtual int I_bmdm { get; set; }

        public virtual int? I_jb { get; set; }

        public virtual int? I_sjdm { get; set; }

        public virtual int? I_xh { get; set; }

        public virtual int? I_xjbm { get; set; }

        public virtual string V_bmbm { get; set; }

        public virtual string V_bmjx { get; set; }

        public virtual string V_BMMC { get; set; }

        public virtual string V_bmqc { get; set; }

        public virtual string Yb { get; set; }
    }
}

