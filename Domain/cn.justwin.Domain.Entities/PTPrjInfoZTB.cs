namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTPrjInfoZTB
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
            return (this.PrjGuid == ((PTPrjInfoZTB) obj).PrjGuid);
        }

        public override int GetHashCode()
        {
            return this.PrjGuid.GetHashCode();
        }

        public override string ToString()
        {
            return this.PrjGuid.ToString();
        }

        public virtual string Area { get; set; }

        public virtual int BidFlowState { get; set; }

        public virtual string BudgetWay { get; set; }

        public virtual string BuildingArea { get; set; }

        public virtual string BuildingType { get; set; }

        public virtual int ChangeFlowSate { get; set; }

        public virtual string ComputeClass { get; set; }

        public virtual string ContractSum { get; set; }

        public virtual string ContractWay { get; set; }

        public virtual string Counsellor { get; set; }

        public virtual string Designer { get; set; }

        public virtual string Duration { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual string FileName { get; set; }

        public virtual string FileURL { get; set; }

        public virtual int GiveUpFlowState { get; set; }

        public virtual string GiveUpNote { get; set; }

        public virtual string GiveUpReason { get; set; }

        public virtual DateTime? GiveUpTime { get; set; }

        public virtual int InitiateFlowState { get; set; }

        public virtual string Inspector { get; set; }

        public virtual bool IsGiveUp { get; set; }

        public virtual string KeyPart { get; set; }

        public virtual string LinkMan { get; set; }

        public virtual Guid? MarketInfoGuid { get; set; }

        public virtual int? OldState { get; set; }

        public virtual string Operator { get; set; }

        public virtual string OtherStatement { get; set; }

        public virtual string Owner { get; set; }

        public virtual int? OwnerCode { get; set; }

        public virtual string ParentTypeCode { get; set; }

        public virtual string PayCondition { get; set; }

        public virtual string PayWay { get; set; }

        public virtual int PftFlowState { get; set; }

        public virtual string PrjCode { get; set; }

        public virtual double? PrjCost { get; set; }

        public virtual string PrjFundInfo { get; set; }

        public virtual Guid PrjGuid { get; set; }

        public virtual string PrjInfo { get; set; }

        public virtual string PrjKindClass { get; set; }

        public virtual string PrjManager { get; set; }

        public virtual string PrjName { get; set; }

        public virtual string PrjPlace { get; set; }

        public virtual int? PrjState { get; set; }

        public virtual DateTime? PrjStateChangeTime { get; set; }

        public virtual string QualityClass { get; set; }

        public virtual string Rank { get; set; }

        public virtual string RationClass { get; set; }

        public virtual string Remark { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual string TenderWay { get; set; }

        public virtual string TotalHouseNum { get; set; }

        public virtual string UndergroundArea { get; set; }

        public virtual string UsegrounArea { get; set; }

        public virtual string WorkUnit { get; set; }
    }
}

