namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class ConPayoutContract
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
            return (this.ContractID == ((ConPayoutContract) obj).ContractID);
        }

        public override int GetHashCode()
        {
            return this.ContractID.GetHashCode();
        }

        public override string ToString()
        {
            return this.ContractID.ToString();
        }

        public virtual string Address { get; set; }

        public virtual string AName { get; set; }

        public virtual string Annex { get; set; }

        public virtual DateTime? ArchiveDate { get; set; }

        public virtual string BalanceMode { get; set; }

        public virtual string BName { get; set; }

        public virtual string CapitalNumber { get; set; }

        public virtual int? conState { get; set; }

        public virtual string ContractCode { get; set; }

        public virtual string ContractID { get; set; }

        public virtual decimal? ContractMoney { get; set; }

        public virtual string ContractName { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual int? fictitious { get; set; }

        public virtual string financeNumber { get; set; }

        public virtual string financeProject { get; set; }

        public virtual int? FlowState { get; set; }

        public virtual string InContractID { get; set; }

        public virtual DateTime? InputDate { get; set; }

        public virtual string InputPerson { get; set; }

        public virtual bool? IsArchived { get; set; }

        public virtual bool? IsMainContract { get; set; }

        public virtual string MainContractID { get; set; }

        public virtual string MainItem { get; set; }

        public virtual decimal? ModifiedMoney { get; set; }

        public virtual string Notes { get; set; }

        public virtual string PaymentCondition { get; set; }

        public virtual string PayMode { get; set; }

        public virtual decimal? PrepayMoney { get; set; }

        public virtual string PrjGuid { get; set; }

        public virtual DateTime? SignDate { get; set; }

        public virtual string SignPerson { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual string TypeID { get; set; }

        public virtual string UserCodes { get; set; }
    }
}

