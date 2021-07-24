namespace cn.justwin.Fund.MonthPlan
{
    using System;

    [Serializable]
    public class MonthDetailModel
    {
        private string _contractid;
        private Guid _monthplanid;
        private decimal? _oldbalance;
        private int? _orderid;
        private decimal? _planmoney;
        private string _plansubject;
        private string _remark;
        private decimal? _thisbalance;
        private Guid _uid;

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

        public Guid MonthPlanID
        {
            get
            {
                return this._monthplanid;
            }
            set
            {
                this._monthplanid = value;
            }
        }

        public decimal? OldBalance
        {
            get
            {
                return this._oldbalance;
            }
            set
            {
                this._oldbalance = value;
            }
        }

        public int? OrderID
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

        public decimal? PlanMoney
        {
            get
            {
                return this._planmoney;
            }
            set
            {
                this._planmoney = value;
            }
        }

        public string Plansubject
        {
            get
            {
                return this._plansubject;
            }
            set
            {
                this._plansubject = value;
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

        public decimal? ThisBalance
        {
            get
            {
                return this._thisbalance;
            }
            set
            {
                this._thisbalance = value;
            }
        }

        public Guid UID
        {
            get
            {
                return this._uid;
            }
            set
            {
                this._uid = value;
            }
        }
    }
}

