namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class ConIncometContract
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
            return (this.ContractID == ((ConIncometContract) obj).ContractID);
        }

        public override int GetHashCode()
        {
            return this.ContractID.GetHashCode();
        }

        public override string ToString()
        {
            return this.ContractID.ToString();
        }

        public virtual string Annex { get; set; }

        public virtual string BalanceMode { get; set; }

        public virtual string CllectionCondition { get; set; }

        public virtual int? conState { get; set; }

        public virtual string ContractCode { get; set; }

        public virtual string ContractID { get; set; }

        public virtual string ContractName { get; set; }

        public virtual decimal? ContractPrice { get; set; }

        public virtual string CParty { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual string FCode { get; set; }

        public virtual DateTime? FileTime { get; set; }

        public virtual int? FlowState { get; set; }

        public virtual bool? IsArchived { get; set; }

        public virtual bool? isFContract { get; set; }

        public virtual string MainProvision { get; set; }

        public virtual string Party { get; set; }

        public virtual string PayMode { get; set; }

        public virtual string Project { get; set; }

        public virtual string QualityPeriod { get; set; }

        public virtual DateTime? RefundDate { get; set; }

        public virtual DateTime? RegisterTime { get; set; }

        public virtual string Remark { get; set; }

        public virtual string Second { get; set; }

        public virtual int? sign { get; set; }

        public virtual string SignedAddress { get; set; }

        public virtual DateTime? SignedTime { get; set; }

        public virtual string SignPeople { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual string Subscriber { get; set; }

        public virtual string TypeID { get; set; }

        public virtual string UserCodes { get; set; }
    }
}

