namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class ConIncometPayment
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
            return (this.ID == ((ConIncometPayment) obj).ID);
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public override string ToString()
        {
            return this.ID.ToString();
        }

        public virtual string Annex { get; set; }

        public virtual string CllectionCode { get; set; }

        public virtual decimal? CllectionPrice { get; set; }

        public virtual DateTime? CllectionTime { get; set; }

        public virtual string CllectionUser { get; set; }

        public virtual string ContractID { get; set; }

        public virtual string ID { get; set; }

        public virtual DateTime? InputDate { get; set; }

        public virtual string InputPerson { get; set; }

        public virtual Guid? MonthPlanUID { get; set; }

        public virtual string Remark { get; set; }

        public virtual int? state { get; set; }
    }
}

