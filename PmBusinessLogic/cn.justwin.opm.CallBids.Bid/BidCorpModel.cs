namespace cn.justwin.opm.CallBids.Bid
{
    using System;

    public class BidCorpModel
    {
        private DateTime _addtime;
        private string _adduser;
        private string _biddept;
        private string _biddeptaddr;
        private decimal _bidmark;
        private decimal _bidmoney;
        private string _bidsuccessful;
        private DateTime _bidtime;
        private string _biduser;
        private string _bidusermobile;
        private string _bidusertel;
        private Guid _callbidid;
        private string _constructiontime;
        private int _corpid;
        private int _flowstate;
        private int _i_xh;
        private string _isvalid;
        private string _remark;
        private Guid _uid;

        public DateTime AddTime
        {
            get
            {
                return this._addtime;
            }
            set
            {
                this._addtime = value;
            }
        }

        public string AddUser
        {
            get
            {
                return this._adduser;
            }
            set
            {
                this._adduser = value;
            }
        }

        public string BidDept
        {
            get
            {
                return this._biddept;
            }
            set
            {
                this._biddept = value;
            }
        }

        public string BidDeptAddr
        {
            get
            {
                return this._biddeptaddr;
            }
            set
            {
                this._biddeptaddr = value;
            }
        }

        public decimal BidMark
        {
            get
            {
                return this._bidmark;
            }
            set
            {
                this._bidmark = value;
            }
        }

        public decimal BidMoney
        {
            get
            {
                return this._bidmoney;
            }
            set
            {
                this._bidmoney = value;
            }
        }

        public DateTime BidTime
        {
            get
            {
                return this._bidtime;
            }
            set
            {
                this._bidtime = value;
            }
        }

        public string BidUser
        {
            get
            {
                return this._biduser;
            }
            set
            {
                this._biduser = value;
            }
        }

        public string BidUserMobile
        {
            get
            {
                return this._bidusermobile;
            }
            set
            {
                this._bidusermobile = value;
            }
        }

        public string BidUserTel
        {
            get
            {
                return this._bidusertel;
            }
            set
            {
                this._bidusertel = value;
            }
        }

        public Guid CallBidID
        {
            get
            {
                return this._callbidid;
            }
            set
            {
                this._callbidid = value;
            }
        }

        public string ConstructionTime
        {
            get
            {
                return this._constructiontime;
            }
            set
            {
                this._constructiontime = value;
            }
        }

        public int CorpID
        {
            get
            {
                return this._corpid;
            }
            set
            {
                this._corpid = value;
            }
        }

        public int FlowState
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

        public int I_xh
        {
            get
            {
                return this._i_xh;
            }
            set
            {
                this._i_xh = value;
            }
        }

        public string IsValid
        {
            get
            {
                return this._isvalid;
            }
            set
            {
                this._isvalid = value;
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

