namespace cn.justwin.Fund.RequestPayment.Model
{
    using System;

    [Serializable]
    public class RequestPayDetailModel
    {
        private string _contractid;
        private bool _isinterest;
        private int _orderid;
        private Guid _planuid;
        private string _remark;
        private string _rpaycause;
        private Guid _rpaymainid;
        private decimal? _rpaymoney;
        private string _rpaysubject;
        private Guid _rpayuid;
        private string _rpayuser;

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

        public bool isInterest
        {
            get
            {
                return this._isinterest;
            }
            set
            {
                this._isinterest = value;
            }
        }

        public int OrderID
        {
            get
            {
                return this._orderid;
            }
            set
            {
                this._orderid = value;
            }
        }

        public Guid PlanUID
        {
            get
            {
                return this._planuid;
            }
            set
            {
                this._planuid = value;
            }
        }

        public string ReMark
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

        public string RPayCause
        {
            get
            {
                return this._rpaycause;
            }
            set
            {
                this._rpaycause = value;
            }
        }

        public Guid RPayMainID
        {
            get
            {
                return this._rpaymainid;
            }
            set
            {
                this._rpaymainid = value;
            }
        }

        public decimal? RPayMoney
        {
            get
            {
                return this._rpaymoney;
            }
            set
            {
                this._rpaymoney = value;
            }
        }

        public string RPaysubject
        {
            get
            {
                return this._rpaysubject;
            }
            set
            {
                this._rpaysubject = value;
            }
        }

        public Guid RPayUID
        {
            get
            {
                return this._rpayuid;
            }
            set
            {
                this._rpayuid = value;
            }
        }

        public string RPayUser
        {
            get
            {
                return this._rpayuser;
            }
            set
            {
                this._rpayuser = value;
            }
        }
    }
}

