namespace cn.justwin.stockModel
{
    using System;
    using System.Data.SqlTypes;

    [Serializable]
    public class StorageModel
    {
        private string _annx;
        private string _explain;
        private int _flowstate;
        private bool _inflag;
        private DateTime _intime;
        private bool _isfirst;
        private SqlDateTime _isintime;
        private string _person;
        private string _project;
        private string _scode;
        private string _sid;
        private string _supervisor;
        private string _tcode;
        private string _trustee;

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

        public bool inflag
        {
            get
            {
                return this._inflag;
            }
            set
            {
                this._inflag = value;
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

        public string project
        {
            get
            {
                return this._project;
            }
            set
            {
                this._project = value;
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

        public string sid
        {
            get
            {
                return this._sid;
            }
            set
            {
                this._sid = value;
            }
        }

        public string Supervisor
        {
            get
            {
                return this._supervisor;
            }
            set
            {
                this._supervisor = value;
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

        public string Trustee
        {
            get
            {
                return this._trustee;
            }
            set
            {
                this._trustee = value;
            }
        }
    }
}

