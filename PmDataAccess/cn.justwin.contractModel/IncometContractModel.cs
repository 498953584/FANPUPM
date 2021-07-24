namespace cn.justwin.contractModel
{
    using cn.justwin.stockModel;
    using cn.justwin.XPMBasicContactCorp;
    using System;

    [Serializable]
    public class IncometContractModel
    {
        private string _annex;
        private string _balancemode;
        private string _cllectioncondition;
        private int _conState;
        private string _contractcode;
        private string _contractid;
        private string _contractname;
        private decimal? _contractprice;
        private string _CParty;
        private DateTime? _enddate;
        private string _fcode;
        private DateTime? _filetime;
        private int _flowState;
        private bool _isarchived;
        private bool _isfcontract;
        private string _mainprovision;
        private BasicContactCorpModel _party = new BasicContactCorpModel();
        private string _paymode;
        private PrjInfoModel _project = new PrjInfoModel();
        private string _qualityPeriod;
        private DateTime? _ReFundDate;
        private DateTime? _registertime;
        private string _remark;
        private string _second;
        private int _sign;
        private string _signedaddress;
        private DateTime? _signedtime;
        private string _signPepole;
        private DateTime? _startdate;
        private string _subscriber;
        private ContractTypeModel _type = new ContractTypeModel();
        private string _usercodes;

        public string Annex
        {
            get
            {
                return this._annex;
            }
            set
            {
                this._annex = value;
            }
        }

        public string BalanceMode
        {
            get
            {
                return this._balancemode;
            }
            set
            {
                this._balancemode = value;
            }
        }

        public string CllectionCondition
        {
            get
            {
                return this._cllectioncondition;
            }
            set
            {
                this._cllectioncondition = value;
            }
        }

        public int ConState
        {
            get
            {
                return this._conState;
            }
            set
            {
                this._conState = value;
            }
        }

        public string ContractCode
        {
            get
            {
                return this._contractcode;
            }
            set
            {
                this._contractcode = value;
            }
        }

        public string ContractID
        {
            get
            {
                return this._contractid;
            }
            set
            {
                this._contractid = value;
            }
        }

        public string ContractName
        {
            get
            {
                return this._contractname;
            }
            set
            {
                this._contractname = value;
            }
        }

        public decimal? ContractPrice
        {
            get
            {
                return this._contractprice;
            }
            set
            {
                this._contractprice = value;
            }
        }

        public string CParty
        {
            get
            {
                return this._CParty;
            }
            set
            {
                this._CParty = value;
            }
        }

        public DateTime? EndDate
        {
            get
            {
                return this._enddate;
            }
            set
            {
                this._enddate = value;
            }
        }

        public string FCode
        {
            get
            {
                return this._fcode;
            }
            set
            {
                this._fcode = value;
            }
        }

        public DateTime? FileTime
        {
            get
            {
                return this._filetime;
            }
            set
            {
                this._filetime = value;
            }
        }

        public int FlowState
        {
            get
            {
                return this._flowState;
            }
            set
            {
                this._flowState = value;
            }
        }

        public bool IsArchived
        {
            get
            {
                return this._isarchived;
            }
            set
            {
                this._isarchived = value;
            }
        }

        public bool isFContract
        {
            get
            {
                return this._isfcontract;
            }
            set
            {
                this._isfcontract = value;
            }
        }

        public string MainProvision
        {
            get
            {
                return this._mainprovision;
            }
            set
            {
                this._mainprovision = value;
            }
        }

        public BasicContactCorpModel Party
        {
            get
            {
                return this._party;
            }
            set
            {
                this._party = value;
            }
        }

        public string PayMode
        {
            get
            {
                return this._paymode;
            }
            set
            {
                this._paymode = value;
            }
        }

        public PrjInfoModel Project
        {
            get
            {
                return this._project;
            }
            set
            {
                this._project = value;
            }
        }

        public string QualityPeriod
        {
            get
            {
                return this._qualityPeriod;
            }
            set
            {
                this._qualityPeriod = value;
            }
        }

        public DateTime? ReFundDate
        {
            get
            {
                return this._ReFundDate;
            }
            set
            {
                this._ReFundDate = value;
            }
        }

        public DateTime? RegisterTime
        {
            get
            {
                return this._registertime;
            }
            set
            {
                this._registertime = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }

        public string Second
        {
            get
            {
                return this._second;
            }
            set
            {
                this._second = value;
            }
        }

        public int Sign
        {
            get
            {
                return this._sign;
            }
            set
            {
                this._sign = value;
            }
        }

        public string SignedAddress
        {
            get
            {
                return this._signedaddress;
            }
            set
            {
                this._signedaddress = value;
            }
        }

        public DateTime? SignedTime
        {
            get
            {
                return this._signedtime;
            }
            set
            {
                this._signedtime = value;
            }
        }

        public string SignPepole
        {
            get
            {
                return this._signPepole;
            }
            set
            {
                this._signPepole = value;
            }
        }

        public DateTime? StartDate
        {
            get
            {
                return this._startdate;
            }
            set
            {
                this._startdate = value;
            }
        }

        public string Subscriber
        {
            get
            {
                return this._subscriber;
            }
            set
            {
                this._subscriber = value;
            }
        }

        public ContractTypeModel TypeID
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        public string UserCodes
        {
            get
            {
                return this._usercodes;
            }
            set
            {
                this._usercodes = value;
            }
        }
    }
}

