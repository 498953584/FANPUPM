namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class BidInfo
    {
        private int _BidDeptID;
        private decimal _BidPrice;
        private string _BidProjectManager = string.Empty;
        private string _BidRemark = "";
        private string _CopySum = string.Empty;
        private string _Envelopment = string.Empty;
        private Guid _InviteBidCode = Guid.Empty;
        private string _OriginalSum = string.Empty;
        private string _Principal = string.Empty;
        private DateTime _SubmitBailDate = DateTime.Now;
        private DateTime _SubmitBidDate = DateTime.Now;
        private DateTime _SubmitCetificateDate = DateTime.Now;

        public int BidDeptID
        {
            get
            {
                return this._BidDeptID;
            }
            set
            {
                this._BidDeptID = value;
            }
        }

        public decimal BidPrice
        {
            get
            {
                return this._BidPrice;
            }
            set
            {
                this._BidPrice = value;
            }
        }

        public string BidProjectManager
        {
            get
            {
                return this._BidProjectManager;
            }
            set
            {
                this._BidProjectManager = value;
            }
        }

        public string BidRemark
        {
            get
            {
                return this._BidRemark;
            }
            set
            {
                this._BidRemark = value;
            }
        }

        public string CopySum
        {
            get
            {
                return this._CopySum;
            }
            set
            {
                this._CopySum = value;
            }
        }

        public string Envelopment
        {
            get
            {
                return this._Envelopment;
            }
            set
            {
                this._Envelopment = value;
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

        public string OriginalSum
        {
            get
            {
                return this._OriginalSum;
            }
            set
            {
                this._OriginalSum = value;
            }
        }

        public string Principal
        {
            get
            {
                return this._Principal;
            }
            set
            {
                this._Principal = value;
            }
        }

        public DateTime SubmitBailDate
        {
            get
            {
                return this._SubmitBailDate;
            }
            set
            {
                this._SubmitBailDate = value;
            }
        }

        public DateTime SubmitBidDate
        {
            get
            {
                return this._SubmitBidDate;
            }
            set
            {
                this._SubmitBidDate = value;
            }
        }

        public DateTime SubmitCetificateDate
        {
            get
            {
                return this._SubmitCetificateDate;
            }
            set
            {
                this._SubmitCetificateDate = value;
            }
        }
    }
}

