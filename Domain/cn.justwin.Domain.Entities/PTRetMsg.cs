namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTRetMsg
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
            return (this.RetMsgId == ((PTRetMsg) obj).RetMsgId);
        }

        public override int GetHashCode()
        {
            return this.RetMsgId.GetHashCode();
        }

        public override string ToString()
        {
            return this.RetMsgId.ToString();
        }

        public virtual string BuildUnitOpinion { get; set; }

        public virtual string BuildUnitPerson { get; set; }

        public virtual DateTime? BuildUnitSignDate { get; set; }

        public virtual string ConstArea { get; set; }

        public virtual string ConstUnit { get; set; }

        public virtual int? FlowState { get; set; }

        public virtual string GeneralSign { get; set; }

        public virtual DateTime? GeneralSignDate { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual string MainContent { get; set; }

        public virtual string PrjGuid { get; set; }

        public virtual string ProjectMileage { get; set; }

        public virtual DateTime RetDate { get; set; }

        public virtual string RetMsgId { get; set; }

        public virtual string StopMsgId { get; set; }

        public virtual string SupervisorSign { get; set; }

        public virtual DateTime? SupervisorSignDate { get; set; }
    }
}

