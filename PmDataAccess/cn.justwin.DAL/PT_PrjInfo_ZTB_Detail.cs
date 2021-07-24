namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="PT_PrjInfo_ZTB_Detail"), DataContract(IsReference=true)]
    public class PT_PrjInfo_ZTB_Detail : EntityObject
    {
        private DateTime? _ActualRunDate;
        private string _AfforestArea;
        private int? _BuildingTypeNo;
        private string _BusinessManager;
        private string _City;
        private DateTime? _CompletedDate;
        private int? _CompletedFlowState;
        private string _CompletedNote;
        private decimal? _ElseMargin;
        private string _EngineeringEstimates;
        private string _EngineeringSubType;
        private string _EngineeringType;
        private decimal? _ForecastProfitRate;
        private string _Grade;
        private DateTime? _InputDate;
        private string _InputUser;
        private bool _IsTender;
        private decimal? _ManagementMargin;
        private int? _MemberFlowState;
        private decimal? _MigrantQualityMarginRate;
        private DateTime? _OutBidDate;
        private bool? _OutBidIsReturn;
        private string _OutBidRemark;
        private string _OwnerAddress;
        private string _OwnerLinkMan;
        private string _OwnerLinkPhone;
        private string _ParkArea;
        private decimal? _PerformanceBond;
        private string _PrequalificationRequire;
        private string _PrjApprovalOf;
        private string _PrjDutyPerson;
        private string _PrjFundWorkable;
        private Guid _PrjGuid;
        private string _PrjManagerRequire;
        private string _PrjProperty;
        private string _PrjReadOne;
        private string _ProgAgent;
        private DateTime? _ProjApplyDate;
        private DateTime? _ProjApprovalDate;
        private string _ProjElseRequest;
        private int? _ProjFlowSate;
        private string _ProjInfoOrigin;
        private int? _ProjPeopleAge;
        private string _ProjPeopleDep;
        private string _ProjPeopleDuty;
        private string _ProjPeopleName;
        private string _ProjPeopleSex;
        private string _ProjPeopleTel;
        private int? _ProjRegistDeadline;
        private DateTime? _ProjStartDate;
        private string _ProjStartRemark;
        private DateTime? _ProjTenderAnswerDate;
        private DateTime? _ProjTenderBeginDate;
        private string _ProjTenderContent;
        private string _ProjTenderCostContent;
        private DateTime? _ProjTenderDate;
        private decimal? _ProjTenderEarnestMoney;
        private string _ProjTenderPayWay;
        private string _ProjTenderRemark;
        private string _Province;
        private DateTime? _QualificationFailData;
        private string _QualificationFailReason;
        private decimal _QualificationMargin;
        private DateTime? _QualificationPassDate;
        private string _QualificationPassReason;
        private string _QualificationReadOne;
        private int? _SetUpFlowState;
        private DateTime? _SuccessBidDate;
        private decimal? _SuccessBidPrice;
        private string _SuccessBidRemark;
        private string _TechnicalLeaderRequire;
        private string _Telephone;
        private string _TenderAppraiseMethod;
        private decimal? _TenderAverage;
        private decimal? _TenderCeilingPrice;
        private DateTime? _TenderProspect;
        private decimal? _TenderQuote;
        private string _TenderReadOne;
        private string _TenderUnit;
        private decimal? _WithholdingTaxRate;

        public static PT_PrjInfo_ZTB_Detail CreatePT_PrjInfo_ZTB_Detail(Guid prjGuid, bool isTender, decimal qualificationMargin)
        {
            return new PT_PrjInfo_ZTB_Detail { PrjGuid = prjGuid, IsTender = isTender, QualificationMargin = qualificationMargin };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? ActualRunDate
        {
            get
            {
                return this._ActualRunDate;
            }
            set
            {
                this.ReportPropertyChanging("ActualRunDate");
                this._ActualRunDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ActualRunDate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string AfforestArea
        {
            get
            {
                return this._AfforestArea;
            }
            set
            {
                this.ReportPropertyChanging("AfforestArea");
                this._AfforestArea = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("AfforestArea");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public int? BuildingTypeNo
        {
            get
            {
                return this._BuildingTypeNo;
            }
            set
            {
                this.ReportPropertyChanging("BuildingTypeNo");
                this._BuildingTypeNo = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("BuildingTypeNo");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string BusinessManager
        {
            get
            {
                return this._BusinessManager;
            }
            set
            {
                this.ReportPropertyChanging("BusinessManager");
                this._BusinessManager = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("BusinessManager");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string City
        {
            get
            {
                return this._City;
            }
            set
            {
                this.ReportPropertyChanging("City");
                this._City = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("City");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? CompletedDate
        {
            get
            {
                return this._CompletedDate;
            }
            set
            {
                this.ReportPropertyChanging("CompletedDate");
                this._CompletedDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CompletedDate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public int? CompletedFlowState
        {
            get
            {
                return this._CompletedFlowState;
            }
            set
            {
                this.ReportPropertyChanging("CompletedFlowState");
                this._CompletedFlowState = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CompletedFlowState");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string CompletedNote
        {
            get
            {
                return this._CompletedNote;
            }
            set
            {
                this.ReportPropertyChanging("CompletedNote");
                this._CompletedNote = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("CompletedNote");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public decimal? ElseMargin
        {
            get
            {
                return this._ElseMargin;
            }
            set
            {
                this.ReportPropertyChanging("ElseMargin");
                this._ElseMargin = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ElseMargin");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string EngineeringEstimates
        {
            get
            {
                return this._EngineeringEstimates;
            }
            set
            {
                this.ReportPropertyChanging("EngineeringEstimates");
                this._EngineeringEstimates = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("EngineeringEstimates");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string EngineeringSubType
        {
            get
            {
                return this._EngineeringSubType;
            }
            set
            {
                this.ReportPropertyChanging("EngineeringSubType");
                this._EngineeringSubType = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("EngineeringSubType");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string EngineeringType
        {
            get
            {
                return this._EngineeringType;
            }
            set
            {
                this.ReportPropertyChanging("EngineeringType");
                this._EngineeringType = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("EngineeringType");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? ForecastProfitRate
        {
            get
            {
                return this._ForecastProfitRate;
            }
            set
            {
                this.ReportPropertyChanging("ForecastProfitRate");
                this._ForecastProfitRate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ForecastProfitRate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Grade
        {
            get
            {
                return this._Grade;
            }
            set
            {
                this.ReportPropertyChanging("Grade");
                this._Grade = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Grade");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? InputDate
        {
            get
            {
                return this._InputDate;
            }
            set
            {
                this.ReportPropertyChanging("InputDate");
                this._InputDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("InputDate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string InputUser
        {
            get
            {
                return this._InputUser;
            }
            set
            {
                this.ReportPropertyChanging("InputUser");
                this._InputUser = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("InputUser");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsTender
        {
            get
            {
                return this._IsTender;
            }
            set
            {
                this.ReportPropertyChanging("IsTender");
                this._IsTender = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsTender");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? ManagementMargin
        {
            get
            {
                return this._ManagementMargin;
            }
            set
            {
                this.ReportPropertyChanging("ManagementMargin");
                this._ManagementMargin = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ManagementMargin");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? MemberFlowState
        {
            get
            {
                return this._MemberFlowState;
            }
            set
            {
                this.ReportPropertyChanging("MemberFlowState");
                this._MemberFlowState = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("MemberFlowState");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? MigrantQualityMarginRate
        {
            get
            {
                return this._MigrantQualityMarginRate;
            }
            set
            {
                this.ReportPropertyChanging("MigrantQualityMarginRate");
                this._MigrantQualityMarginRate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("MigrantQualityMarginRate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? OutBidDate
        {
            get
            {
                return this._OutBidDate;
            }
            set
            {
                this.ReportPropertyChanging("OutBidDate");
                this._OutBidDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("OutBidDate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public bool? OutBidIsReturn
        {
            get
            {
                return this._OutBidIsReturn;
            }
            set
            {
                this.ReportPropertyChanging("OutBidIsReturn");
                this._OutBidIsReturn = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("OutBidIsReturn");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string OutBidRemark
        {
            get
            {
                return this._OutBidRemark;
            }
            set
            {
                this.ReportPropertyChanging("OutBidRemark");
                this._OutBidRemark = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("OutBidRemark");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string OwnerAddress
        {
            get
            {
                return this._OwnerAddress;
            }
            set
            {
                this.ReportPropertyChanging("OwnerAddress");
                this._OwnerAddress = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("OwnerAddress");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string OwnerLinkMan
        {
            get
            {
                return this._OwnerLinkMan;
            }
            set
            {
                this.ReportPropertyChanging("OwnerLinkMan");
                this._OwnerLinkMan = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("OwnerLinkMan");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string OwnerLinkPhone
        {
            get
            {
                return this._OwnerLinkPhone;
            }
            set
            {
                this.ReportPropertyChanging("OwnerLinkPhone");
                this._OwnerLinkPhone = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("OwnerLinkPhone");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ParkArea
        {
            get
            {
                return this._ParkArea;
            }
            set
            {
                this.ReportPropertyChanging("ParkArea");
                this._ParkArea = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ParkArea");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? PerformanceBond
        {
            get
            {
                return this._PerformanceBond;
            }
            set
            {
                this.ReportPropertyChanging("PerformanceBond");
                this._PerformanceBond = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PerformanceBond");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string PrequalificationRequire
        {
            get
            {
                return this._PrequalificationRequire;
            }
            set
            {
                this.ReportPropertyChanging("PrequalificationRequire");
                this._PrequalificationRequire = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrequalificationRequire");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PrjApprovalOf
        {
            get
            {
                return this._PrjApprovalOf;
            }
            set
            {
                this.ReportPropertyChanging("PrjApprovalOf");
                this._PrjApprovalOf = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjApprovalOf");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string PrjDutyPerson
        {
            get
            {
                return this._PrjDutyPerson;
            }
            set
            {
                this.ReportPropertyChanging("PrjDutyPerson");
                this._PrjDutyPerson = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjDutyPerson");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PrjFundWorkable
        {
            get
            {
                return this._PrjFundWorkable;
            }
            set
            {
                this.ReportPropertyChanging("PrjFundWorkable");
                this._PrjFundWorkable = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjFundWorkable");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public Guid PrjGuid
        {
            get
            {
                return this._PrjGuid;
            }
            set
            {
                if (this._PrjGuid != value)
                {
                    this.ReportPropertyChanging("PrjGuid");
                    this._PrjGuid = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("PrjGuid");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string PrjManagerRequire
        {
            get
            {
                return this._PrjManagerRequire;
            }
            set
            {
                this.ReportPropertyChanging("PrjManagerRequire");
                this._PrjManagerRequire = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjManagerRequire");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PrjProperty
        {
            get
            {
                return this._PrjProperty;
            }
            set
            {
                this.ReportPropertyChanging("PrjProperty");
                this._PrjProperty = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjProperty");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string PrjReadOne
        {
            get
            {
                return this._PrjReadOne;
            }
            set
            {
                this.ReportPropertyChanging("PrjReadOne");
                this._PrjReadOne = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjReadOne");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ProgAgent
        {
            get
            {
                return this._ProgAgent;
            }
            set
            {
                this.ReportPropertyChanging("ProgAgent");
                this._ProgAgent = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ProgAgent");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? ProjApplyDate
        {
            get
            {
                return this._ProjApplyDate;
            }
            set
            {
                this.ReportPropertyChanging("ProjApplyDate");
                this._ProjApplyDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ProjApplyDate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? ProjApprovalDate
        {
            get
            {
                return this._ProjApprovalDate;
            }
            set
            {
                this.ReportPropertyChanging("ProjApprovalDate");
                this._ProjApprovalDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ProjApprovalDate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ProjElseRequest
        {
            get
            {
                return this._ProjElseRequest;
            }
            set
            {
                this.ReportPropertyChanging("ProjElseRequest");
                this._ProjElseRequest = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ProjElseRequest");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public int? ProjFlowSate
        {
            get
            {
                return this._ProjFlowSate;
            }
            set
            {
                this.ReportPropertyChanging("ProjFlowSate");
                this._ProjFlowSate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ProjFlowSate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string ProjInfoOrigin
        {
            get
            {
                return this._ProjInfoOrigin;
            }
            set
            {
                this.ReportPropertyChanging("ProjInfoOrigin");
                this._ProjInfoOrigin = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ProjInfoOrigin");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public int? ProjPeopleAge
        {
            get
            {
                return this._ProjPeopleAge;
            }
            set
            {
                this.ReportPropertyChanging("ProjPeopleAge");
                this._ProjPeopleAge = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ProjPeopleAge");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ProjPeopleDep
        {
            get
            {
                return this._ProjPeopleDep;
            }
            set
            {
                this.ReportPropertyChanging("ProjPeopleDep");
                this._ProjPeopleDep = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ProjPeopleDep");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ProjPeopleDuty
        {
            get
            {
                return this._ProjPeopleDuty;
            }
            set
            {
                this.ReportPropertyChanging("ProjPeopleDuty");
                this._ProjPeopleDuty = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ProjPeopleDuty");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ProjPeopleName
        {
            get
            {
                return this._ProjPeopleName;
            }
            set
            {
                this.ReportPropertyChanging("ProjPeopleName");
                this._ProjPeopleName = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ProjPeopleName");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ProjPeopleSex
        {
            get
            {
                return this._ProjPeopleSex;
            }
            set
            {
                this.ReportPropertyChanging("ProjPeopleSex");
                this._ProjPeopleSex = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ProjPeopleSex");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ProjPeopleTel
        {
            get
            {
                return this._ProjPeopleTel;
            }
            set
            {
                this.ReportPropertyChanging("ProjPeopleTel");
                this._ProjPeopleTel = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ProjPeopleTel");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? ProjRegistDeadline
        {
            get
            {
                return this._ProjRegistDeadline;
            }
            set
            {
                this.ReportPropertyChanging("ProjRegistDeadline");
                this._ProjRegistDeadline = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ProjRegistDeadline");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? ProjStartDate
        {
            get
            {
                return this._ProjStartDate;
            }
            set
            {
                this.ReportPropertyChanging("ProjStartDate");
                this._ProjStartDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ProjStartDate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ProjStartRemark
        {
            get
            {
                return this._ProjStartRemark;
            }
            set
            {
                this.ReportPropertyChanging("ProjStartRemark");
                this._ProjStartRemark = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ProjStartRemark");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? ProjTenderAnswerDate
        {
            get
            {
                return this._ProjTenderAnswerDate;
            }
            set
            {
                this.ReportPropertyChanging("ProjTenderAnswerDate");
                this._ProjTenderAnswerDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ProjTenderAnswerDate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? ProjTenderBeginDate
        {
            get
            {
                return this._ProjTenderBeginDate;
            }
            set
            {
                this.ReportPropertyChanging("ProjTenderBeginDate");
                this._ProjTenderBeginDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ProjTenderBeginDate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ProjTenderContent
        {
            get
            {
                return this._ProjTenderContent;
            }
            set
            {
                this.ReportPropertyChanging("ProjTenderContent");
                this._ProjTenderContent = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ProjTenderContent");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string ProjTenderCostContent
        {
            get
            {
                return this._ProjTenderCostContent;
            }
            set
            {
                this.ReportPropertyChanging("ProjTenderCostContent");
                this._ProjTenderCostContent = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ProjTenderCostContent");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? ProjTenderDate
        {
            get
            {
                return this._ProjTenderDate;
            }
            set
            {
                this.ReportPropertyChanging("ProjTenderDate");
                this._ProjTenderDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ProjTenderDate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? ProjTenderEarnestMoney
        {
            get
            {
                return this._ProjTenderEarnestMoney;
            }
            set
            {
                this.ReportPropertyChanging("ProjTenderEarnestMoney");
                this._ProjTenderEarnestMoney = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ProjTenderEarnestMoney");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ProjTenderPayWay
        {
            get
            {
                return this._ProjTenderPayWay;
            }
            set
            {
                this.ReportPropertyChanging("ProjTenderPayWay");
                this._ProjTenderPayWay = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ProjTenderPayWay");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string ProjTenderRemark
        {
            get
            {
                return this._ProjTenderRemark;
            }
            set
            {
                this.ReportPropertyChanging("ProjTenderRemark");
                this._ProjTenderRemark = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ProjTenderRemark");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Province
        {
            get
            {
                return this._Province;
            }
            set
            {
                this.ReportPropertyChanging("Province");
                this._Province = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Province");
            }
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__PT_PrjInf__PrjGu__0313E4B1", "PT_PrjInfo_Kind"), SoapIgnore, DataMember]
        public EntityCollection<cn.justwin.DAL.PT_PrjInfo_Kind> PT_PrjInfo_Kind
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.PT_PrjInfo_Kind>("Pm2Model.FK__PT_PrjInf__PrjGu__0313E4B1", "PT_PrjInfo_Kind");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.PT_PrjInfo_Kind>("Pm2Model.FK__PT_PrjInf__PrjGu__0313E4B1", "PT_PrjInfo_Kind", value);
                }
            }
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__PT_PrjInf__PrjGu__06E47595", "PT_PrjInfo_Rank"), XmlIgnore, SoapIgnore, DataMember]
        public EntityCollection<cn.justwin.DAL.PT_PrjInfo_Rank> PT_PrjInfo_Rank
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.PT_PrjInfo_Rank>("Pm2Model.FK__PT_PrjInf__PrjGu__06E47595", "PT_PrjInfo_Rank");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.PT_PrjInfo_Rank>("Pm2Model.FK__PT_PrjInf__PrjGu__06E47595", "PT_PrjInfo_Rank", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? QualificationFailData
        {
            get
            {
                return this._QualificationFailData;
            }
            set
            {
                this.ReportPropertyChanging("QualificationFailData");
                this._QualificationFailData = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("QualificationFailData");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string QualificationFailReason
        {
            get
            {
                return this._QualificationFailReason;
            }
            set
            {
                this.ReportPropertyChanging("QualificationFailReason");
                this._QualificationFailReason = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("QualificationFailReason");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public decimal QualificationMargin
        {
            get
            {
                return this._QualificationMargin;
            }
            set
            {
                this.ReportPropertyChanging("QualificationMargin");
                this._QualificationMargin = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("QualificationMargin");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? QualificationPassDate
        {
            get
            {
                return this._QualificationPassDate;
            }
            set
            {
                this.ReportPropertyChanging("QualificationPassDate");
                this._QualificationPassDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("QualificationPassDate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string QualificationPassReason
        {
            get
            {
                return this._QualificationPassReason;
            }
            set
            {
                this.ReportPropertyChanging("QualificationPassReason");
                this._QualificationPassReason = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("QualificationPassReason");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string QualificationReadOne
        {
            get
            {
                return this._QualificationReadOne;
            }
            set
            {
                this.ReportPropertyChanging("QualificationReadOne");
                this._QualificationReadOne = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("QualificationReadOne");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? SetUpFlowState
        {
            get
            {
                return this._SetUpFlowState;
            }
            set
            {
                this.ReportPropertyChanging("SetUpFlowState");
                this._SetUpFlowState = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("SetUpFlowState");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? SuccessBidDate
        {
            get
            {
                return this._SuccessBidDate;
            }
            set
            {
                this.ReportPropertyChanging("SuccessBidDate");
                this._SuccessBidDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("SuccessBidDate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public decimal? SuccessBidPrice
        {
            get
            {
                return this._SuccessBidPrice;
            }
            set
            {
                this.ReportPropertyChanging("SuccessBidPrice");
                this._SuccessBidPrice = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("SuccessBidPrice");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string SuccessBidRemark
        {
            get
            {
                return this._SuccessBidRemark;
            }
            set
            {
                this.ReportPropertyChanging("SuccessBidRemark");
                this._SuccessBidRemark = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("SuccessBidRemark");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string TechnicalLeaderRequire
        {
            get
            {
                return this._TechnicalLeaderRequire;
            }
            set
            {
                this.ReportPropertyChanging("TechnicalLeaderRequire");
                this._TechnicalLeaderRequire = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("TechnicalLeaderRequire");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Telephone
        {
            get
            {
                return this._Telephone;
            }
            set
            {
                this.ReportPropertyChanging("Telephone");
                this._Telephone = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Telephone");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string TenderAppraiseMethod
        {
            get
            {
                return this._TenderAppraiseMethod;
            }
            set
            {
                this.ReportPropertyChanging("TenderAppraiseMethod");
                this._TenderAppraiseMethod = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("TenderAppraiseMethod");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? TenderAverage
        {
            get
            {
                return this._TenderAverage;
            }
            set
            {
                this.ReportPropertyChanging("TenderAverage");
                this._TenderAverage = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("TenderAverage");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? TenderCeilingPrice
        {
            get
            {
                return this._TenderCeilingPrice;
            }
            set
            {
                this.ReportPropertyChanging("TenderCeilingPrice");
                this._TenderCeilingPrice = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("TenderCeilingPrice");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? TenderProspect
        {
            get
            {
                return this._TenderProspect;
            }
            set
            {
                this.ReportPropertyChanging("TenderProspect");
                this._TenderProspect = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("TenderProspect");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? TenderQuote
        {
            get
            {
                return this._TenderQuote;
            }
            set
            {
                this.ReportPropertyChanging("TenderQuote");
                this._TenderQuote = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("TenderQuote");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string TenderReadOne
        {
            get
            {
                return this._TenderReadOne;
            }
            set
            {
                this.ReportPropertyChanging("TenderReadOne");
                this._TenderReadOne = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("TenderReadOne");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string TenderUnit
        {
            get
            {
                return this._TenderUnit;
            }
            set
            {
                this.ReportPropertyChanging("TenderUnit");
                this._TenderUnit = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("TenderUnit");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public decimal? WithholdingTaxRate
        {
            get
            {
                return this._WithholdingTaxRate;
            }
            set
            {
                this.ReportPropertyChanging("WithholdingTaxRate");
                this._WithholdingTaxRate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("WithholdingTaxRate");
            }
        }
    }
}

