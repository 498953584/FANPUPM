namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class BudConsReport
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
            return (this.ConsReportId == ((BudConsReport) obj).ConsReportId);
        }

        public override int GetHashCode()
        {
            return this.ConsReportId.GetHashCode();
        }

        public override string ToString()
        {
            return this.ConsReportId.ToString();
        }

        public virtual string BalanceId { get; set; }

        public virtual string CancelAuditReason { get; set; }

        public virtual string CancelReportReason { get; set; }

        public virtual string ConsReportId { get; set; }

        public virtual string ConstractId { get; set; }

        public virtual int FlowState { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual bool? IsValid { get; set; }

        public virtual string PrjId { get; set; }

        public virtual string State { get; set; }

        public virtual string Type { get; set; }

        public virtual string WorkCard { get; set; }
    }
}

