namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class ResUnit
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
            return (this.UnitId == ((ResUnit) obj).UnitId);
        }

        public override int GetHashCode()
        {
            return this.UnitId.GetHashCode();
        }

        public override string ToString()
        {
            return this.UnitId.ToString();
        }

        public virtual string Code { get; set; }

        public virtual DateTime? InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual string Name { get; set; }

        public virtual string UnitId { get; set; }
    }
}

