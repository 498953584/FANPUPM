namespace AccountManage.Model
{
    using System;

    [Serializable]
    public class fund_ReqinfoModle
    {
        private decimal? _amount;
        private int? _auditstate;
        private string _id;
        private decimal? _interestnum;
        private int? _IsContr;
        private bool _isdefault;
        private bool _isinterest;
        private string _prjnum;
        private string _remark;
        private string _reqcause;
        private DateTime? _reqdate;
        private string _reqnum;
        private string _reqpeopnum;
        private int? _reqtype;
        private DateTime? _usedate;

        public decimal? amount
        {
            get
            {
                return this._amount;
            }
            set
            {
                this._amount = value;
            }
        }

        public int? auditState
        {
            get
            {
                return this._auditstate;
            }
            set
            {
                this._auditstate = value;
            }
        }

        public string id
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

        public decimal? InterestNum
        {
            get
            {
                return this._interestnum;
            }
            set
            {
                this._interestnum = value;
            }
        }

        public int? IsContr
        {
            get
            {
                return this._IsContr;
            }
            set
            {
                this._IsContr = value;
            }
        }

        public bool isDefault
        {
            get
            {
                return this._isdefault;
            }
            set
            {
                this._isdefault = value;
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

        public string PrjNum
        {
            get
            {
                return this._prjnum;
            }
            set
            {
                this._prjnum = value;
            }
        }

        public string remark
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

        public string reqCause
        {
            get
            {
                return this._reqcause;
            }
            set
            {
                this._reqcause = value;
            }
        }

        public DateTime? reqDate
        {
            get
            {
                return this._reqdate;
            }
            set
            {
                this._reqdate = value;
            }
        }

        public string reqNum
        {
            get
            {
                return this._reqnum;
            }
            set
            {
                this._reqnum = value;
            }
        }

        public string reqPeopNum
        {
            get
            {
                return this._reqpeopnum;
            }
            set
            {
                this._reqpeopnum = value;
            }
        }

        public int? reqType
        {
            get
            {
                return this._reqtype;
            }
            set
            {
                this._reqtype = value;
            }
        }

        public DateTime? useDate
        {
            get
            {
                return this._usedate;
            }
            set
            {
                this._usedate = value;
            }
        }
    }
}

