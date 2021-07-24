namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ProgressProceedInfo
    {
        private string _Account;
        private string _AdministerFruitUnit;
        private string _AppPrejectName;
        private decimal _AuditValue;
        private string _DealinMinister;
        private DateTime _EndDate = DateTime.Today;
        private string _Engineer;
        private DateTime _EntAuditDate = DateTime.Today;
        private string _EntAuditIdea;
        private string _EntAuditPeople;
        private bool _EntAuditResult = true;
        private int _EtcaeterasPeopleAmount;
        private string _FruitContent;
        private string _FruitName;
        private int _MainID;
        private decimal _PPMAdvancementIncomeCount;
        private int _PPMAuditResult = -1;
        private string _PPMCommitteeIdea;
        private string _PPMDeclareUnitIdea;
        private string _PPMGroupIdea;
        private string _PPMRemark;
        private string _PPMSerialNumber;
        private string _PrejectMinister;
        private string _PrjCode;
        private DateTime _StartDate = DateTime.Today;

        public string Account
        {
            get
            {
                return this._Account;
            }
            set
            {
                this._Account = value;
            }
        }

        public string AdministerFruitUnit
        {
            get
            {
                return this._AdministerFruitUnit;
            }
            set
            {
                this._AdministerFruitUnit = value;
            }
        }

        public string AppPrejectName
        {
            get
            {
                return this._AppPrejectName;
            }
            set
            {
                this._AppPrejectName = value;
            }
        }

        public string AuditResult
        {
            get
            {
                if (this._EntAuditResult)
                {
                    return "审核通过";
                }
                return "审核未通过";
            }
        }

        public decimal AuditValue
        {
            get
            {
                return this._AuditValue;
            }
            set
            {
                this._AuditValue = value;
            }
        }

        public string DealinMinister
        {
            get
            {
                return this._DealinMinister;
            }
            set
            {
                this._DealinMinister = value;
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

        public string Engineer
        {
            get
            {
                return this._Engineer;
            }
            set
            {
                this._Engineer = value;
            }
        }

        public DateTime EntAuditDate
        {
            get
            {
                return this._EntAuditDate;
            }
            set
            {
                this._EntAuditDate = value;
            }
        }

        public string EntAuditIdea
        {
            get
            {
                return this._EntAuditIdea;
            }
            set
            {
                this._EntAuditIdea = value;
            }
        }

        public string EntAuditPeople
        {
            get
            {
                return this._EntAuditPeople;
            }
            set
            {
                this._EntAuditPeople = value;
            }
        }

        public bool EntAuditResult
        {
            get
            {
                return this._EntAuditResult;
            }
            set
            {
                this._EntAuditResult = value;
            }
        }

        public int EtcaeterasPeopleAmount
        {
            get
            {
                return this._EtcaeterasPeopleAmount;
            }
            set
            {
                this._EtcaeterasPeopleAmount = value;
            }
        }

        public string FruitContent
        {
            get
            {
                return this._FruitContent;
            }
            set
            {
                this._FruitContent = value;
            }
        }

        public string FruitName
        {
            get
            {
                return this._FruitName;
            }
            set
            {
                this._FruitName = value;
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

        public decimal PPMAdvancementIncomeCount
        {
            get
            {
                return this._PPMAdvancementIncomeCount;
            }
            set
            {
                this._PPMAdvancementIncomeCount = value;
            }
        }

        public int PPMAuditResult
        {
            get
            {
                return this._PPMAuditResult;
            }
            set
            {
                this._PPMAuditResult = value;
            }
        }

        public string PPMCommitteeIdea
        {
            get
            {
                return this._PPMCommitteeIdea;
            }
            set
            {
                this._PPMCommitteeIdea = value;
            }
        }

        public string PPMDeclareUnitIdea
        {
            get
            {
                return this._PPMDeclareUnitIdea;
            }
            set
            {
                this._PPMDeclareUnitIdea = value;
            }
        }

        public string PPMGroupIdea
        {
            get
            {
                return this._PPMGroupIdea;
            }
            set
            {
                this._PPMGroupIdea = value;
            }
        }

        public string PPMRemark
        {
            get
            {
                return this._PPMRemark;
            }
            set
            {
                this._PPMRemark = value;
            }
        }

        public string PPMSerialNumber
        {
            get
            {
                return this._PPMSerialNumber;
            }
            set
            {
                this._PPMSerialNumber = value;
            }
        }

        public string PrejectMinister
        {
            get
            {
                return this._PrejectMinister;
            }
            set
            {
                this._PrejectMinister = value;
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
    }
}

