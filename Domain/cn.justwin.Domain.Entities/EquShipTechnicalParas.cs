namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquShipTechnicalParas
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
            return (this.TechId == ((EquShipTechnicalParas) obj).TechId);
        }

        public override int GetHashCode()
        {
            return this.TechId.GetHashCode();
        }

        public override string ToString()
        {
            return this.TechId.ToString();
        }

        public virtual string EquId { get; set; }

        public virtual string OtherShipInfo { get; set; }

        public virtual string TechId { get; set; }
    }
}

