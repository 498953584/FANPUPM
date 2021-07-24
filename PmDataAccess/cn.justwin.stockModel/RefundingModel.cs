namespace cn.justwin.stockModel
{
    using System;
    using System.Data.SqlTypes;

    [Serializable]
    public class RefundingModel
    {
        private string _annx;
        private string _explain;
        private int _flowstate;
        private DateTime _intime;
        private bool _isin;
        private SqlDateTime _isintime;
        private string _person;
        private string _procode;
        private string _rcode;
        private string _rid;
        private string _tcode;

        public string annx
        {
            get
            {
                return this._annx;
            }
            set
            {
                this._annx = value;
            }
        }

        public string explain
        {
            get
            {
                return this._explain;
            }
            set
            {
                this._explain = value;
            }
        }

        public int flowstate
        {
            get
            {
                return this._flowstate;
            }
            set
            {
                this._flowstate = value;
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

        public bool isin
        {
            get
            {
                return this._isin;
            }
            set
            {
                this._isin = value;
            }
        }

        public SqlDateTime IsInTime
        {
            get
            {
                return this._isintime;
            }
            set
            {
                this._isintime = value;
            }
        }

        public string person
        {
            get
            {
                return this._person;
            }
            set
            {
                this._person = value;
            }
        }

        public string procode
        {
            get
            {
                return this._procode;
            }
            set
            {
                this._procode = value;
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

        public string rid
        {
            get
            {
                return this._rid;
            }
            set
            {
                this._rid = value;
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
    }
}

