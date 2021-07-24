namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class PrjDisModel
    {
        private string _Address = "";
        private string _Area = "";
        private string _BalanceRemark = "";
        private string _ComputeClass = "";
        private string _Counsellor = "";
        private string _Department = "";
        private string _Designer = "";
        private int _Duration;
        private DateTime _EndDate = DateTime.Now;
        private string _EntRationClass = "";
        private DateTime _FinishDate = DateTime.MinValue;
        private string _GoalCost = "";
        private string _Inspector = "";
        private string _OwnerCode = "";
        private Guid _PrincipalReg = Guid.NewGuid();
        private string _PrjCode = "";
        private string _PrjCost = "";
        private string _PrjKindClass = "";
        private string _PrjPlace = "";
        private int _PrjState = 1;
        private string _QualityClass = "";
        private string _QualityGoalRemark = "";
        private string _QualityRemark = "";
        private string _RangeRemark = "";
        private string _Remark = "";
        private string _SafetyRemark = "";
        private string _SocietyRationClass = "";
        private DateTime _StartDate = DateTime.Now;
        private string _SubPrjCode = "";
        private string _SubPrjName = "";
        private string _SubPrjPrincipal = "";
        private Guid _SubPrjReg = Guid.NewGuid();
        private DateTime _VersionTime = DateTime.Now;

        public string Address
        {
            get
            {
                return this._Address;
            }
            set
            {
                this._Address = value;
            }
        }

        public string Area
        {
            get
            {
                return this._Area;
            }
            set
            {
                this._Area = value;
            }
        }

        public string BalanceRemark
        {
            get
            {
                return this._BalanceRemark;
            }
            set
            {
                this._BalanceRemark = value;
            }
        }

        public string ComputeClass
        {
            get
            {
                return this._ComputeClass;
            }
            set
            {
                this._ComputeClass = value;
            }
        }

        public string Counsellor
        {
            get
            {
                return this._Counsellor;
            }
            set
            {
                this._Counsellor = value;
            }
        }

        public string Department
        {
            get
            {
                return this._Department;
            }
            set
            {
                this._Department = value;
            }
        }

        public string Designer
        {
            get
            {
                return this._Designer;
            }
            set
            {
                this._Designer = value;
            }
        }

        public int Duration
        {
            get
            {
                return this._Duration;
            }
            set
            {
                this._Duration = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                this._EndDate = value;
            }
        }

        public string EntRationClass
        {
            get
            {
                return this._EntRationClass;
            }
            set
            {
                this._EntRationClass = value;
            }
        }

        public string GoalCost
        {
            get
            {
                return this._GoalCost;
            }
            set
            {
                this._GoalCost = value;
            }
        }

        public string Inspector
        {
            get
            {
                return this._Inspector;
            }
            set
            {
                this._Inspector = value;
            }
        }

        public string OwnerCode
        {
            get
            {
                return this._OwnerCode;
            }
            set
            {
                this._OwnerCode = value;
            }
        }

        public Guid PrincipalReg
        {
            get
            {
                return this._PrincipalReg;
            }
            set
            {
                this._PrincipalReg = value;
            }
        }

        public string PrjCode
        {
            get
            {
                return this._PrjCode;
            }
            set
            {
                this._PrjCode = value;
            }
        }

        public string PrjCost
        {
            get
            {
                return this._PrjCost;
            }
            set
            {
                this._PrjCost = value;
            }
        }

        public string PrjKindClass
        {
            get
            {
                return this._PrjKindClass;
            }
            set
            {
                this._PrjKindClass = value;
            }
        }

        public string PrjPlace
        {
            get
            {
                return this._PrjPlace;
            }
            set
            {
                this._PrjPlace = value;
            }
        }

        public int PrjState
        {
            get
            {
                return this._PrjState;
            }
            set
            {
                this._PrjState = value;
            }
        }

        public string QualityClass
        {
            get
            {
                return this._QualityClass;
            }
            set
            {
                this._QualityClass = value;
            }
        }

        public string QualityGoalRemark
        {
            get
            {
                return this._QualityGoalRemark;
            }
            set
            {
                this._QualityGoalRemark = value;
            }
        }

        public string QualityRemark
        {
            get
            {
                return this._QualityRemark;
            }
            set
            {
                this._QualityRemark = value;
            }
        }

        public string RangeRemark
        {
            get
            {
                return this._RangeRemark;
            }
            set
            {
                this._RangeRemark = value;
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

        public string SafetyRemark
        {
            get
            {
                return this._SafetyRemark;
            }
            set
            {
                this._SafetyRemark = value;
            }
        }

        public string SocietyRationClass
        {
            get
            {
                return this._SocietyRationClass;
            }
            set
            {
                this._SocietyRationClass = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                this._StartDate = value;
            }
        }

        public string SubPrjCode
        {
            get
            {
                return this._SubPrjCode;
            }
            set
            {
                this._SubPrjCode = value;
            }
        }

        public string SubPrjName
        {
            get
            {
                return this._SubPrjName;
            }
            set
            {
                this._SubPrjName = value;
            }
        }

        public string SubPrjPrincipal
        {
            get
            {
                return this._SubPrjPrincipal;
            }
            set
            {
                this._SubPrjPrincipal = value;
            }
        }

        public Guid SubPrjReg
        {
            get
            {
                return this._SubPrjReg;
            }
            set
            {
                this._SubPrjReg = value;
            }
        }
    }
}

