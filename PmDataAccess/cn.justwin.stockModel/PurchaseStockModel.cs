namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class PurchaseStockModel
    {
        private string _arrivalDate;
        private string _corp;
        private decimal _number;
        private string _pscode;
        private string _psid;
        private string _scode;
        private decimal _sprice;

        public string ArrivalDate
        {
            get
            {
                return this._arrivalDate;
            }
            set
            {
                this._arrivalDate = value;
            }
        }

        public string corp
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

        public decimal number
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

        public string pscode
        {
            get
            {
                return this._pscode;
            }
            set
            {
                this._pscode = value;
            }
        }

        public string psid
        {
            get
            {
                return this._psid;
            }
            set
            {
                this._psid = value;
            }
        }

        public string scode
        {
            get
            {
                return this._scode;
            }
            set
            {
                this._scode = value;
            }
        }

        public decimal sprice
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
    }
}

