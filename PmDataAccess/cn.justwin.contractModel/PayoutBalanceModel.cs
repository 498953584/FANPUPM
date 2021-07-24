namespace cn.justwin.contractModel
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class PayoutBalanceModel
    {
        private string _annex;
        private string _balancecode;
        private DateTime? _balancedate;
        private string _balanceid;
        private string _balancemode;
        private decimal? _balancemoney;
        private string _balanceobj;
        private string _balanceperson;
        private bool _containpending;
        private string _contractid;
        private string _contractname;
        private int? _flowstate;
        private DateTime? _inputdate;
        private string _inputperson;
        private string _notes;
        private string _paymode;
        private string userCodes;

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

        public string BalanceCode
        {
            get
            {
                return this._balancecode;
            }
            set
            {
                this._balancecode = value;
            }
        }

        public DateTime? BalanceDate
        {
            get
            {
                return this._balancedate;
            }
            set
            {
                this._balancedate = value;
            }
        }

        public string BalanceID
        {
            get
            {
                return this._balanceid;
            }
            set
            {
                this._balanceid = value;
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

        public decimal? BalanceMoney
        {
            get
            {
                return this._balancemoney;
            }
            set
            {
                this._balancemoney = value;
            }
        }

        public string BalanceObject
        {
            get
            {
                return this._balanceobj;
            }
            set
            {
                this._balanceobj = value;
            }
        }

        public string BalancePerson
        {
            get
            {
                return this._balanceperson;
            }
            set
            {
                this._balanceperson = value;
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

