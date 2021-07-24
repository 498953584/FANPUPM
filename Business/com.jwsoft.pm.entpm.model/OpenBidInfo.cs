namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class OpenBidInfo
    {
        private decimal _AveragePrice;
        private string _BidCorpPower = string.Empty;
        private decimal _BidPrice;
        private int _BingoCorp = -2147483648;
        private string _EvaluatingMode = string.Empty;
        private Guid _InvitedBidCode = Guid.Empty;
        private decimal _LowerLimit;
        private DateTime _OpenBidDate = DateTime.Now;
        private string _OwnerCorpPower = string.Empty;
        private decimal _PrimeDriftRate;
        private decimal _PrimePrice;
        private Guid _ProjectCode = Guid.Empty;
        private string _Remark = string.Empty;
        private decimal _UpperLimit;

        public decimal AveragePrice
        {
            get
            {
                return this._AveragePrice;
            }
            set
            {
                this._AveragePrice = value;
            }
        }

        public string BidCorpPower
        {
            get
            {
                return this._BidCorpPower;
            }
            set
            {
                this._BidCorpPower = value;
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

        public int BingoCorp
        {
            get
            {
                return this._BingoCorp;
            }
            set
            {
                this._BingoCorp = value;
            }
        }

        public string EvaluatingMode
        {
            get
            {
                return this._EvaluatingMode;
            }
            set
            {
                this._EvaluatingMode = value;
            }
        }

        public Guid InvitedBidCode
        {
            get
            {
                return this._InvitedBidCode;
            }
            set
            {
                this._InvitedBidCode = value;
            }
        }

        public decimal LowerLimit
        {
            get
            {
                return this._LowerLimit;
            }
            set
            {
                this._LowerLimit = value;
            }
        }

        public DateTime OpenBidDate
        {
            get
            {
                return this._OpenBidDate;
            }
            set
            {
                this._OpenBidDate = value;
            }
        }

        public string OwnerCorpPower
        {
            get
            {
                return this._OwnerCorpPower;
            }
            set
            {
                this._OwnerCorpPower = value;
            }
        }

        public decimal PrimeDriftRate
        {
            get
            {
                return this._PrimeDriftRate;
            }
            set
            {
                this._PrimeDriftRate = value;
            }
        }

        public decimal PrimePrice
        {
            get
            {
                return this._PrimePrice;
            }
            set
            {
                this._PrimePrice = value;
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

        public decimal UpperLimit
        {
            get
            {
                return this._UpperLimit;
            }
            set
            {
                this._UpperLimit = value;
            }
        }
    }
}

