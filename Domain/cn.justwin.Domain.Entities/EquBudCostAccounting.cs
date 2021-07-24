namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquBudCostAccounting
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
            return (this.BudId == ((EquBudCostAccounting) obj).BudId);
        }

        public override int GetHashCode()
        {
            return this.BudId.GetHashCode();
        }

        public override string ToString()
        {
            return this.BudId.ToString();
        }

        public virtual decimal? AccessoriesCosts { get; set; }

        public virtual string BudId { get; set; }

        public virtual decimal? CleaningCosts { get; set; }

        public virtual decimal? EngineCosts { get; set; }

        public virtual decimal? EngineRepairCosts { get; set; }

        public virtual string EquId { get; set; }

        public virtual int FlowState { get; set; }

        public virtual decimal? FuelCosts { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual decimal? LaborProtecteCosts { get; set; }

        public virtual string LabourAnnexId { get; set; }

        public virtual decimal? LabourCosts { get; set; }

        public virtual decimal? MaintenanceCosts { get; set; }

        public virtual string MaterialsAnnexId { get; set; }

        public virtual decimal? MaterialsCosts { get; set; }

        public virtual decimal? MealsCosts { get; set; }

        public virtual string Note { get; set; }

        public virtual decimal? OilCosts { get; set; }

        public virtual decimal? OtherCosts { get; set; }

        public virtual string PipeAndGraftAnnexId { get; set; }

        public virtual string PowerAnnexId { get; set; }

        public virtual DateTime ReportDate { get; set; }

        public virtual string ReportUser { get; set; }

        public virtual decimal? SailRepairCosts { get; set; }

        public virtual decimal? SmallRepairCosts { get; set; }

        public virtual decimal? TrafficCosts { get; set; }

        public virtual string UpkeepAnnexId { get; set; }

        public virtual decimal? WaterCosts { get; set; }

        public virtual decimal? WorkerWelfareCosts { get; set; }
    }
}

