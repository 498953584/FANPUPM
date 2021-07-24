namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTStartReport
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
            return (this.ReportId == ((PTStartReport) obj).ReportId);
        }

        public override int GetHashCode()
        {
            return this.ReportId.GetHashCode();
        }

        public override string ToString()
        {
            return this.ReportId.ToString();
        }

        public virtual string ActualPrincipal { get; set; }

        public virtual DateTime? ApplyStartDate { get; set; }

        public virtual string ApplyUnit { get; set; }

        public virtual string AuditUnit { get; set; }

        public virtual string BuildUnitOpinion { get; set; }

        public virtual string ConstructionUnit { get; set; }

        public virtual string ExistenceProblem { get; set; }

        public virtual int? FlowState { get; set; }

        public virtual string ImplDep { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual string MainContent { get; set; }

        public virtual string ParentPrjId { get; set; }

        public virtual string PrepareCondition { get; set; }

        public virtual string PrjGuid { get; set; }

        public virtual string ProjectPlace { get; set; }

        public virtual DateTime? RealityStartDate { get; set; }

        public virtual string ReportId { get; set; }

        public virtual string SingleProjectName { get; set; }

        public virtual string SupervisorUnitOpinion { get; set; }
    }
}

