namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquRepairReport
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
            return (this.ReportId == ((EquRepairReport) obj).ReportId);
        }

        public override int GetHashCode()
        {
            return this.ReportId.GetHashCode();
        }

        public override string ToString()
        {
            return this.ReportId.ToString();
        }

        public virtual string Acceptor { get; set; }

        public virtual string ApplyId { get; set; }

        public virtual string ContractId { get; set; }

        public virtual string EquId { get; set; }

        public virtual string FaultDescription { get; set; }

        public virtual int FlowState { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual decimal LaborCost { get; set; }

        public virtual decimal MaterialCost { get; set; }

        public virtual string Note { get; set; }

        public virtual decimal? OtherBudCosts { get; set; }

        public virtual int? OtherBudCostType { get; set; }

        public virtual string OutCompany { get; set; }

        public virtual string OutDepartment { get; set; }

        public virtual string OutSubContractor { get; set; }

        public virtual string Reason { get; set; }

        public virtual DateTime RepairEndDate { get; set; }

        public virtual string RepairPerson { get; set; }

        public virtual DateTime RepairStartDate { get; set; }

        public virtual int RepairType { get; set; }

        public virtual DateTime ReportDate { get; set; }

        public virtual string ReportId { get; set; }
    }
}

