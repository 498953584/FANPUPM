namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class PurchaseModel
    {
        private int _acceptstate;
        private string _annx;
        private string _contract;
        private string _contractName;
        private string _explain;
        private int _flowstate;
        private DateTime _intime;
        private bool _isConPurchase;
        private string _pcode = string.Empty;
        private string _person;
        private string _pid = string.Empty;
        private string _project;
        private int _purchaseType;

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

        public string contract
        {
            get
            {
                return this._contract;
            }
            set
            {
                this._contract = value;
            }
        }

        public string contractName
        {
            get
            {
                return this._contractName;
            }
            set
            {
                this._contractName = value;
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

        public bool IsConPurchase
        {
            get
            {
                return this._isConPurchase;
            }
            set
            {
                this._isConPurchase = value;
            }
        }

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

        public string pid
        {
            get
            {
                return this._pid;
            }
            set
            {
                this._pid = value;
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

        public int PurchaseType
        {
            get
            {
                return this._purchaseType;
            }
            set
            {
                this._purchaseType = value;
            }
        }
    }
}

