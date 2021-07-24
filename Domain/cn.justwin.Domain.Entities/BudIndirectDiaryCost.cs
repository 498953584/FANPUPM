namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class BudIndirectDiaryCost
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
            return (this.InDiaryId == ((BudIndirectDiaryCost) obj).InDiaryId);
        }

        public override int GetHashCode()
        {
            return this.InDiaryId.GetHashCode();
        }

        public override string ToString()
        {
            return this.InDiaryId.ToString();
        }

        public virtual string ApplyId { get; set; }

        public virtual DateTime AuditDate { get; set; }

        public virtual string Code { get; set; }

        public virtual string CostType { get; set; }

        public virtual string Department { get; set; }

        public virtual int FlowState { get; set; }

        public virtual string InDiaryId { get; set; }

        public virtual string IndireCode { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual DateTime InputDate2 { get; set; }

        public virtual string InputUser { get; set; }

        public virtual string InputUserCode { get; set; }

        public virtual string InssuedByCode { get; set; }

        public virtual bool IsAudit { get; set; }

        public virtual string IssuedBy { get; set; }

        public virtual string Name { get; set; }

        public virtual string PettyCashId { get; set; }

        public virtual string ProjectId { get; set; }

        public virtual string Type { get; set; }
    }
}

