namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class WFBusinessCode
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
            return (this.BusinessCode == ((WFBusinessCode) obj).BusinessCode);
        }

        public override int GetHashCode()
        {
            return this.BusinessCode.GetHashCode();
        }

        public override string ToString()
        {
            return this.BusinessCode.ToString();
        }

        public virtual string BusinessCode { get; set; }

        public virtual string BusinessName { get; set; }

        public virtual string C_MKDM { get; set; }

        public virtual string DoWithUrl { get; set; }

        public virtual string KeyWord { get; set; }

        public virtual string LinkTable { get; set; }

        public virtual string LookUrl { get; set; }

        public virtual string NameField { get; set; }

        public virtual string PrimaryField { get; set; }

        public virtual string ProjectField { get; set; }

        public virtual string StateField { get; set; }
    }
}

