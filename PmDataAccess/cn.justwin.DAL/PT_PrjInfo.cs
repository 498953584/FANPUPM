namespace cn.justwin.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Runtime.Serialization;

    [Serializable, EdmEntityType(NamespaceName="Pm2Model", Name="PT_PrjInfo"), DataContract(IsReference=true)]
    public class PT_PrjInfo : EntityObject
    {
        private string _Area;
        private string _BudgetWay;
        private string _BuildingArea;
        private string _BuildingType;
        private string _businessman;
        private string _ComputeClass;
        private string _ContractSum;
        private string _ContractWay;
        private string _Counsellor;
        private string _Designer;
        private string _Duration;
        private DateTime? _EndDate;
        private string _FileName;
        private string _FileURL;
        private string _grade;
        private int? _i_ChildNum;
        private int? _i_xh;
        private string _Inspector;
        private bool? _IsConfirm;
        private string _IsValid;
        private string _KeyPart;
        private string _LinkMan;
        private Guid? _MarketInfoGuid;
        private string _OtherStatement;
        private string _Owner;
        private int? _OwnerCode;
        private string _PayCondition;
        private string _PayWay;
        private string _Podepom;
        private string _PrjCode;
        private double? _PrjCost;
        private string _PrjFundInfo;
        private Guid? _PrjGuid;
        private string _PrjInfo;
        private string _PrjKindClass;
        private string _PrjManager;
        private string _PrjName;
        private string _PrjPlace;
        private int? _PrjState;
        private string _PrjStateRemark;
        private string _QualityClass;
        private string _Rank;
        private string _RationClass;
        private DateTime? _RecordDate;
        private string _Remark1;
        private DateTime? _StartDate;
        private string _telephone;
        private string _TenderWay;
        private string _TotalHouseNum;
        private string _TypeCode;
        private string _UndergroundArea;
        private string _UsegrounArea;
        private string _UserCode;
        private string _WorkUnit;
        private string _xmgroup;

        public static PT_PrjInfo CreatePT_PrjInfo(string typeCode)
        {
            return new PT_PrjInfo { TypeCode = typeCode };
        }

        private int GetEndYear(string userId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from p in entities.PT_PrjInfo
                    where ((p.IsValid == "1") && (p.Podepom.Contains(userId) || p.PrjManager.Contains(userId))) && p.EndDate.HasValue
                    orderby p.EndDate.Value descending
                    select p.EndDate.Value.Year).FirstOrDefault<int>();
            }
        }

        public List<PT_PrjInfo> GetProject(string userId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from p in entities.PT_PrjInfo
                    where ((p.IsValid == "1") && (p.PrjManager.Contains(userId) || p.Podepom.Contains(userId))) && ((((p.PrjState == -1) || (p.PrjState == 6)) || (p.PrjState == 4)) || (p.PrjState == 8))
                    orderby p.RecordDate
                    select p).ToList<PT_PrjInfo>();
            }
        }

        public List<PT_PrjInfo> GetProject(int year, string userId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from p in entities.PT_PrjInfo
                    where (((((p.IsValid == "1") && (p.PrjManager.Contains(userId) || p.Podepom.Contains(userId))) && p.StartDate.HasValue) && (p.StartDate.Value.Year <= year)) && p.EndDate.HasValue) && (p.EndDate.Value.Year >= year)
                    orderby p.RecordDate
                    select p).ToList<PT_PrjInfo>();
            }
        }

        public List<PT_PrjInfo> GetProject2(string userId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from p in entities.PT_PrjInfo
                    where ((p.IsValid == "1") && (p.i_ChildNum == 0)) && (p.PrjManager.Contains(userId) || p.Podepom.Contains(userId))
                    orderby p.RecordDate
                    select p).ToList<PT_PrjInfo>();
            }
        }

        public List<PT_PrjInfo> GetReportProject(int year, string userId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from p in entities.PT_PrjInfo
                    where ((((((p.IsValid == "1") && (p.PrjManager.Contains(userId) || p.Podepom.Contains(userId))) && p.StartDate.HasValue) && (p.StartDate.Value.Year <= year)) && p.EndDate.HasValue) && (p.EndDate.Value.Year >= year)) && ((((p.PrjState == -1) || (p.PrjState == 6)) || (p.PrjState == 4)) || (p.PrjState == 8))
                    orderby p.RecordDate
                    select p).ToList<PT_PrjInfo>();
            }
        }

        private int GetStartYear(string userId)
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return (from p in entities.PT_PrjInfo
                    where ((p.IsValid == "1") && (p.Podepom.Contains(userId) || p.PrjManager.Contains(userId))) && p.StartDate.HasValue
                    orderby p.StartDate.Value.Year
                    select p.StartDate.Value.Year).FirstOrDefault<int>();
            }
        }

        public List<object> GetYears(string userId)
        {
            List<object> list = new List<object>();
            new List<int>();
            int startYear = this.GetStartYear(userId);
            int endYear = this.GetEndYear(userId);
            while (startYear <= endYear)
            {
                list.Add(new { Value = startYear, Text = startYear + "年度" });
                startYear++;
            }
            return list;
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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
        public string businessman
        {
            get
            {
                return this._businessman;
            }
            set
            {
                this.ReportPropertyChanging("businessman");
                this._businessman = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("businessman");
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string grade
        {
            get
            {
                return this._grade;
            }
            set
            {
                this.ReportPropertyChanging("grade");
                this._grade = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("grade");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public int? i_ChildNum
        {
            get
            {
                return this._i_ChildNum;
            }
            set
            {
                this.ReportPropertyChanging("i_ChildNum");
                this._i_ChildNum = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("i_ChildNum");
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public bool? IsConfirm
        {
            get
            {
                return this._IsConfirm;
            }
            set
            {
                this.ReportPropertyChanging("IsConfirm");
                this._IsConfirm = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("IsConfirm");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                this.ReportPropertyChanging("IsValid");
                this._IsValid = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("IsValid");
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Podepom
        {
            get
            {
                return this._Podepom;
            }
            set
            {
                this.ReportPropertyChanging("Podepom");
                this._Podepom = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Podepom");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string PrjCode
        {
            get
            {
                return this._PrjCode;
            }
            set
            {
                this.ReportPropertyChanging("PrjCode");
                this._PrjCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjCode");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public Guid? PrjGuid
        {
            get
            {
                return this._PrjGuid;
            }
            set
            {
                this.ReportPropertyChanging("PrjGuid");
                this._PrjGuid = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("PrjGuid");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string PrjStateRemark
        {
            get
            {
                return this._PrjStateRemark;
            }
            set
            {
                this.ReportPropertyChanging("PrjStateRemark");
                this._PrjStateRemark = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("PrjStateRemark");
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public DateTime? RecordDate
        {
            get
            {
                return this._RecordDate;
            }
            set
            {
                this.ReportPropertyChanging("RecordDate");
                this._RecordDate = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("RecordDate");
            }
        }

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
        public string Remark1
        {
            get
            {
                return this._Remark1;
            }
            set
            {
                this.ReportPropertyChanging("Remark1");
                this._Remark1 = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Remark1");
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
        public string telephone
        {
            get
            {
                return this._telephone;
            }
            set
            {
                this.ReportPropertyChanging("telephone");
                this._telephone = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("telephone");
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=true, IsNullable=false)]
        public string TypeCode
        {
            get
            {
                return this._TypeCode;
            }
            set
            {
                if (this._TypeCode != value)
                {
                    this.ReportPropertyChanging("TypeCode");
                    this._TypeCode = StructuralObject.SetValidValue(value, false);
                    this.ReportPropertyChanged("TypeCode");
                }
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [DataMember, EdmScalarProperty(EntityKeyProperty=false, IsNullable=true)]
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
        public string UserCode
        {
            get
            {
                return this._UserCode;
            }
            set
            {
                this.ReportPropertyChanging("UserCode");
                this._UserCode = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("UserCode");
            }
        }

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
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

        [EdmScalarProperty(EntityKeyProperty=false, IsNullable=true), DataMember]
        public string xmgroup
        {
            get
            {
                return this._xmgroup;
            }
            set
            {
                this.ReportPropertyChanging("xmgroup");
                this._xmgroup = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("xmgroup");
            }
        }
    }
}

