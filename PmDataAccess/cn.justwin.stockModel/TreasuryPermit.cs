namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class TreasuryPermit
    {
        private string _pcode;
        private string _ptype;
        private string _tcode;
        private string _tpid;

        public string pcode
        {
            get
            {
                return this._pcode;
            }
            set
            {
                this._pcode = value;
            }
        }

        public string ptype
        {
            get
            {
                return this._ptype;
            }
            set
            {
                this._ptype = value;
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

        public string tpid
        {
            get
            {
                return this._tpid;
            }
            set
            {
                this._tpid = value;
            }
        }
    }
}

