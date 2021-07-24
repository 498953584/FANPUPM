namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class ConPayoutModify
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
            return (this.ModifyID == ((ConPayoutModify) obj).ModifyID);
        }

        public override int GetHashCode()
        {
            return this.ModifyID.GetHashCode();
        }

        public override string ToString()
        {
            return this.ModifyID.ToString();
        }

        public virtual string Annex { get; set; }

        public virtual string BudModifyId { get; set; }

        public virtual string ContractID { get; set; }

        public virtual int? FlowState { get; set; }

        public virtual DateTime? InputDate { get; set; }

        public virtual string InputPerson { get; set; }

        public virtual string ModifyCode { get; set; }

        public virtual DateTime? ModifyDate { get; set; }

        public virtual string ModifyID { get; set; }

        public virtual decimal? ModifyMoney { get; set; }

        public virtual string ModifyPerson { get; set; }

        public virtual string Notes { get; set; }

        public virtual string Reason { get; set; }
    }
}

