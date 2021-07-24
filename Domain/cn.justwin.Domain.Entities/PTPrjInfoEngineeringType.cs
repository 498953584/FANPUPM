namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTPrjInfoEngineeringType
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
            return (this.ID == ((PTPrjInfoEngineeringType) obj).ID);
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public override string ToString()
        {
            return this.ID.ToString();
        }

        public virtual int? EngineeringSubType { get; set; }

        public virtual string EngineeringType { get; set; }

        public virtual string Grade { get; set; }

        public virtual string ID { get; set; }

        public virtual Guid PrjGuid { get; set; }
    }
}

