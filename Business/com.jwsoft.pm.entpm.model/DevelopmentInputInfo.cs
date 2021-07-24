namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class DevelopmentInputInfo
    {
        private string _AuditIdea;
        private string _AuditPeople;
        private bool _AuditResult;
        private DateTime _AuditTime;
        private DateTime _FillTime;
        private int _MainID;
        private string _PrjCode;
        private string _PrjName;
        private string _Remark;
        private decimal _ScienceEmpolderMoney;
        private string _UnitCode;
        private string _UnitName;
        private string _WeavePeople = "";

        public string AuditIdea
        {
            get
            {
                return this._AuditIdea;
            }
            set
            {
                this._AuditIdea = value;
            }
        }

        public string AuditPeople
        {
            get
            {
                return this._AuditPeople;
            }
            set
            {
                this._AuditPeople = value;
            }
        }

        public bool AuditResult
        {
            get
            {
                return this._AuditResult;
            }
            set
            {
                this._AuditResult = value;
            }
        }

        public DateTime AuditTime
        {
            get
            {
                return this._AuditTime;
            }
            set
            {
                this._AuditTime = value;
            }
        }

        public DateTime FillTime
        {
            get
            {
                return this._FillTime;
            }
            set
            {
                this._FillTime = value;
            }
        }

        public int MainID
        {
            get
            {
                return this._MainID;
            }
            set
            {
                this._MainID = value;
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

        public string PrjName
        {
            get
            {
                return this._PrjName;
            }
            set
            {
                this._PrjName = value;
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

        public decimal ScienceEmpolderMoney
        {
            get
            {
                return this._ScienceEmpolderMoney;
            }
            set
            {
                this._ScienceEmpolderMoney = value;
            }
        }

        public int Season
        {
            get
            {
                if (this._FillTime.Month <= 3)
                {
                    return 1;
                }
                if (this._FillTime.Month <= 6)
                {
                    return 2;
                }
                if (this._FillTime.Month <= 9)
                {
                    return 2;
                }
                return 4;
            }
        }

        public string UnitCode
        {
            get
            {
                return this._UnitCode;
            }
            set
            {
                this._UnitCode = value;
            }
        }

        public string UnitName
        {
            get
            {
                return this._UnitName;
            }
            set
            {
                this._UnitName = value;
            }
        }

        public string WeavePeople
        {
            get
            {
                return this._WeavePeople;
            }
            set
            {
                this._WeavePeople = value;
            }
        }

        public int Year
        {
            get
            {
                return this._FillTime.Year;
            }
        }
    }
}

