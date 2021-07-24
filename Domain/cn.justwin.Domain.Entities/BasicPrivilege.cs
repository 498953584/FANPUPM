namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class BasicPrivilege
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
            return (this.PrivilegeId == ((BasicPrivilege) obj).PrivilegeId);
        }

        public override int GetHashCode()
        {
            return this.PrivilegeId.GetHashCode();
        }

        public override string ToString()
        {
            return this.PrivilegeId.ToString();
        }

        public virtual string PrivilegeId { get; set; }

        public virtual string RelationsKey { get; set; }

        public virtual string RelationsTable { get; set; }

        public virtual string UserCode { get; set; }
    }
}

