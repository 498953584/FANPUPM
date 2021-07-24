namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class PTPrjInfo
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!object.ReferenceEquals(this, obj))
            {
                if (base.GetType() != obj.GetType())
                {
                    return false;
                }
                Guid? prjGuid = this.PrjGuid;
                Guid? nullable2 = ((PTPrjInfo) obj).PrjGuid;
                if (prjGuid.HasValue != nullable2.HasValue)
                {
                    return false;
                }
                if (prjGuid.HasValue)
                {
                    return (prjGuid.GetValueOrDefault() == nullable2.GetValueOrDefault());
                }
            }
            return true;
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

        public virtual string BudgetWay { get; set; }

        public virtual string BuildingArea { get; set; }

        public virtual string BuildingType { get; set; }

        public virtual string businessman { get; set; }

        public virtual string ComputeClass { get; set; }

        public virtual string ContractSum { get; set; }

        public virtual string ContractWay { get; set; }

        public virtual string Counsellor { get; set; }

        public virtual string Designer { get; set; }

        public virtual string Duration { get; set; }

        public virtual DateTime? EndDate { get; set; }

        public virtual string FileName { get; set; }

        public virtual string FileURL { get; set; }

        public virtual string grade { get; set; }

        public virtual int? i_ChildNum { get; set; }

        public virtual int? i_xh { get; set; }

        public virtual string Inspector { get; set; }

        public virtual bool? IsConfirm { get; set; }

        public virtual string IsValid { get; set; }

        public virtual string KeyPart { get; set; }

        public virtual string LinkMan { get; set; }

        public virtual Guid? MarketInfoGuid { get; set; }

        public virtual string OtherStatement { get; set; }

        public virtual string Owner { get; set; }

        public virtual int? OwnerCode { get; set; }

        public virtual string PayCondition { get; set; }

        public virtual string PayWay { get; set; }

        public virtual string Podepom { get; set; }

        [DataMember]
        public virtual string PrjCode { get; set; }

        public virtual double? PrjCost { get; set; }

        [DataMember]
        public virtual string PrjFundInfo { get; set; }

        [DataMember]
        public virtual Guid? PrjGuid { get; set; }

        public virtual string PrjInfo { get; set; }

        public virtual string PrjKindClass { get; set; }

        public virtual string PrjManager { get; set; }

        [DataMember]
        public virtual string PrjName { get; set; }

        public virtual string PrjPlace { get; set; }

        public virtual int? PrjState { get; set; }

        public virtual string PrjStateRemark { get; set; }

        [DataMember]
        public virtual string QualityClass { get; set; }

        public virtual string Rank { get; set; }

        public virtual string RationClass { get; set; }

        public virtual DateTime? RecordDate { get; set; }

        public virtual string Remark1 { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual string telephone { get; set; }

        public virtual string TenderWay { get; set; }

        public virtual string TotalHouseNum { get; set; }

        [DataMember]
        public virtual string TypeCode { get; set; }

        public virtual string UndergroundArea { get; set; }

        public virtual string UsegrounArea { get; set; }

        public virtual string UserCode { get; set; }

        public virtual string WorkUnit { get; set; }

        public virtual string xmgroup { get; set; }
    }
}

