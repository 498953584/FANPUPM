namespace cn.justwin.contractModel
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    public class PayoutModifyModel
    {
        private string _annex;
        private string _contractid;
        private string _contractname;
        private int? _flowstate;
        private DateTime? _inputdate;
        private string _inputperson;
        private string _modifycode;
        private DateTime? _modifydate;
        private string _modifyid;
        private decimal? _modifymoney;
        private string _modifyperson;
        private string _notes;
        private string _reason;
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

        public string ModifyCode
        {
            get
            {
                return this._modifycode;
            }
            set
            {
                this._modifycode = value;
            }
        }

        public DateTime? ModifyDate
        {
            get
            {
                return this._modifydate;
            }
            set
            {
                this._modifydate = value;
            }
        }

        public string ModifyID
        {
            get
            {
                return this._modifyid;
            }
            set
            {
                this._modifyid = value;
            }
        }

        public decimal? ModifyMoney
        {
            get
            {
                return this._modifymoney;
            }
            set
            {
                this._modifymoney = value;
            }
        }

        public string ModifyPerson
        {
            get
            {
                return this._modifyperson;
            }
            set
            {
                this._modifyperson = value;
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

        public string PrjGuid { get; set; }

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

