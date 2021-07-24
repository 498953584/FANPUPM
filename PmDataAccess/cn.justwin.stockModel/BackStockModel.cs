namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class BackStockModel
    {
        private string _corp;
        private string _intype;
        private string _linkcode;
        private decimal _number;
        private string _rcode;
        private string _rsid;
        private string _scode;
        private decimal _sprice;
        private string _taskId;

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

        public string rcode
        {
            get
            {
                return this._rcode;
            }
            set
            {
                this._rcode = value;
            }
        }

        public string rsid
        {
            get
            {
                return this._rsid;
            }
            set
            {
                this._rsid = value;
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

        public string taskId
        {
            get
            {
                return this._taskId;
            }
            set
            {
                this._taskId = value;
            }
        }
    }
}

