namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTLOGIN
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
            return (this.V_DLID == ((PTLOGIN) obj).V_DLID);
        }

        public override int GetHashCode()
        {
            return this.V_DLID.GetHashCode();
        }

        public override string ToString()
        {
            return this.V_DLID.ToString();
        }

        public virtual string AuditNameImagePath { get; set; }

        public virtual string AuditPwd { get; set; }

        public virtual string C_SFYX { get; set; }

        public virtual string ControlDept { get; set; }

        public virtual string IsManager { get; set; }

        public virtual int? MailPageSize { get; set; }

        public virtual int? MailSpace { get; set; }

        public virtual string PmSet { get; set; }

        public virtual string RoleCodes { get; set; }

        public virtual int? SkinID { get; set; }

        public virtual string V_BGFW { get; set; }

        public virtual string v_cycd { get; set; }

        public virtual string V_DLID { get; set; }

        public virtual string V_DLMM { get; set; }

        public virtual string V_YHDM { get; set; }
    }
}

