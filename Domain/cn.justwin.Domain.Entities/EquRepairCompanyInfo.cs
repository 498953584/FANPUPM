namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquRepairCompanyInfo
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
            return (this.ItemCode == ((EquRepairCompanyInfo) obj).ItemCode);
        }

        public override int GetHashCode()
        {
            return this.ItemCode.GetHashCode();
        }

        public override string ToString()
        {
            return this.ItemCode.ToString();
        }

        public virtual string Address { get; set; }

        public virtual string ItemCode { get; set; }

        public virtual string ItemName { get; set; }

        public virtual string Note { get; set; }

        public virtual string ParentCode { get; set; }

        public virtual decimal? UnitPrice { get; set; }
    }
}

