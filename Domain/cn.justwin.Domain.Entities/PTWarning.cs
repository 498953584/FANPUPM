namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTWarning
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
            if (obj.GetType() != base.GetType())
            {
                return false;
            }
            return (this.WarningId == ((PTWarning) obj).WarningId);
        }

        public override int GetHashCode()
        {
            return this.WarningId.GetHashCode();
        }

        public override string ToString()
        {
            return this.WarningId.ToString();
        }

        public virtual DateTime? InputDate { get; set; }

        public virtual bool IsOpened { get; set; }

        public virtual bool? IsValid { get; set; }

        public virtual string RelationsColumn { get; set; }

        public virtual string RelationsKey { get; set; }

        public virtual string RelationsTable { get; set; }

        public virtual string URI { get; set; }

        public virtual string UserCode { get; set; }

        public virtual string WarningContent { get; set; }

        public virtual int WarningId { get; set; }

        public virtual string WarningTitle { get; set; }
    }
}

