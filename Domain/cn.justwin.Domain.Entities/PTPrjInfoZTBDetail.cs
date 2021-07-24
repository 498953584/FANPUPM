namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class PTPrjInfoZTBDetail
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
            return (this.PrjGuid == ((PTPrjInfoZTBDetail) obj).PrjGuid);
        }

        public override int GetHashCode()
        {
            return this.PrjGuid.GetHashCode();
        }

        public override string ToString()
        {
            return this.PrjGuid.ToString();
        }

        public virtual DateTime? ActualRunDate { get; set; }

        public virtual string AfforestArea { get; set; }

        public virtual int? BuildingTypeNo { get; set; }

        public virtual string BusinessManager { get; set; }

        public virtual string City { get; set; }

        public virtual DateTime? CompletedDate { get; set; }

        public virtual int? CompletedFlowState { get; set; }

        public virtual string CompletedNote { get; set; }

        public virtual decimal? ElseMargin { get; set; }

        public virtual string EngineeringEstimates { get; set; }

        public virtual string EngineeringSubType { get; set; }

        public virtual string EngineeringType { get; set; }

        [DataMember]
        public virtual decimal? ForecastProfitRate { get; set; }

        public virtual string Grade { get; set; }

        public virtual DateTime? InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual bool IsTender { get; set; }

        public virtual decimal? ManagementMargin { get; set; }

        public virtual int? MemberFlowState { get; set; }

        public virtual decimal? MigrantQualityMarginRate { get; set; }

        public virtual DateTime? OutBidDate { get; set; }

        public virtual bool? OutBidIsReturn { get; set; }

        public virtual string OutBidRemark { get; set; }

        public virtual string OwnerAddress { get; set; }

        public virtual string OwnerLinkMan { get; set; }

        public virtual string OwnerLinkPhone { get; set; }

        public virtual string ParkArea { get; set; }

        public virtual decimal? PerformanceBond { get; set; }

        public virtual string PrequalificationRequire { get; set; }

        public virtual string PrjApprovalOf { get; set; }

        public virtual string PrjDutyPerson { get; set; }

        [DataMember]
        public virtual string PrjFundWorkable { get; set; }

        [DataMember]
        public virtual Guid PrjGuid { get; set; }

        public virtual string PrjManagerRequire { get; set; }

        public virtual string PrjProperty { get; set; }

        public virtual string PrjReadOne { get; set; }

        public virtual string ProgAgent { get; set; }

        public virtual DateTime? ProjApplyDate { get; set; }

        public virtual DateTime? ProjApprovalDate { get; set; }

        public virtual string ProjElseRequest { get; set; }

        public virtual int? ProjFlowSate { get; set; }

        public virtual string ProjInfoOrigin { get; set; }

        public virtual int? ProjPeopleAge { get; set; }

        public virtual string ProjPeopleDep { get; set; }

        public virtual string ProjPeopleDuty { get; set; }

        [DataMember]
        public virtual string ProjPeopleName { get; set; }

        [DataMember]
        public virtual string ProjPeopleSex { get; set; }

        public virtual string ProjPeopleTel { get; set; }

        public virtual int? ProjRegistDeadline { get; set; }

        public virtual DateTime? ProjStartDate { get; set; }

        public virtual string ProjStartRemark { get; set; }

        public virtual DateTime? ProjTenderAnswerDate { get; set; }

        public virtual DateTime? ProjTenderBeginDate { get; set; }

        public virtual string ProjTenderContent { get; set; }

        public virtual string ProjTenderCostContent { get; set; }

        public virtual DateTime? ProjTenderDate { get; set; }

        public virtual decimal? ProjTenderEarnestMoney { get; set; }

        public virtual string ProjTenderPayWay { get; set; }

        public virtual string ProjTenderRemark { get; set; }

        public virtual string Province { get; set; }

        public virtual DateTime? QualificationFailData { get; set; }

        public virtual string QualificationFailReason { get; set; }

        public virtual decimal QualificationMargin { get; set; }

        public virtual DateTime? QualificationPassDate { get; set; }

        public virtual string QualificationPassReason { get; set; }

        public virtual string QualificationReadOne { get; set; }

        public virtual int? SetUpFlowState { get; set; }

        public virtual DateTime? SuccessBidDate { get; set; }

        public virtual decimal? SuccessBidPrice { get; set; }

        public virtual string SuccessBidRemark { get; set; }

        public virtual string TechnicalLeaderRequire { get; set; }

        public virtual string Telephone { get; set; }

        public virtual string TenderAppraiseMethod { get; set; }

        public virtual decimal? TenderAverage { get; set; }

        public virtual decimal? TenderCeilingPrice { get; set; }

        public virtual DateTime? TenderProspect { get; set; }

        public virtual decimal? TenderQuote { get; set; }

        public virtual string TenderReadOne { get; set; }

        public virtual string TenderUnit { get; set; }

        public virtual decimal? WithholdingTaxRate { get; set; }
    }
}

