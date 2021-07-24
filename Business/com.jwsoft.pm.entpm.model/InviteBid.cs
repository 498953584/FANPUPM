namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class InviteBid
    {
        private DateTime _AddDate = DateTime.Now;
        private string _AgentInfo = "";
        private string _BidLocus = "";
        private int _BidMode;
        private decimal _BidPaperPrice;
        private string _BidRequest = "";
        private int _BidType;
        private DateTime _CloseDate = DateTime.Now;
        private string _CollectMode = string.Empty;
        private Guid _InviteBidCode = Guid.Empty;
        private DateTime _IssueDate = DateTime.Now;
        private Guid _ProjectCode = Guid.Empty;
        private string _ProjectState = "";
        private string _Remark = string.Empty;

        public DateTime AddDate
        {
            get
            {
                return this._AddDate;
            }
            set
            {
                this._AddDate = value;
            }
        }

        public string AgentInfo
        {
            get
            {
                return this._AgentInfo;
            }
            set
            {
                this._AgentInfo = value;
            }
        }

        public string BidLocus
        {
            get
            {
                return this._BidLocus;
            }
            set
            {
                this._BidLocus = value;
            }
        }

        public int BidMode
        {
            get
            {
                return this._BidMode;
            }
            set
            {
                this._BidMode = value;
            }
        }

        public decimal BidPaperPrice
        {
            get
            {
                return this._BidPaperPrice;
            }
            set
            {
                this._BidPaperPrice = value;
            }
        }

        public string BidRequest
        {
            get
            {
                return this._BidRequest;
            }
            set
            {
                this._BidRequest = value;
            }
        }

        public int BidType
        {
            get
            {
                return this._BidType;
            }
            set
            {
                this._BidType = value;
            }
        }

        public DateTime CloseDate
        {
            get
            {
                return this._CloseDate;
            }
            set
            {
                this._CloseDate = value;
            }
        }

        public string CollectMode
        {
            get
            {
                return this._CollectMode;
            }
            set
            {
                this._CollectMode = value;
            }
        }

        public Guid InviteBidCode
        {
            get
            {
                return this._InviteBidCode;
            }
            set
            {
                this._InviteBidCode = value;
            }
        }

        public DateTime IssueDate
        {
            get
            {
                return this._IssueDate;
            }
            set
            {
                this._IssueDate = value;
            }
        }

        public Guid ProjectCode
        {
            get
            {
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }

        public string ProjectState
        {
            get
            {
                return this._ProjectState;
            }
            set
            {
                this._ProjectState = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }
    }
}

