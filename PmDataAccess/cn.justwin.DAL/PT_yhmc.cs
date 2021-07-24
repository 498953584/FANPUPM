namespace cn.justwin.DAL
{
    using System;
    using System.ComponentModel;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="PT_yhmc")]
    public class PT_yhmc : EntityObject
    {
        private string _Address;
        private int? _Age;
        private DateTime? _BeginWorkDate;
        private DateTime? _Birthday;
        private string _c_sfyx;
        private int? _ClassID;
        private string _CommunicateAddress;
        private string _ComputeLevel;
        private DateTime? _conEndDate;
        private string _DriveLevel;
        private string _EducationalBackground;
        private string _EndowmentInsurance;
        private string _EnglishLevel;
        private DateTime? _EnterCorpDate;
        private decimal? _ExpectationSalary;
        private DateTime? _FormalDate;
        private string _GraduateSchool;
        private string _HousingAccumulationFund;
        private int? _i_bmdm;
        private int? _I_DUTYID;
        private int? _i_xh;
        private string _IDCard;
        private string _InjuryInsurance;
        private string _Introducer;
        private bool _IsChargeMan;
        private string _JoinCorpMode;
        private DateTime? _JoinPartyDate;
        private DateTime? _leavetime;
        private string _Level;
        private string _Marriage;
        private string _MedicareInsurance;
        private string _MobilePhoneCode;
        private string _Nation;
        private string _OtherDepts;
        private string _OtherDutyIDs;
        private string _PastPerformance;
        private string _PersonSuddennessInsurance;
        private string _PoliticsFace;
        private int? _PositionLevel;
        private string _PostAndCompetency;
        private string _PostAndRank;
        private string _Postcode;
        private string _rdeNature;
        private string _RegisteredPlace;
        private string _RelationCorp;
        private string _RTXID;
        private string _Sex;
        private string _Specialty;
        private int? _State;
        private string _Stature;
        private int? _SuperordinateDuty;
        private string _Tel;
        private string _ucmConcern;
        private string _ucmTel;
        private string _UnemploymentInsurance;
        private string _urgentCellMan;
        private string _userCode;
        private string _UserPhoto;
        private string _v_xm;
        private string _v_yhdm;

        public static PT_yhmc CreatePT_yhmc(string v_yhdm, bool isChargeMan)
        {
            return new PT_yhmc { v_yhdm = v_yhdm, IsChargeMan = isChargeMan };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Address
        {
            get
            {
                return this._Address;
            }
            set
            {
                this.ReportPropertyChanging("Address");
                this._Address = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Address");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? Age
        {
            get
            {
                return this._Age;
            }
            set
            {
                this.ReportPropertyChanging("Age");
                this._Age = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Age");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? BeginWorkDate
        {
            get
            {
                return this._BeginWorkDate;
            }
            set
            {
                this.ReportPropertyChanging("BeginWorkDate");
                this._BeginWorkDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("BeginWorkDate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? Birthday
        {
            get
            {
                return this._Birthday;
            }
            set
            {
                this.ReportPropertyChanging("Birthday");
                this._Birthday = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Birthday");
            }
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__Bud_TaskC__Input__292EA0A1", "Bud_TaskChange"), SoapIgnore, DataMember]
        public EntityCollection<cn.justwin.DAL.Bud_TaskChange> Bud_TaskChange
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.Bud_TaskChange>("Pm2Model.FK__Bud_TaskC__Input__292EA0A1", "Bud_TaskChange");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.Bud_TaskChange>("Pm2Model.FK__Bud_TaskC__Input__292EA0A1", "Bud_TaskChange", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string c_sfyx
        {
            get
            {
                return this._c_sfyx;
            }
            set
            {
                this.ReportPropertyChanging("c_sfyx");
                this._c_sfyx = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("c_sfyx");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? ClassID
        {
            get
            {
                return this._ClassID;
            }
            set
            {
                this.ReportPropertyChanging("ClassID");
                this._ClassID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ClassID");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string CommunicateAddress
        {
            get
            {
                return this._CommunicateAddress;
            }
            set
            {
                this.ReportPropertyChanging("CommunicateAddress");
                this._CommunicateAddress = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("CommunicateAddress");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ComputeLevel
        {
            get
            {
                return this._ComputeLevel;
            }
            set
            {
                this.ReportPropertyChanging("ComputeLevel");
                this._ComputeLevel = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ComputeLevel");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? conEndDate
        {
            get
            {
                return this._conEndDate;
            }
            set
            {
                this.ReportPropertyChanging("conEndDate");
                this._conEndDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("conEndDate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string DriveLevel
        {
            get
            {
                return this._DriveLevel;
            }
            set
            {
                this.ReportPropertyChanging("DriveLevel");
                this._DriveLevel = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("DriveLevel");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string EducationalBackground
        {
            get
            {
                return this._EducationalBackground;
            }
            set
            {
                this.ReportPropertyChanging("EducationalBackground");
                this._EducationalBackground = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("EducationalBackground");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string EndowmentInsurance
        {
            get
            {
                return this._EndowmentInsurance;
            }
            set
            {
                this.ReportPropertyChanging("EndowmentInsurance");
                this._EndowmentInsurance = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("EndowmentInsurance");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string EnglishLevel
        {
            get
            {
                return this._EnglishLevel;
            }
            set
            {
                this.ReportPropertyChanging("EnglishLevel");
                this._EnglishLevel = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("EnglishLevel");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? EnterCorpDate
        {
            get
            {
                return this._EnterCorpDate;
            }
            set
            {
                this.ReportPropertyChanging("EnterCorpDate");
                this._EnterCorpDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("EnterCorpDate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public decimal? ExpectationSalary
        {
            get
            {
                return this._ExpectationSalary;
            }
            set
            {
                this.ReportPropertyChanging("ExpectationSalary");
                this._ExpectationSalary = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("ExpectationSalary");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? FormalDate
        {
            get
            {
                return this._FormalDate;
            }
            set
            {
                this.ReportPropertyChanging("FormalDate");
                this._FormalDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("FormalDate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string GraduateSchool
        {
            get
            {
                return this._GraduateSchool;
            }
            set
            {
                this.ReportPropertyChanging("GraduateSchool");
                this._GraduateSchool = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("GraduateSchool");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string HousingAccumulationFund
        {
            get
            {
                return this._HousingAccumulationFund;
            }
            set
            {
                this.ReportPropertyChanging("HousingAccumulationFund");
                this._HousingAccumulationFund = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("HousingAccumulationFund");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? i_bmdm
        {
            get
            {
                return this._i_bmdm;
            }
            set
            {
                this.ReportPropertyChanging("i_bmdm");
                this._i_bmdm = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("i_bmdm");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public int? I_DUTYID
        {
            get
            {
                return this._I_DUTYID;
            }
            set
            {
                this.ReportPropertyChanging("I_DUTYID");
                this._I_DUTYID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("I_DUTYID");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? i_xh
        {
            get
            {
                return this._i_xh;
            }
            set
            {
                this.ReportPropertyChanging("i_xh");
                this._i_xh = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("i_xh");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string IDCard
        {
            get
            {
                return this._IDCard;
            }
            set
            {
                this.ReportPropertyChanging("IDCard");
                this._IDCard = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("IDCard");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string InjuryInsurance
        {
            get
            {
                return this._InjuryInsurance;
            }
            set
            {
                this.ReportPropertyChanging("InjuryInsurance");
                this._InjuryInsurance = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("InjuryInsurance");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Introducer
        {
            get
            {
                return this._Introducer;
            }
            set
            {
                this.ReportPropertyChanging("Introducer");
                this._Introducer = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Introducer");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public bool IsChargeMan
        {
            get
            {
                return this._IsChargeMan;
            }
            set
            {
                this.ReportPropertyChanging("IsChargeMan");
                this._IsChargeMan = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsChargeMan");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string JoinCorpMode
        {
            get
            {
                return this._JoinCorpMode;
            }
            set
            {
                this.ReportPropertyChanging("JoinCorpMode");
                this._JoinCorpMode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("JoinCorpMode");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? JoinPartyDate
        {
            get
            {
                return this._JoinPartyDate;
            }
            set
            {
                this.ReportPropertyChanging("JoinPartyDate");
                this._JoinPartyDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("JoinPartyDate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? leavetime
        {
            get
            {
                return this._leavetime;
            }
            set
            {
                this.ReportPropertyChanging("leavetime");
                this._leavetime = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("leavetime");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Level
        {
            get
            {
                return this._Level;
            }
            set
            {
                this.ReportPropertyChanging("Level");
                this._Level = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Level");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Marriage
        {
            get
            {
                return this._Marriage;
            }
            set
            {
                this.ReportPropertyChanging("Marriage");
                this._Marriage = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Marriage");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string MedicareInsurance
        {
            get
            {
                return this._MedicareInsurance;
            }
            set
            {
                this.ReportPropertyChanging("MedicareInsurance");
                this._MedicareInsurance = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("MedicareInsurance");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string MobilePhoneCode
        {
            get
            {
                return this._MobilePhoneCode;
            }
            set
            {
                this.ReportPropertyChanging("MobilePhoneCode");
                this._MobilePhoneCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("MobilePhoneCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Nation
        {
            get
            {
                return this._Nation;
            }
            set
            {
                this.ReportPropertyChanging("Nation");
                this._Nation = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Nation");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string OtherDepts
        {
            get
            {
                return this._OtherDepts;
            }
            set
            {
                this.ReportPropertyChanging("OtherDepts");
                this._OtherDepts = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("OtherDepts");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string OtherDutyIDs
        {
            get
            {
                return this._OtherDutyIDs;
            }
            set
            {
                this.ReportPropertyChanging("OtherDutyIDs");
                this._OtherDutyIDs = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("OtherDutyIDs");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PastPerformance
        {
            get
            {
                return this._PastPerformance;
            }
            set
            {
                this.ReportPropertyChanging("PastPerformance");
                this._PastPerformance = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PastPerformance");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string PersonSuddennessInsurance
        {
            get
            {
                return this._PersonSuddennessInsurance;
            }
            set
            {
                this.ReportPropertyChanging("PersonSuddennessInsurance");
                this._PersonSuddennessInsurance = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PersonSuddennessInsurance");
            }
        }

        [SoapIgnore, XmlIgnore, DataMember, EdmRelationshipNavigationProperty("Pm2Model", "FK_plus_progress_InputUser", "plus_progress")]
        public EntityCollection<cn.justwin.DAL.plus_progress> plus_progress
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.plus_progress>("Pm2Model.FK_plus_progress_InputUser", "plus_progress");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.plus_progress>("Pm2Model.FK_plus_progress_InputUser", "plus_progress", value);
                }
            }
        }

        [SoapIgnore, XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK_privilege_UserCode", "plus_progress_privilege"), DataMember]
        public EntityCollection<cn.justwin.DAL.plus_progress_privilege> plus_progress_privilege
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.plus_progress_privilege>("Pm2Model.FK_privilege_UserCode", "plus_progress_privilege");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.plus_progress_privilege>("Pm2Model.FK_privilege_UserCode", "plus_progress_privilege", value);
                }
            }
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK_progress_version_InputUser", "plus_progress_version"), SoapIgnore, DataMember]
        public EntityCollection<cn.justwin.DAL.plus_progress_version> plus_progress_version
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.plus_progress_version>("Pm2Model.FK_progress_version_InputUser", "plus_progress_version");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.plus_progress_version>("Pm2Model.FK_progress_version_InputUser", "plus_progress_version", value);
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string PoliticsFace
        {
            get
            {
                return this._PoliticsFace;
            }
            set
            {
                this.ReportPropertyChanging("PoliticsFace");
                this._PoliticsFace = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PoliticsFace");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? PositionLevel
        {
            get
            {
                return this._PositionLevel;
            }
            set
            {
                this.ReportPropertyChanging("PositionLevel");
                this._PositionLevel = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PositionLevel");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PostAndCompetency
        {
            get
            {
                return this._PostAndCompetency;
            }
            set
            {
                this.ReportPropertyChanging("PostAndCompetency");
                this._PostAndCompetency = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PostAndCompetency");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string PostAndRank
        {
            get
            {
                return this._PostAndRank;
            }
            set
            {
                this.ReportPropertyChanging("PostAndRank");
                this._PostAndRank = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PostAndRank");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Postcode
        {
            get
            {
                return this._Postcode;
            }
            set
            {
                this.ReportPropertyChanging("Postcode");
                this._Postcode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Postcode");
            }
        }

        [XmlIgnore, DataMember, EdmRelationshipNavigationProperty("Pm2Model", "用户_部门", "PT_d_bm"), SoapIgnore]
        public cn.justwin.DAL.PT_d_bm PT_d_bm
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_d_bm>("Pm2Model.用户_部门", "PT_d_bm").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_d_bm>("Pm2Model.用户_部门", "PT_d_bm").Value = value;
            }
        }

        [DataMember, Browsable(false)]
        public EntityReference<cn.justwin.DAL.PT_d_bm> PT_d_bmReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<cn.justwin.DAL.PT_d_bm>("Pm2Model.用户_部门", "PT_d_bm");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<cn.justwin.DAL.PT_d_bm>("Pm2Model.用户_部门", "PT_d_bm", value);
                }
            }
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__PT_Prj_Co__Input__5EE0A5DC", "PT_Prj_Completed"), SoapIgnore, DataMember]
        public EntityCollection<cn.justwin.DAL.PT_Prj_Completed> PT_Prj_Completed
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.PT_Prj_Completed>("Pm2Model.FK__PT_Prj_Co__Input__5EE0A5DC", "PT_Prj_Completed");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.PT_Prj_Completed>("Pm2Model.FK__PT_Prj_Co__Input__5EE0A5DC", "PT_Prj_Completed", value);
                }
            }
        }

        [EdmRelationshipNavigationProperty("Pm2Model", "FK__PT_Prj_Co__Input__6B467CC1", "PT_Prj_Completed_Detail"), XmlIgnore, SoapIgnore, DataMember]
        public EntityCollection<cn.justwin.DAL.PT_Prj_Completed_Detail> PT_Prj_Completed_Detail
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.PT_Prj_Completed_Detail>("Pm2Model.FK__PT_Prj_Co__Input__6B467CC1", "PT_Prj_Completed_Detail");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.PT_Prj_Completed_Detail>("Pm2Model.FK__PT_Prj_Co__Input__6B467CC1", "PT_Prj_Completed_Detail", value);
                }
            }
        }

        [XmlIgnore, EdmRelationshipNavigationProperty("Pm2Model", "FK__PT_PrjMem__Membe__40071901", "PT_PrjMember"), SoapIgnore, DataMember]
        public EntityCollection<cn.justwin.DAL.PT_PrjMember> PT_PrjMember
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<cn.justwin.DAL.PT_PrjMember>("Pm2Model.FK__PT_PrjMem__Membe__40071901", "PT_PrjMember");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<cn.justwin.DAL.PT_PrjMember>("Pm2Model.FK__PT_PrjMem__Membe__40071901", "PT_PrjMember", value);
                }
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string rdeNature
        {
            get
            {
                return this._rdeNature;
            }
            set
            {
                this.ReportPropertyChanging("rdeNature");
                this._rdeNature = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("rdeNature");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string RegisteredPlace
        {
            get
            {
                return this._RegisteredPlace;
            }
            set
            {
                this.ReportPropertyChanging("RegisteredPlace");
                this._RegisteredPlace = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("RegisteredPlace");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string RelationCorp
        {
            get
            {
                return this._RelationCorp;
            }
            set
            {
                this.ReportPropertyChanging("RelationCorp");
                this._RelationCorp = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("RelationCorp");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string RTXID
        {
            get
            {
                return this._RTXID;
            }
            set
            {
                this.ReportPropertyChanging("RTXID");
                this._RTXID = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("RTXID");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Sex
        {
            get
            {
                return this._Sex;
            }
            set
            {
                this.ReportPropertyChanging("Sex");
                this._Sex = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Sex");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Specialty
        {
            get
            {
                return this._Specialty;
            }
            set
            {
                this.ReportPropertyChanging("Specialty");
                this._Specialty = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Specialty");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public int? State
        {
            get
            {
                return this._State;
            }
            set
            {
                this.ReportPropertyChanging("State");
                this._State = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("State");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Stature
        {
            get
            {
                return this._Stature;
            }
            set
            {
                this.ReportPropertyChanging("Stature");
                this._Stature = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Stature");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? SuperordinateDuty
        {
            get
            {
                return this._SuperordinateDuty;
            }
            set
            {
                this.ReportPropertyChanging("SuperordinateDuty");
                this._SuperordinateDuty = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("SuperordinateDuty");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Tel
        {
            get
            {
                return this._Tel;
            }
            set
            {
                this.ReportPropertyChanging("Tel");
                this._Tel = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Tel");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ucmConcern
        {
            get
            {
                return this._ucmConcern;
            }
            set
            {
                this.ReportPropertyChanging("ucmConcern");
                this._ucmConcern = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ucmConcern");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string ucmTel
        {
            get
            {
                return this._ucmTel;
            }
            set
            {
                this.ReportPropertyChanging("ucmTel");
                this._ucmTel = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ucmTel");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string UnemploymentInsurance
        {
            get
            {
                return this._UnemploymentInsurance;
            }
            set
            {
                this.ReportPropertyChanging("UnemploymentInsurance");
                this._UnemploymentInsurance = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UnemploymentInsurance");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string urgentCellMan
        {
            get
            {
                return this._urgentCellMan;
            }
            set
            {
                this.ReportPropertyChanging("urgentCellMan");
                this._urgentCellMan = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("urgentCellMan");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string userCode
        {
            get
            {
                return this._userCode;
            }
            set
            {
                this.ReportPropertyChanging("userCode");
                this._userCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("userCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string UserPhoto
        {
            get
            {
                return this._UserPhoto;
            }
            set
            {
                this.ReportPropertyChanging("UserPhoto");
                this._UserPhoto = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UserPhoto");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string v_xm
        {
            get
            {
                return this._v_xm;
            }
            set
            {
                this.ReportPropertyChanging("v_xm");
                this._v_xm = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("v_xm");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string v_yhdm
        {
            get
            {
                return this._v_yhdm;
            }
            set
            {
                if (this._v_yhdm != value)
                {
                    this.ReportPropertyChanging("v_yhdm");
                    this._v_yhdm = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("v_yhdm");
                }
            }
        }
    }
}

