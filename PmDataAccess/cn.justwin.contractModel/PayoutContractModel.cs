namespace cn.justwin.contractModel
{
    using System;

    [Serializable]
    public class PayoutContractModel
    {
        private string _address;
        private string _aname;
        private string _annex;
        private DateTime? _archivedate;
        private string _balancemode;
        private string _balancemodeName;
        private string _bname;
        private string _CapitalNumber;
        private int _conState;
        private string _contractcode;
        private string _contractid;
        private decimal? _contractmoney;
        private string _contractname;
        private DateTime? _enddate;
        private int _fictitious;
        private int? _flowstate;
        private string _incontractid;
        private DateTime? _inputdate;
        private string _inputperson;
        private bool _isarchived;
        private bool _ismaincontract;
        private string _maincontractid;
        private string _mainitem;
        private decimal? _modifiedmoney;
        private string _notes;
        private string _paymentcondition;
        private string _paymode;
        private string _paymodeName;
        private decimal? _prepaymoney;
        private string _prjguid;
        private string _prjName;
        private DateTime? _signdate;
        private DateTime? _startdate;
        private string _termsPayment;
        private string _typeid;
        private string _typeName;
        private string _usercodes;
        private string corpName;
        private string financeNumber;
        private string financeProject;
        private string signPerson;

        public string Address
        {
            get
            {
                return this._address;
            }
            set
            {
                this._address = value;
            }
        }

        public string AName
        {
            get
            {
                return this._aname;
            }
            set
            {
                this._aname = value;
            }
        }

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

        public DateTime? ArchiveDate
        {
            get
            {
                return this._archivedate;
            }
            set
            {
                this._archivedate = value;
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

        public string BalanceModeName
        {
            get
            {
                return this._balancemodeName;
            }
            set
            {
                this._balancemodeName = value;
            }
        }

        public string BName
        {
            get
            {
                return this._bname;
            }
            set
            {
                this._bname = value;
            }
        }

        public string CapitalNumber
        {
            get
            {
                return this._CapitalNumber;
            }
            set
            {
                this._CapitalNumber = value;
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

        public decimal? ContractMoney
        {
            get
            {
                return this._contractmoney;
            }
            set
            {
                this._contractmoney = value;
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

        public string CorpName
        {
            get
            {
                return this.corpName;
            }
            set
            {
                this.corpName = value;
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

        public int Fictitious
        {
            get
            {
                return this._fictitious;
            }
            set
            {
                this._fictitious = value;
            }
        }

        public string FinanceNumber
        {
            get
            {
                return this.financeNumber;
            }
            set
            {
                this.financeNumber = value;
            }
        }

        public string FinanceProject
        {
            get
            {
                return this.financeProject;
            }
            set
            {
                this.financeProject = value;
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

        public string InContractID
        {
            get
            {
                return this._incontractid;
            }
            set
            {
                this._incontractid = value;
            }
        }

        public DateTime? InputDate
        {
            get
            {
                return this._inputdate;
            }
            set
            {
                this._inputdate = value;
            }
        }

        public string InputPerson
        {
            get
            {
                return this._inputperson;
            }
            set
            {
                this._inputperson = value;
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

        public bool IsMainContract
        {
            get
            {
                return this._ismaincontract;
            }
            set
            {
                this._ismaincontract = value;
            }
        }

        public string MainContractID
        {
            get
            {
                return this._maincontractid;
            }
            set
            {
                this._maincontractid = value;
            }
        }

        public string MainItem
        {
            get
            {
                return this._mainitem;
            }
            set
            {
                this._mainitem = value;
            }
        }

        public decimal? ModifiedMoney
        {
            get
            {
                return this._modifiedmoney;
            }
            set
            {
                this._modifiedmoney = value;
            }
        }

        public string Notes
        {
            get
            {
                return this._notes;
            }
            set
            {
                this._notes = value;
            }
        }

        public string PaymentCondition
        {
            get
            {
                return this._paymentcondition;
            }
            set
            {
                this._paymentcondition = value;
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

        public string PayModeName
        {
            get
            {
                return this._paymodeName;
            }
            set
            {
                this._paymodeName = value;
            }
        }

        public decimal? PrepayMoney
        {
            get
            {
                return this._prepaymoney;
            }
            set
            {
                this._prepaymoney = value;
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

        public DateTime? SignDate
        {
            get
            {
                return this._signdate;
            }
            set
            {
                this._signdate = value;
            }
        }

        public string SignPerson
        {
            get
            {
                return this.signPerson;
            }
            set
            {
                this.signPerson = value;
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

        public string TermsPayment
        {
            get
            {
                return this._termsPayment;
            }
            set
            {
                this._termsPayment = value;
            }
        }

        public string TypeID
        {
            get
            {
                return this._typeid;
            }
            set
            {
                this._typeid = value;
            }
        }

        public string TypeName
        {
            get
            {
                return this._typeName;
            }
            set
            {
                this._typeName = value;
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

