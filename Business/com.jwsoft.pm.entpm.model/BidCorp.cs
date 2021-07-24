namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class BidCorp
    {
        private decimal _BidPrice;
        private int _CorpCode;
        private Guid _InviteBidCode = Guid.Empty;
        private bool _IsJoin;
        private bool _IsWin;
        private int _ManagerTitle;
        private int _NoteID;
        private int _ResultCode;
        private DateTime _SubmitBidDate = DateTime.Now;
        private string _Summary = string.Empty;
        private int _WorkDay = 1;

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

        public int CorpCode
        {
            get
            {
                return this._CorpCode;
            }
            set
            {
                this._CorpCode = value;
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

        public bool IsJoin
        {
            get
            {
                return this._IsJoin;
            }
            set
            {
                this._IsJoin = value;
            }
        }

        public bool IsWin
        {
            get
            {
                return this._IsWin;
            }
            set
            {
                this._IsWin = value;
            }
        }

        public int ManagerTitle
        {
            get
            {
                return this._ManagerTitle;
            }
            set
            {
                this._ManagerTitle = value;
            }
        }

        public int NoteID
        {
            get
            {
                return this._NoteID;
            }
            set
            {
                this._NoteID = value;
            }
        }

        public int ResultCode
        {
            get
            {
                return this._ResultCode;
            }
            set
            {
                this._ResultCode = value;
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

        public string Summary
        {
            get
            {
                return this._Summary;
            }
            set
            {
                this._Summary = value;
            }
        }

        public int WorkDay
        {
            get
            {
                return this._WorkDay;
            }
            set
            {
                this._WorkDay = value;
            }
        }
    }
}

