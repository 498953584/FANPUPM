namespace cn.justwin.contractModel
{
    using System;

    [Serializable]
    public class IncometModifyModel
    {
        private string _annex;
        private string _changecode;
        private decimal? _changeprice;
        private string _changereason;
        private DateTime? _changetime;
        private string _contractid;
        private string _id;
        private DateTime? _inputdate;
        private string _inputperson;
        private string _remark;
        private string _transactor;

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

        public string ChangeCode
        {
            get
            {
                return this._changecode;
            }
            set
            {
                this._changecode = value;
            }
        }

        public decimal? ChangePrice
        {
            get
            {
                return this._changeprice;
            }
            set
            {
                this._changeprice = value;
            }
        }

        public string ChangeReason
        {
            get
            {
                return this._changereason;
            }
            set
            {
                this._changereason = value;
            }
        }

        public DateTime? ChangeTime
        {
            get
            {
                return this._changetime;
            }
            set
            {
                this._changetime = value;
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

        public string Transactor
        {
            get
            {
                return this._transactor;
            }
            set
            {
                this._transactor = value;
            }
        }
    }
}

