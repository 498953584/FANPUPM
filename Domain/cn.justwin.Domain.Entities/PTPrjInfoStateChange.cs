namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTPrjInfoStateChange
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
            return (this.Id == ((PTPrjInfoStateChange) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual string ChangeReason { get; set; }

        public virtual int? ChangeState { get; set; }

        public virtual DateTime? ChangeTime { get; set; }

        public virtual string ChangeUser { get; set; }

        public virtual int? FlowState { get; set; }

        public virtual string Id { get; set; }

        public virtual DateTime? InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual string Note { get; set; }

        public virtual int? OldState { get; set; }

        public virtual string PrjId { get; set; }
    }
}

