namespace cn.justwin.Fund.PlanSummary.Model
{
    using System;

    [Serializable]
    public class PlanSummaryDetail
    {
        private decimal? _currplanmoney;
        private Guid _did;
        private string _inputpeople;
        private DateTime? _inputtime;
        private decimal? _lastactualmoney;
        private decimal? _lastplanmoney;
        private string _mid;
        private string _planid;
        private string _prjid;
        private string _remark;

        public decimal? CurrPlanMoney
        {
            get
            {
                return this._currplanmoney;
            }
            set
            {
                this._currplanmoney = value;
            }
        }

        public Guid DID
        {
            get
            {
                return this._did;
            }
            set
            {
                this._did = value;
            }
        }

        public string InputPeople
        {
            get
            {
                return this._inputpeople;
            }
            set
            {
                this._inputpeople = value;
            }
        }

        public DateTime? InputTime
        {
            get
            {
                return this._inputtime;
            }
            set
            {
                this._inputtime = value;
            }
        }

        public decimal? LastActualMoney
        {
            get
            {
                return this._lastactualmoney;
            }
            set
            {
                this._lastactualmoney = value;
            }
        }

        public decimal? LastPlanMoney
        {
            get
            {
                return this._lastplanmoney;
            }
            set
            {
                this._lastplanmoney = value;
            }
        }

        public string MID
        {
            get
            {
                return this._mid;
            }
            set
            {
                this._mid = value;
            }
        }

        public string PlanID
        {
            get
            {
                return this._planid;
            }
            set
            {
                this._planid = value;
            }
        }

        public string PrjID
        {
            get
            {
                return this._prjid;
            }
            set
            {
                this._prjid = value;
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

