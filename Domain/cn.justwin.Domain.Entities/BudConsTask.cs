namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class BudConsTask
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
            return (this.ConsTaskId == ((BudConsTask) obj).ConsTaskId);
        }

        public override int GetHashCode()
        {
            return this.ConsTaskId.GetHashCode();
        }

        public override string ToString()
        {
            return this.ConsTaskId.ToString();
        }

        public virtual decimal? AccountingQuantity { get; set; }

        public virtual decimal CompleteQuantity { get; set; }

        public virtual string ConsReportId { get; set; }

        public virtual string ConsTaskId { get; set; }

        public virtual string Note { get; set; }

        public virtual string TaskId { get; set; }

        public virtual string WorkContent { get; set; }
    }
}

