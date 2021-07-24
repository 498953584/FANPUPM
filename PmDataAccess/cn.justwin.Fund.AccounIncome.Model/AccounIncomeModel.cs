namespace cn.justwin.Fund.AccounIncome.Model
{
    using System;

    public class AccounIncomeModel
    {
        private string _contractid;
        private string _ContractName;
        private int? _flowstate;
        private DateTime? _getdate;
        private decimal? _getmoney;
        private string _incode;
        private DateTime? _indate;
        private decimal? _inmoney;
        private string _inpeople;
        private Guid _inuid;
        private string _planuid;
        private string _prjguid;
        private string _remark;
        private string _subject;

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
                return this._ContractName;
            }
            set
            {
                this._ContractName = value;
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

        public DateTime? GetDate
        {
            get
            {
                return this._getdate;
            }
            set
            {
                this._getdate = value;
            }
        }

        public decimal? GetMoney
        {
            get
            {
                return this._getmoney;
            }
            set
            {
                this._getmoney = value;
            }
        }

        public string InCode
        {
            get
            {
                return this._incode;
            }
            set
            {
                this._incode = value;
            }
        }

        public DateTime? InDate
        {
            get
            {
                return this._indate;
            }
            set
            {
                this._indate = value;
            }
        }

        public decimal? InMoney
        {
            get
            {
                return this._inmoney;
            }
            set
            {
                this._inmoney = value;
            }
        }

        public string InPeople
        {
            get
            {
                return this._inpeople;
            }
            set
            {
                this._inpeople = value;
            }
        }

        public Guid InUid
        {
            get
            {
                return this._inuid;
            }
            set
            {
                this._inuid = value;
            }
        }

        public string PlanUid
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

        public string PrjGuid
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

        public string Subject
        {
            get
            {
                return this._subject;
            }
            set
            {
                this._subject = value;
            }
        }
    }
}

