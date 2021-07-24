namespace cn.justwin.Fund.PrjLoad
{
    using System;

    [Serializable]
    public class PrjLoadModel
    {
        private Guid _accountid;
        private string _accountNum;
        private string _acountName;
        private int? _flowstate;
        private string _loancause;
        private string _loanCode;
        private DateTime? _loandate;
        private decimal? _loanfund;
        private Guid _loanid;
        private string _loanman;
        private string _loanmanName;
        private decimal? _loanrate;
        private DateTime? _planreturndate;
        private Guid _prjguid;
        private string _prjName;
        private string _remark;
        private DateTime? _returndate;
        private string _returnman;
        private string _returnmanName;
        private int? _returnState;

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

        public string AccountNum
        {
            get
            {
                return this._accountNum;
            }
            set
            {
                this._accountNum = value;
            }
        }

        public string AcountName
        {
            get
            {
                return this._acountName;
            }
            set
            {
                this._acountName = value;
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

        public string LoanCause
        {
            get
            {
                return this._loancause;
            }
            set
            {
                this._loancause = value;
            }
        }

        public string LoanCode
        {
            get
            {
                return this._loanCode;
            }
            set
            {
                this._loanCode = value;
            }
        }

        public DateTime? LoanDate
        {
            get
            {
                return this._loandate;
            }
            set
            {
                this._loandate = value;
            }
        }

        public decimal? LoanFund
        {
            get
            {
                return this._loanfund;
            }
            set
            {
                this._loanfund = value;
            }
        }

        public Guid LoanID
        {
            get
            {
                return this._loanid;
            }
            set
            {
                this._loanid = value;
            }
        }

        public string LoanMan
        {
            get
            {
                return this._loanman;
            }
            set
            {
                this._loanman = value;
            }
        }

        public string LoanmanName
        {
            get
            {
                return this._loanmanName;
            }
            set
            {
                this._loanmanName = value;
            }
        }

        public decimal? LoanRate
        {
            get
            {
                return this._loanrate;
            }
            set
            {
                this._loanrate = value;
            }
        }

        public DateTime? PlanReturnDate
        {
            get
            {
                return this._planreturndate;
            }
            set
            {
                this._planreturndate = value;
            }
        }

        public Guid PrjGuid
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

        public string PrjName
        {
            get
            {
                return this._prjName;
            }
            set
            {
                this._prjName = value;
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

        public DateTime? ReturnDate
        {
            get
            {
                return this._returndate;
            }
            set
            {
                this._returndate = value;
            }
        }

        public string ReturnMan
        {
            get
            {
                return this._returnman;
            }
            set
            {
                this._returnman = value;
            }
        }

        public string ReturnmanName
        {
            get
            {
                return this._returnmanName;
            }
            set
            {
                this._returnmanName = value;
            }
        }

        public int? ReturnState
        {
            get
            {
                return this._returnState;
            }
            set
            {
                this._returnState = value;
            }
        }
    }
}

