namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class SmWantplan
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
            return (this.swcode == ((SmWantplan) obj).swcode);
        }

        public override int GetHashCode()
        {
            return this.swcode.GetHashCode();
        }

        public override string ToString()
        {
            return this.swcode.ToString();
        }

        public virtual int acceptstate { get; set; }

        public virtual string annx { get; set; }

        public virtual bool? ContainPending { get; set; }

        public virtual string EquipmentId { get; set; }

        public virtual string explain { get; set; }

        public virtual int flowstate { get; set; }

        public virtual DateTime intime { get; set; }

        public virtual string person { get; set; }

        public virtual string procode { get; set; }

        public virtual string swcode { get; set; }

        public virtual string swid { get; set; }
    }
}

