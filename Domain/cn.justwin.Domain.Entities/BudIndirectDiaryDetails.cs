namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class BudIndirectDiaryDetails
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
            return (this.InDiaryDetailsId == ((BudIndirectDiaryDetails) obj).InDiaryDetailsId);
        }

        public override int GetHashCode()
        {
            return this.InDiaryDetailsId.GetHashCode();
        }

        public override string ToString()
        {
            return this.InDiaryDetailsId.ToString();
        }

        public virtual decimal Amount { get; set; }

        public virtual string ApplyDetailId { get; set; }

        public virtual decimal AuditAmount { get; set; }

        public virtual string CBSCode { get; set; }

        public virtual string IndetailsCode { get; set; }

        public virtual string InDiaryDetailsId { get; set; }

        public virtual string InDiaryId { get; set; }

        public virtual string Name { get; set; }

        public virtual int No { get; set; }

        public virtual string Note { get; set; }

        public virtual string PettyCashId { get; set; }
    }
}

