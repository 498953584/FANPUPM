namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class WFprivilege
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
            return (this.wp_id == ((WFprivilege) obj).wp_id);
        }

        public override int GetHashCode()
        {
            return this.wp_id.GetHashCode();
        }

        public override string ToString()
        {
            return this.wp_id.ToString();
        }

        public virtual string businessClass { get; set; }

        public virtual string userlist { get; set; }

        public virtual int wp_id { get; set; }
    }
}

