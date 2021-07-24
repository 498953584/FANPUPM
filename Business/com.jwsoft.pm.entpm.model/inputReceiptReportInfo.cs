namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class inputReceiptReportInfo
    {
        private decimal _InputAmount;
        private decimal _InputCompAmount;
        private bool _IsProject;
        private string _PrjName;
        private decimal _ReceiptAmount;
        private decimal _ReceiptCompAmount;

        public decimal InputAmount
        {
            get
            {
                return this._InputAmount;
            }
            set
            {
                this._InputAmount = value;
            }
        }

        public decimal InputCompAmount
        {
            get
            {
                return this._InputCompAmount;
            }
            set
            {
                this._InputCompAmount = decimal.Round(value, 4);
            }
        }

        public decimal InputCompRate
        {
            get
            {
                if (this._InputAmount != 0M)
                {
                    return (decimal.Round(this._InputCompAmount / this._InputAmount, 4) * 100M);
                }
                return 0M;
            }
        }

        public bool IsProject
        {
            get
            {
                return this._IsProject;
            }
            set
            {
                this._IsProject = value;
            }
        }

        public string PrjName
        {
            get
            {
                return this._PrjName;
            }
            set
            {
                this._PrjName = value;
            }
        }

        public decimal ReceiptAmount
        {
            get
            {
                return this._ReceiptAmount;
            }
            set
            {
                this._ReceiptAmount = value;
            }
        }

        public decimal ReceiptCompAmount
        {
            get
            {
                return this._ReceiptCompAmount;
            }
            set
            {
                this._ReceiptCompAmount = decimal.Round(value, 4);
            }
        }

        public decimal ReceiptCompRate
        {
            get
            {
                if (this._ReceiptAmount != 0M)
                {
                    return (decimal.Round(this._ReceiptCompAmount / this._ReceiptAmount, 4) * 100M);
                }
                return 0M;
            }
        }
    }
}

