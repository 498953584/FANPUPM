namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class SmPurchaseplanModel
    {
        private int _acceptstate;
        private string _annx;
        private string _explain;
        private int _flowstate;
        private DateTime _intime;
        private string _person;
        private string _ppcode;
        private string _ppid;
        private string _project;

        public int acceptstate
        {
            get
            {
                return this._acceptstate;
            }
            set
            {
                this._acceptstate = value;
            }
        }

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

        public string ppcode
        {
            get
            {
                return this._ppcode;
            }
            set
            {
                this._ppcode = value;
            }
        }

        public string ppid
        {
            get
            {
                return this._ppid;
            }
            set
            {
                this._ppid = value;
            }
        }

        public string Project
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
    }
}

