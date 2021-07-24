namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquEquipment
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
            return (this.Id == ((EquEquipment) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual decimal DepreciationRate { get; set; }

        public virtual string DurableYear { get; set; }

        public virtual string EquCode { get; set; }

        public virtual string EquName { get; set; }

        public virtual int EquProperty { get; set; }

        public virtual DateTime? FactoryDate { get; set; }

        public virtual string FactoryNumber { get; set; }

        public virtual string Id { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual DateTime? MiddleInspectDate { get; set; }

        public virtual string Note { get; set; }

        public virtual string OtherCredentials { get; set; }

        public virtual string PeriodicVertification { get; set; }

        public virtual DateTime? PurchaseDate { get; set; }

        public virtual decimal PurchasePrice { get; set; }

        public virtual string ReceiptNo { get; set; }

        public virtual string ShipCapaticy { get; set; }

        public virtual string ShipLength { get; set; }

        public virtual string ShipWidth { get; set; }

        public virtual int State { get; set; }

        public virtual int? SupplierId { get; set; }

        public virtual string TypeId { get; set; }

        public virtual DateTime? YearInspectDate { get; set; }
    }
}

