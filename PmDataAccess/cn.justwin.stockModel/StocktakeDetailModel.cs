namespace cn.justwin.stockModel
{
    using System;

    public class StocktakeDetailModel
    {
        private decimal _bookNum;
        private decimal _firstStorageNum;
        private string _id;
        private DateTime _inputDate;
        private decimal _lastMonthNum;
        private string _note;
        private decimal _outReserveNum;
        private decimal _price;
        private decimal _refundingNum;
        private string _resourceCode;
        private string _resourceName;
        private string _specification;
        private string _stocktakeId;
        private decimal _stocktakeNum;
        private decimal _storageNum;
        private string _supplier;
        private string _supplierId;
        private decimal _transferringInNum;
        private decimal _transferringOutNum;
        private string _unit;
        private decimal _wastageNum;

        public decimal BookNum
        {
            get
            {
                return this._bookNum;
            }
            set
            {
                this._bookNum = value;
            }
        }

        public decimal FirstStorageNum
        {
            get
            {
                return this._firstStorageNum;
            }
            set
            {
                this._firstStorageNum = value;
            }
        }

        public string Id
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

        public DateTime InputDate
        {
            get
            {
                return this._inputDate;
            }
            set
            {
                this._inputDate = value;
            }
        }

        public decimal LastMonthNum
        {
            get
            {
                return this._lastMonthNum;
            }
            set
            {
                this._lastMonthNum = value;
            }
        }

        public string Note
        {
            get
            {
                return this._note;
            }
            set
            {
                this._note = value;
            }
        }

        public decimal OutReserveNum
        {
            get
            {
                return this._outReserveNum;
            }
            set
            {
                this._outReserveNum = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this._price;
            }
            set
            {
                this._price = value;
            }
        }

        public decimal RefundingNum
        {
            get
            {
                return this._refundingNum;
            }
            set
            {
                this._refundingNum = value;
            }
        }

        public string ResourceCode
        {
            get
            {
                return this._resourceCode;
            }
            set
            {
                this._resourceCode = value;
            }
        }

        public string ResourceName
        {
            get
            {
                return this._resourceName;
            }
            set
            {
                this._resourceName = value;
            }
        }

        public string Specification
        {
            get
            {
                return this._specification;
            }
            set
            {
                this._specification = value;
            }
        }

        public string StocktakeId
        {
            get
            {
                return this._stocktakeId;
            }
            set
            {
                this._stocktakeId = value;
            }
        }

        public decimal StocktakeNum
        {
            get
            {
                return this._stocktakeNum;
            }
            set
            {
                this._stocktakeNum = value;
            }
        }

        public decimal StorageNum
        {
            get
            {
                return this._storageNum;
            }
            set
            {
                this._storageNum = value;
            }
        }

        public string Supplier
        {
            get
            {
                return this._supplier;
            }
            set
            {
                this._supplier = value;
            }
        }

        public string SupplierId
        {
            get
            {
                return this._supplierId;
            }
            set
            {
                this._supplierId = value;
            }
        }

        public decimal TransferringInNum
        {
            get
            {
                return this._transferringInNum;
            }
            set
            {
                this._transferringInNum = value;
            }
        }

        public decimal TransferringOutNum
        {
            get
            {
                return this._transferringOutNum;
            }
            set
            {
                this._transferringOutNum = value;
            }
        }

        public string Unit
        {
            get
            {
                return this._unit;
            }
            set
            {
                this._unit = value;
            }
        }

        public decimal WastageNum
        {
            get
            {
                return this._wastageNum;
            }
            set
            {
                this._wastageNum = value;
            }
        }
    }
}

