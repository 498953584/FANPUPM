namespace cn.justwin.contractModel
{
    using System;

    [Serializable]
    public class IncometBalanceModel
    {
        private string _annex;
        private string _balancemode;
        private string _balanceobject;
        private string _clearingnumber;
        private decimal? _clearingprice;
        private DateTime? _clearingtime;
        private string _clearinguser;
        private string _contractid;
        private string _id;
        private DateTime? _inputdate;
        private string _inputperson;
        private string _paymode;
        private string _remark;

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

        public string BalanceObject
        {
            get
            {
                return this._balanceobject;
            }
            set
            {
                this._balanceobject = value;
            }
        }

        public string ClearingNumber
        {
            get
            {
                return this._clearingnumber;
            }
            set
            {
                this._clearingnumber = value;
            }
        }

        public decimal? ClearingPrice
        {
            get
            {
                return this._clearingprice;
            }
            set
            {
                this._clearingprice = value;
            }
        }

        public DateTime? ClearingTime
        {
            get
            {
                return this._clearingtime;
            }
            set
            {
                this._clearingtime = value;
            }
        }

        public string ClearingUser
        {
            get
            {
                return this._clearinguser;
            }
            set
            {
                this._clearinguser = value;
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

