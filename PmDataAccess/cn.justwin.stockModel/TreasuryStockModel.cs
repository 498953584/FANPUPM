namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class TreasuryStockModel
    {
        private string _corp;
        private string _incode;
        private DateTime _intime;
        private int _intype;
        private bool _isfirst;
        private string _scode;
        private decimal _snumber;
        private decimal _sprice;
        private string _tcode;
        private string _tsid;
        private string _type;

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

        public string incode
        {
            get
            {
                return this._incode;
            }
            set
            {
                this._incode = value;
            }
        }

        public DateTime intime
        {
            get
            {
                return this._intime;
            }
            set
            {
                this._intime = value;
            }
        }

        public int intype
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

        public bool isfirst
        {
            get
            {
                return this._isfirst;
            }
            set
            {
                this._isfirst = value;
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

        public decimal snumber
        {
            get
            {
                return this._snumber;
            }
            set
            {
                this._snumber = value;
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

        public string tcode
        {
            get
            {
                return this._tcode;
            }
            set
            {
                this._tcode = value;
            }
        }

        public string tsid
        {
            get
            {
                return this._tsid;
            }
            set
            {
                this._tsid = value;
            }
        }

        public string Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}

