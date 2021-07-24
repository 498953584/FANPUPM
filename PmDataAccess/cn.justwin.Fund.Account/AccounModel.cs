namespace cn.justwin.Fund.Account
{
    using System;

    [Serializable]
    public class AccounModel
    {
        private Guid _accountid;
        private string _accountnum;
        private int? _accountstate;
        private string _acountname;
        private DateTime? _activeDate;
        private string _activeman;
        private string _authorizer;
        private DateTime? _closeDate;
        private string _closeman;
        private DateTime? _creatDate;
        private string _createman;
        private decimal? _currentfund;
        private int? _flowstate;
        private decimal? _incomefund;
        private decimal? _initialfund;
        private decimal? _moneyrate;
        private decimal? _payoutfund;
        private string _prjguid;
        private string _remark;

        public Guid AccountID
        {
            get
            {
                return this._accountid;
            }
            set
            {
                this._accountid = value;
            }
        }

        public string accountNum
        {
            get
            {
                return this._accountnum;
            }
            set
            {
                this._accountnum = value;
            }
        }

        public int? AccountState
        {
            get
            {
                return this._accountstate;
            }
            set
            {
                this._accountstate = value;
            }
        }

        public string acountName
        {
            get
            {
                return this._acountname;
            }
            set
            {
                this._acountname = value;
            }
        }

        public DateTime? activeDate
        {
            get
            {
                return this._activeDate;
            }
            set
            {
                this._activeDate = value;
            }
        }

        public string activeMan
        {
            get
            {
                return this._activeman;
            }
            set
            {
                this._activeman = value;
            }
        }

        public string authorizer
        {
            get
            {
                return this._authorizer;
            }
            set
            {
                this._authorizer = value;
            }
        }

        public DateTime? closeDate
        {
            get
            {
                return this._closeDate;
            }
            set
            {
                this._closeDate = value;
            }
        }

        public string closeMan
        {
            get
            {
                return this._closeman;
            }
            set
            {
                this._closeman = value;
            }
        }

        public DateTime? creatDate
        {
            get
            {
                return this._creatDate;
            }
            set
            {
                this._creatDate = value;
            }
        }

        public string createMan
        {
            get
            {
                return this._createman;
            }
            set
            {
                this._createman = value;
            }
        }

        public decimal? CurrentFund
        {
            get
            {
                return this._currentfund;
            }
            set
            {
                this._currentfund = value;
            }
        }

        public int? FlowState
        {
            get
            {
                return this._flowstate;
            }
            set
            {
                this._flowstate = value;
            }
        }

        public decimal? IncomeFund
        {
            get
            {
                return this._incomefund;
            }
            set
            {
                this._incomefund = value;
            }
        }

        public decimal? initialFund
        {
            get
            {
                return this._initialfund;
            }
            set
            {
                this._initialfund = value;
            }
        }

        public decimal? moneyRate
        {
            get
            {
                return this._moneyrate;
            }
            set
            {
                this._moneyrate = value;
            }
        }

        public decimal? PayoutFund
        {
            get
            {
                return this._payoutfund;
            }
            set
            {
                this._payoutfund = value;
            }
        }

        public string PrjGuid
        {
            get
            {
                return this._prjguid;
            }
            set
            {
                this._prjguid = value;
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
    }
}

