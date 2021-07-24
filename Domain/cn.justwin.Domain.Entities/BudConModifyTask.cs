namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class BudConModifyTask
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
            return (this.ModifyTaskId == ((BudConModifyTask) obj).ModifyTaskId);
        }

        public override int GetHashCode()
        {
            return this.ModifyTaskId.GetHashCode();
        }

        public override string ToString()
        {
            return this.ModifyTaskId.ToString();
        }

        public virtual int? ConstructionPeriod { get; set; }

        public virtual string ContractId { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual string FeatureDescription { get; set; }

        public virtual decimal? Labor { get; set; }

        public virtual decimal? MainMaterial { get; set; }

        public virtual string ModifyId { get; set; }

        public virtual string ModifyTaskCode { get; set; }

        public virtual string ModifyTaskContent { get; set; }

        public virtual string ModifyTaskId { get; set; }

        public virtual int? ModifyType { get; set; }

        public virtual string Note { get; set; }

        public virtual string OrderNumber { get; set; }

        public virtual string ParentId { get; set; }

        public virtual string PrjId2 { get; set; }

        public virtual decimal Quantity { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual decimal? SubMaterial { get; set; }

        public virtual string TaskId { get; set; }

        public virtual decimal Total { get; set; }

        public virtual string Unit { get; set; }

        public virtual decimal UnitPrice { get; set; }
    }
}

