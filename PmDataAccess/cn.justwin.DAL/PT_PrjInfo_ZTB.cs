namespace cn.justwin.DAL
{
    using System;
    using System.Data.Objects.DataClasses;
    using System.Runtime.Serialization;

    [Serializable, DataContract(IsReference=true), EdmEntityType(NamespaceName="Pm2Model", Name="PT_PrjInfo_ZTB")]
    public class PT_PrjInfo_ZTB : EntityObject
    {
        private string _Area;
        private string _BudgetWay;
        private string _BuildingArea;
        private string _BuildingType;
        private string _ComputeClass;
        private string _ContractSum;
        private string _ContractWay;
        private string _Counsellor;
        private string _Designer;
        private string _Duration;
        private DateTime? _EndDate;
        private string _FileName;
        private string _FileURL;
        private string _Inspector;
        private string _KeyPart;
        private string _LinkMan;
        private Guid? _MarketInfoGuid;
        private string _OtherStatement;
        private string _Owner;
        private int? _OwnerCode;
        private string _PayCondition;
        private string _PayWay;
        private string _PrjCode;
        private double? _PrjCost;
        private string _PrjFundInfo;
        private Guid _PrjGuid;
        private string _PrjInfo;
        private string _PrjKindClass;
        private string _PrjManager;
        private string _PrjName;
        private string _PrjPlace;
        private int? _PrjState;
        private string _QualityClass;
        private string _Rank;
        private string _RationClass;
        private string _Remark;
        private DateTime? _StartDate;
        private string _TenderWay;
        private string _TotalHouseNum;
        private string _UndergroundArea;
        private string _UsegrounArea;
        private string _WorkUnit;

        public static PT_PrjInfo_ZTB CreatePT_PrjInfo_ZTB(string prjCode, Guid prjGuid)
        {
            return new PT_PrjInfo_ZTB { PrjCode = prjCode, PrjGuid = prjGuid };
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Area
        {
            get
            {
                return this._Area;
            }
            set
            {
                this.ReportPropertyChanging("Area");
                this._Area = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Area");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string BudgetWay
        {
            get
            {
                return this._BudgetWay;
            }
            set
            {
                this.ReportPropertyChanging("BudgetWay");
                this._BudgetWay = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("BudgetWay");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string BuildingArea
        {
            get
            {
                return this._BuildingArea;
            }
            set
            {
                this.ReportPropertyChanging("BuildingArea");
                this._BuildingArea = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("BuildingArea");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string BuildingType
        {
            get
            {
                return this._BuildingType;
            }
            set
            {
                this.ReportPropertyChanging("BuildingType");
                this._BuildingType = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("BuildingType");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ComputeClass
        {
            get
            {
                return this._ComputeClass;
            }
            set
            {
                this.ReportPropertyChanging("ComputeClass");
                this._ComputeClass = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ComputeClass");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string ContractSum
        {
            get
            {
                return this._ContractSum;
            }
            set
            {
                this.ReportPropertyChanging("ContractSum");
                this._ContractSum = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ContractSum");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string ContractWay
        {
            get
            {
                return this._ContractWay;
            }
            set
            {
                this.ReportPropertyChanging("ContractWay");
                this._ContractWay = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("ContractWay");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Counsellor
        {
            get
            {
                return this._Counsellor;
            }
            set
            {
                this.ReportPropertyChanging("Counsellor");
                this._Counsellor = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Counsellor");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Designer
        {
            get
            {
                return this._Designer;
            }
            set
            {
                this.ReportPropertyChanging("Designer");
                this._Designer = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Designer");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Duration
        {
            get
            {
                return this._Duration;
            }
            set
            {
                this.ReportPropertyChanging("Duration");
                this._Duration = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Duration");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                this.ReportPropertyChanging("EndDate");
                this._EndDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("EndDate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string FileName
        {
            get
            {
                return this._FileName;
            }
            set
            {
                this.ReportPropertyChanging("FileName");
                this._FileName = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("FileName");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string FileURL
        {
            get
            {
                return this._FileURL;
            }
            set
            {
                this.ReportPropertyChanging("FileURL");
                this._FileURL = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("FileURL");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Inspector
        {
            get
            {
                return this._Inspector;
            }
            set
            {
                this.ReportPropertyChanging("Inspector");
                this._Inspector = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Inspector");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string KeyPart
        {
            get
            {
                return this._KeyPart;
            }
            set
            {
                this.ReportPropertyChanging("KeyPart");
                this._KeyPart = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("KeyPart");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string LinkMan
        {
            get
            {
                return this._LinkMan;
            }
            set
            {
                this.ReportPropertyChanging("LinkMan");
                this._LinkMan = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("LinkMan");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public Guid? MarketInfoGuid
        {
            get
            {
                return this._MarketInfoGuid;
            }
            set
            {
                this.ReportPropertyChanging("MarketInfoGuid");
                this._MarketInfoGuid = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("MarketInfoGuid");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string OtherStatement
        {
            get
            {
                return this._OtherStatement;
            }
            set
            {
                this.ReportPropertyChanging("OtherStatement");
                this._OtherStatement = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("OtherStatement");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Owner
        {
            get
            {
                return this._Owner;
            }
            set
            {
                this.ReportPropertyChanging("Owner");
                this._Owner = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Owner");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public int? OwnerCode
        {
            get
            {
                return this._OwnerCode;
            }
            set
            {
                this.ReportPropertyChanging("OwnerCode");
                this._OwnerCode = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("OwnerCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PayCondition
        {
            get
            {
                return this._PayCondition;
            }
            set
            {
                this.ReportPropertyChanging("PayCondition");
                this._PayCondition = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PayCondition");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string PayWay
        {
            get
            {
                return this._PayWay;
            }
            set
            {
                this.ReportPropertyChanging("PayWay");
                this._PayWay = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PayWay");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=false)]
        public string PrjCode
        {
            get
            {
                return this._PrjCode;
            }
            set
            {
                this.ReportPropertyChanging("PrjCode");
                this._PrjCode = StructuralObject.SetValidValue(value, false);
                this.ReportPropertyChanged("PrjCode");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public double? PrjCost
        {
            get
            {
                return this._PrjCost;
            }
            set
            {
                this.ReportPropertyChanging("PrjCost");
                this._PrjCost = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PrjCost");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PrjFundInfo
        {
            get
            {
                return this._PrjFundInfo;
            }
            set
            {
                this.ReportPropertyChanging("PrjFundInfo");
                this._PrjFundInfo = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjFundInfo");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=true, IsNullable=false), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PrjInfo
        {
            get
            {
                return this._PrjInfo;
            }
            set
            {
                this.ReportPropertyChanging("PrjInfo");
                this._PrjInfo = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjInfo");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PrjKindClass
        {
            get
            {
                return this._PrjKindClass;
            }
            set
            {
                this.ReportPropertyChanging("PrjKindClass");
                this._PrjKindClass = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjKindClass");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string PrjManager
        {
            get
            {
                return this._PrjManager;
            }
            set
            {
                this.ReportPropertyChanging("PrjManager");
                this._PrjManager = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjManager");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PrjName
        {
            get
            {
                return this._PrjName;
            }
            set
            {
                this.ReportPropertyChanging("PrjName");
                this._PrjName = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjName");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PrjPlace
        {
            get
            {
                return this._PrjPlace;
            }
            set
            {
                this.ReportPropertyChanging("PrjPlace");
                this._PrjPlace = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjPlace");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public int? PrjState
        {
            get
            {
                return this._PrjState;
            }
            set
            {
                this.ReportPropertyChanging("PrjState");
                this._PrjState = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PrjState");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string QualityClass
        {
            get
            {
                return this._QualityClass;
            }
            set
            {
                this.ReportPropertyChanging("QualityClass");
                this._QualityClass = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("QualityClass");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Rank
        {
            get
            {
                return this._Rank;
            }
            set
            {
                this.ReportPropertyChanging("Rank");
                this._Rank = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Rank");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string RationClass
        {
            get
            {
                return this._RationClass;
            }
            set
            {
                this.ReportPropertyChanging("RationClass");
                this._RationClass = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("RationClass");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this.ReportPropertyChanging("Remark");
                this._Remark = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Remark");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public DateTime? StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                this.ReportPropertyChanging("StartDate");
                this._StartDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("StartDate");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string TenderWay
        {
            get
            {
                return this._TenderWay;
            }
            set
            {
                this.ReportPropertyChanging("TenderWay");
                this._TenderWay = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("TenderWay");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string TotalHouseNum
        {
            get
            {
                return this._TotalHouseNum;
            }
            set
            {
                this.ReportPropertyChanging("TotalHouseNum");
                this._TotalHouseNum = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("TotalHouseNum");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string UndergroundArea
        {
            get
            {
                return this._UndergroundArea;
            }
            set
            {
                this.ReportPropertyChanging("UndergroundArea");
                this._UndergroundArea = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UndergroundArea");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string UsegrounArea
        {
            get
            {
                return this._UsegrounArea;
            }
            set
            {
                this.ReportPropertyChanging("UsegrounArea");
                this._UsegrounArea = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UsegrounArea");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string WorkUnit
        {
            get
            {
                return this._WorkUnit;
            }
            set
            {
                this.ReportPropertyChanging("WorkUnit");
                this._WorkUnit = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("WorkUnit");
            }
        }
    }
}

