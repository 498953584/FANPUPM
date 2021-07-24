namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class BudTask : ICloneable
    {
        public virtual object Clone()
        {
            return new BudTask { 
                TaskId = this.TaskId, TaskCode = this.TaskCode, TaskName = this.TaskName, TaskType = this.TaskType, ParentId = this.ParentId, OrderNumber = this.OrderNumber, PrjId = this.PrjId, Quantity = this.Quantity, Unit = this.Unit, UnitPrice = this.UnitPrice, Total = this.Total, ConstructionPeriod = this.ConstructionPeriod, StartDate = this.StartDate, EndDate = this.EndDate, InputDate = this.InputDate, InputUser = this.InputUser, 
                IsValid = this.IsValid, Note = this.Note, Modified = this.Modified, Version = this.Version, ModifyId = this.ModifyId, ModifyType = this.ModifyType
             };
        }

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
            return (this.TaskId == ((BudTask) obj).TaskId);
        }

        public override int GetHashCode()
        {
            return this.TaskId.GetHashCode();
        }

        public override string ToString()
        {
            return this.TaskId.ToString();
        }

        public virtual int? ConstructionPeriod { get; set; }

        public virtual string ContractId { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual string FeatureDescription { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual bool? IsValid { get; set; }

        public virtual string Modified { get; set; }

        public virtual string ModifyId { get; set; }

        public virtual string ModifyType { get; set; }

        public virtual string Note { get; set; }

        public virtual string OrderNumber { get; set; }

        public virtual string ParentId { get; set; }

        public virtual string PrjId { get; set; }

        public virtual decimal? Quantity { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual string TaskCode { get; set; }

        public virtual string TaskId { get; set; }

        public virtual string TaskName { get; set; }

        public virtual string TaskType { get; set; }

        public virtual decimal? Total { get; set; }

        public virtual decimal? Total2 { get; set; }

        public virtual string Unit { get; set; }

        public virtual decimal? UnitPrice { get; set; }

        public virtual int? Version { get; set; }
    }
}

