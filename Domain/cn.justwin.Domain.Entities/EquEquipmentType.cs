namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class EquEquipmentType
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
            return (this.Id == ((EquEquipmentType) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual string Code { get; set; }

        public virtual string Flag { get; set; }

        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual int No { get; set; }

        public virtual string ParentId { get; set; }
    }
}

