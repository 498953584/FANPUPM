namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class BudTaskChange
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
            return (this.TaskChangeId == ((BudTaskChange) obj).TaskChangeId);
        }

        public override int GetHashCode()
        {
            return this.TaskChangeId.GetHashCode();
        }

        public override string ToString()
        {
            return this.TaskChangeId.ToString();
        }

        public virtual int? FlowState { get; set; }

        public virtual DateTime? InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual string Note { get; set; }

        public virtual string PrjId { get; set; }

        public virtual string TaskChangeId { get; set; }

        public virtual int? Version { get; set; }

        public virtual string VersionCode { get; set; }
    }
}

