namespace cn.justwin.contractModel
{
    using System;

    [Serializable]
    public class IncometPaymentModel
    {
        private string _annex;
        private string _cllectioncode;
        private decimal? _cllectionprice;
        private DateTime? _cllectiontime;
        private string _cllectionuser;
        private string _contractid;
        private string _id;
        private DateTime? _inputdate;
        private string _inputperson;
        private string _monthPlanUID;
        private string _remark;
        private int state;

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

        public string CllectionCode
        {
            get
            {
                return this._cllectioncode;
            }
            set
            {
                this._cllectioncode = value;
            }
        }

        public decimal? CllectionPrice
        {
            get
            {
                return this._cllectionprice;
            }
            set
            {
                this._cllectionprice = value;
            }
        }

        public DateTime? CllectionTime
        {
            get
            {
                return this._cllectiontime;
            }
            set
            {
                this._cllectiontime = value;
            }
        }

        public string CllectionUser
        {
            get
            {
                return this._cllectionuser;
            }
            set
            {
                this._cllectionuser = value;
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

        public int State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }
    }
}

