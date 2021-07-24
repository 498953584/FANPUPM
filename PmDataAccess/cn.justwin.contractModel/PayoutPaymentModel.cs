namespace cn.justwin.contractModel
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class PayoutPaymentModel
    {
        private string _account;
        private string _annex;
        private string _bank;
        private string _beneficiary;
        private string _capitalnumber;
        private bool _containpending;
        private string _contractid;
        private string _contractname;
        private int? _flowstate;
        private string _id;
        private DateTime? _inputdate;
        private string _inputperson;
        private string _monthPlanUID;
        private string _notes;
        private string _paymentcode;
        private DateTime? _paymentdate;
        private decimal? _paymentmoney;
        private string _paymentperson;
        private int _paytype;
        private decimal? _pidcont;
        private string userCodes;

        public string Account
        {
            get
            {
                return this._account;
            }
            set
            {
                this._account = value;
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

        public string Bank
        {
            get
            {
                return this._bank;
            }
            set
            {
                this._bank = value;
            }
        }

        public string Beneficiary
        {
            get
            {
                return this._beneficiary;
            }
            set
            {
                this._beneficiary = value;
            }
        }

        public string Capitalnumber
        {
            get
            {
                return this._capitalnumber;
            }
            set
            {
                this._capitalnumber = value;
            }
        }

        public bool ContainPending
        {
            get
            {
                return this._containpending;
            }
            set
            {
                this._containpending = value;
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

        public string ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
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

        public string MonthPlanUID
        {
            get
            {
                return this._monthPlanUID;
            }
            set
            {
                this._monthPlanUID = value;
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

        public string PaymentCode
        {
            get
            {
                return this._paymentcode;
            }
            set
            {
                this._paymentcode = value;
            }
        }

        public DateTime? PaymentDate
        {
            get
            {
                return this._paymentdate;
            }
            set
            {
                this._paymentdate = value;
            }
        }

        public decimal? PaymentMoney
        {
            get
            {
                return this._paymentmoney;
            }
            set
            {
                this._paymentmoney = value;
            }
        }

        public string PaymentPerson
        {
            get
            {
                return this._paymentperson;
            }
            set
            {
                this._paymentperson = value;
            }
        }

        public int Paytype
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

        public decimal? Pidcont
        {
            get
            {
                return this._pidcont;
            }
            set
            {
                this._pidcont = value;
            }
        }

        public string PrjGuid { get; set; }

        public string UserCodes
        {
            get
            {
                return this.userCodes;
            }
            set
            {
                this.userCodes = value;
            }
        }
    }
}

