namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class ScheduleInfo : BaseInfo
    {
        private int _ChildNum;
        private int _ConfineClass;
        private DateTime _FactBeginDate;
        private DateTime _FactEndDate;
        private int _IsMileStone;
        private int _IsPivotal;
        private int _IsServer;
        private int _IsValid;
        private int _OverPercent;
        private string _ParentScheduleCode = "";
        private DateTime _PlanBeginDate;
        private DateTime _PlanEnddate;
        private decimal _Quantity;
        private string _QuantityUnit;
        private string _RationItemNo = "";
        private string _RationVersion = "";
        private string _Remark;
        private string _ScheduleCode;
        private int _ScheduleLevel = 1;
        private string _ScheduleName;
        private string _ScheduleRelation = "";
        private decimal _SynthesizeSumPrice;
        private decimal _SynthesizeUnitPrice;
        private int _TheOrder;
        private Guid _VersionCode = Guid.Empty;
        private int _WorkDay;
        private int _WorkLayer;

        public int ChildNum
        {
            get
            {
                return this._ChildNum;
            }
            set
            {
                this._ChildNum = value;
            }
        }

        public int ConfineClass
        {
            get
            {
                return this._ConfineClass;
            }
            set
            {
                this._ConfineClass = value;
            }
        }

        public DateTime FactBeginDate
        {
            get
            {
                return this._FactBeginDate;
            }
            set
            {
                this._FactBeginDate = value;
            }
        }

        public DateTime FactEndDate
        {
            get
            {
                return this._FactEndDate;
            }
            set
            {
                this._FactEndDate = value;
            }
        }

        public int IsMileStone
        {
            get
            {
                return this._IsMileStone;
            }
            set
            {
                this._IsMileStone = value;
            }
        }

        public int IsPivotal
        {
            get
            {
                return this._IsPivotal;
            }
            set
            {
                this._IsPivotal = value;
            }
        }

        public int IsServer
        {
            get
            {
                return this._IsServer;
            }
            set
            {
                this._IsServer = value;
            }
        }

        public int IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                this._IsValid = value;
            }
        }

        public int OverPercent
        {
            get
            {
                return this._OverPercent;
            }
            set
            {
                this._OverPercent = value;
            }
        }

        public string ParentScheduleCode
        {
            get
            {
                return this._ParentScheduleCode;
            }
            set
            {
                this._ParentScheduleCode = value;
            }
        }

        public DateTime PlanBeginDate
        {
            get
            {
                return this._PlanBeginDate;
            }
            set
            {
                this._PlanBeginDate = value;
            }
        }

        public DateTime PlanEndDate
        {
            get
            {
                return this._PlanEnddate;
            }
            set
            {
                this._PlanEnddate = value;
            }
        }

        public decimal Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                this._Quantity = value;
            }
        }

        public string QuantityUnit
        {
            get
            {
                return this._QuantityUnit;
            }
            set
            {
                this._QuantityUnit = value;
            }
        }

        public string RationItemNo
        {
            get
            {
                return this._RationItemNo;
            }
            set
            {
                this._RationItemNo = value;
            }
        }

        public string RationVersion
        {
            get
            {
                return this._RationVersion;
            }
            set
            {
                this._RationVersion = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        public string ScheduleCode
        {
            get
            {
                return this._ScheduleCode;
            }
            set
            {
                this._ScheduleCode = value;
            }
        }

        public int ScheduleLevel
        {
            get
            {
                return this._ScheduleLevel;
            }
            set
            {
                this._ScheduleLevel = value;
            }
        }

        public string ScheduleName
        {
            get
            {
                return this._ScheduleName;
            }
            set
            {
                this._ScheduleName = value;
            }
        }

        public string ScheduleRelation
        {
            get
            {
                return this._ScheduleRelation;
            }
            set
            {
                this._ScheduleRelation = value;
            }
        }

        public decimal SynthesizeSumPrice
        {
            get
            {
                return this._SynthesizeSumPrice;
            }
            set
            {
                this._SynthesizeSumPrice = value;
            }
        }

        public decimal SynthesizeUnitPrice
        {
            get
            {
                return this._SynthesizeUnitPrice;
            }
            set
            {
                this._SynthesizeUnitPrice = value;
            }
        }

        public int TheOrder
        {
            get
            {
                return this._TheOrder;
            }
            set
            {
                this._TheOrder = value;
            }
        }

        public Guid VersionCode
        {
            get
            {
                return this._VersionCode;
            }
            set
            {
                this._VersionCode = value;
            }
        }

        public int WorkDay
        {
            get
            {
                return this._WorkDay;
            }
            set
            {
                this._WorkDay = value;
            }
        }

        public int WorkLayer
        {
            get
            {
                return this._WorkLayer;
            }
            set
            {
                this._WorkLayer = value;
            }
        }
    }
}

