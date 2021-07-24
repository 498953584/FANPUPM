namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class SmWastageStockModel
    {
        private string _corp;
        private string _id;
        private decimal _number;
        private string _resourceCode;
        private decimal _sprice;
        private string _wastageCode;

        public string Corp
        {
            get
            {
                return this._corp;
            }
            set
            {
                this._corp = value;
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

        public decimal Number
        {
            get
            {
                return this._number;
            }
            set
            {
                this._number = value;
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

        public decimal Sprice
        {
            get
            {
                return this._sprice;
            }
            set
            {
                this._sprice = value;
            }
        }

        public string WastageCode
        {
            get
            {
                return this._wastageCode;
            }
            set
            {
                this._wastageCode = value;
            }
        }
    }
}

