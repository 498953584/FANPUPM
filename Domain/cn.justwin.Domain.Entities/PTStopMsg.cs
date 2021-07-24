namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTStopMsg
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
            return (this.StopMsgId == ((PTStopMsg) obj).StopMsgId);
        }

        public override int GetHashCode()
        {
            return this.StopMsgId.GetHashCode();
        }

        public override string ToString()
        {
            return this.StopMsgId.ToString();
        }

        public virtual string ConstArea { get; set; }

        public virtual string ConstUnit { get; set; }

        public virtual int? FlowState { get; set; }

        public virtual string GeneralSign { get; set; }

        public virtual DateTime? GeneralSignDate { get; set; }

        public virtual string ImpactLossDegree { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual string MainContent { get; set; }

        public virtual string PrjGuid { get; set; }

        public virtual string ProblemReason { get; set; }

        public virtual string ProjectMileage { get; set; }

        public virtual string ProjectProblem { get; set; }

        public virtual string RemedialMeasure { get; set; }

        public virtual DateTime StopDate { get; set; }

        public virtual string StopMsgId { get; set; }

        public virtual string StopReason { get; set; }

        public virtual string SupervisorSign { get; set; }

        public virtual DateTime? SupervisorSignDate { get; set; }
    }
}

