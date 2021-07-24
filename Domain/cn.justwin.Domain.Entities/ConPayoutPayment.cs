namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class ConPayoutPayment
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
            return (this.ID == ((ConPayoutPayment) obj).ID);
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }

        public override string ToString()
        {
            return this.ID.ToString();
        }

        public virtual string Account { get; set; }

        public virtual string Annex { get; set; }

        public virtual string Bank { get; set; }

        public virtual string Beneficiary { get; set; }

        public virtual string CapitalNumber { get; set; }

        public virtual bool? ContainPending { get; set; }

        public virtual string ContractID { get; set; }

        public virtual int? FlowState { get; set; }

        public virtual string ID { get; set; }

        public virtual DateTime? InputDate { get; set; }

        public virtual string InputPerson { get; set; }

        public virtual Guid? MonthPlanUID { get; set; }

        public virtual string Notes { get; set; }

        public virtual string PaymentCode { get; set; }

        public virtual DateTime? PaymentDate { get; set; }

        public virtual decimal? PaymentMoney { get; set; }

        public virtual string PaymentPerson { get; set; }

        public virtual int? PayType { get; set; }
    }
}

