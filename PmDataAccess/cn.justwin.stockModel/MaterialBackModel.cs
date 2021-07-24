namespace cn.justwin.stockModel
{
    using System;

    public class MaterialBackModel
    {
        private string _annx;
        private string _explain;
        private int _flowstate;
        private DateTime _intime;
        private bool _isin;
        private string _person;
        private string _procode;
        private string _rcode;
        private string _rid;
        private string _tcode;

        public string Annx
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

        public string Explain
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

        public int FlowState
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

        public DateTime InTime
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

        public bool IsIn
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

        public string Person
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

        public string Procode
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

        public string Rcode
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

        public string Rid
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
            set
            {
                this._rid = value;
            }
        }

        public string Tcode
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

