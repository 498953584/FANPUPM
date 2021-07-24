namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ContractPayInfo
    {
        private string _accountbank;
        private string _applyuser;
        private int _auditstate;
        private string _bankaccounts;
        private string _contractcode;
        private Guid _guidflow;
        private int _paycompany;
        private DateTime _paydate;
        private string _paymode;
        private decimal _paymoney;
        private decimal _paypercent;
        private string _paytype;
        private Guid _prjcode;
        private decimal _productvalue;
        private string _reason;

        public string AccountBank
        {
            get
            {
                return this._accountbank;
            }
            set
            {
                this._accountbank = value;
            }
        }

        public string ApplyUser
        {
            get
            {
                return this._applyuser;
            }
            set
            {
                this._applyuser = value;
            }
        }

        public int AuditState
        {
            get
            {
                return this._auditstate;
            }
            set
            {
                this._auditstate = value;
            }
        }

        public string BankAccounts
        {
            get
            {
                return this._bankaccounts;
            }
            set
            {
                this._bankaccounts = value;
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

        public Guid GuidFlow
        {
            get
            {
                return this._guidflow;
            }
            set
            {
                this._guidflow = value;
            }
        }

        public int PayCompany
        {
            get
            {
                return this._paycompany;
            }
            set
            {
                this._paycompany = value;
            }
        }

        public DateTime PayDate
        {
            get
            {
                return this._paydate;
            }
            set
            {
                this._paydate = value;
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

        public decimal PayMoney
        {
            get
            {
                return this._paymoney;
            }
            set
            {
                this._paymoney = value;
            }
        }

        public decimal PayPercent
        {
            get
            {
                return this._paypercent;
            }
            set
            {
                this._paypercent = value;
            }
        }

        public string PayType
        {
            get
            {
                return this._paytype;
            }
            set
            {
                this._paytype = value;
            }
        }

        public Guid PrjCode
        {
            get
            {
                return this._prjcode;
            }
            set
            {
                this._prjcode = value;
            }
        }

        public decimal ProductValue
        {
            get
            {
                return this._productvalue;
            }
            set
            {
                this._productvalue = value;
            }
        }

        public string Reason
        {
            get
            {
                return this._reason;
            }
            set
            {
                this._reason = value;
            }
        }
    }
}

