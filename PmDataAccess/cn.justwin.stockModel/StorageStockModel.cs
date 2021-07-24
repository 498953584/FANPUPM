namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class StorageStockModel
    {
        private string _checkCondition;
        private string _corp;
        private string _intype;
        private string _linkcode;
        private decimal _number;
        private string _scode;
        private decimal _sprice;
        private string _ssid;
        private string _stcode;

        public string CheckCondition
        {
            get
            {
                return this._checkCondition;
            }
            set
            {
                this._checkCondition = value;
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

        public string intype
        {
            get
            {
                return this._intype;
            }
            set
            {
                this._intype = value;
            }
        }

        public string linkcode
        {
            get
            {
                return this._linkcode;
            }
            set
            {
                this._linkcode = value;
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

        public string ssid
        {
            get
            {
                return this._ssid;
            }
            set
            {
                this._ssid = value;
            }
        }

        public string stcode
        {
            get
            {
                return this._stcode;
            }
            set
            {
                this._stcode = value;
            }
        }
    }
}

