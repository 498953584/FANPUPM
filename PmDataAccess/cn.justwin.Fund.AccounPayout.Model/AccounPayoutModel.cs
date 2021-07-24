namespace cn.justwin.Fund.AccounPayout.Model
{
    using System;

    [Serializable]
    public class AccounPayoutModel
    {
        private string _ContractName;
        private int? _floestate;
        private string _handler;
        private string _payoutcode;
        private Guid _payoutguid;
        private decimal? _payoutmoney;
        private string _payoutpeople;
        private DateTime? _payouttime;
        private string _prjguid;
        private string _remark;
        private string _rpguid;
        private int? _type;
        private DateTime? _updatetime;
        private string _updateuser;

        public string ContractName
        {
            get
            {
                return this._ContractName;
            }
            set
            {
                this._ContractName = value;
            }
        }

        public int? FloeState
        {
            get
            {
                return this._floestate;
            }
            set
            {
                this._floestate = value;
            }
        }

        public string Handler
        {
            get
            {
                return this._handler;
            }
            set
            {
                this._handler = value;
            }
        }

        public string PayOutCode
        {
            get
            {
                return this._payoutcode;
            }
            set
            {
                this._payoutcode = value;
            }
        }

        public Guid PayOutGuid
        {
            get
            {
                return this._payoutguid;
            }
            set
            {
                this._payoutguid = value;
            }
        }

        public decimal? PayOutMoney
        {
            get
            {
                return this._payoutmoney;
            }
            set
            {
                this._payoutmoney = value;
            }
        }

        public string PayOutPeople
        {
            get
            {
                return this._payoutpeople;
            }
            set
            {
                this._payoutpeople = value;
            }
        }

        public DateTime? PayOutTime
        {
            get
            {
                return this._payouttime;
            }
            set
            {
                this._payouttime = value;
            }
        }

        public string prjGuid
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

        public string RPGuid
        {
            get
            {
                return this._rpguid;
            }
            set
            {
                this._rpguid = value;
            }
        }

        public int? Type
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

        public DateTime? UpdateTime
        {
            get
            {
                return this._updatetime;
            }
            set
            {
                this._updatetime = value;
            }
        }

        public string UpdateUser
        {
            get
            {
                return this._updateuser;
            }
            set
            {
                this._updateuser = value;
            }
        }
    }
}

