namespace cn.justwin.Fund.Account
{
    using System;

    [Serializable]
    public class ActivityModel
    {
        private string _accountid;
        private int? _accountstate;
        private DateTime? _activeDate;
        private string _activeman;
        private DateTime? _closeDate;
        private string _closeman;

        public string AccountID
        {
            get
            {
                return this._accountid;
            }
            set
            {
                this._accountid = value;
            }
        }

        public int? AccountState
        {
            get
            {
                return this._accountstate;
            }
            set
            {
                this._accountstate = value;
            }
        }

        public DateTime? activeDate
        {
            get
            {
                return this._activeDate;
            }
            set
            {
                this._activeDate = value;
            }
        }

        public string activeMan
        {
            get
            {
                return this._activeman;
            }
            set
            {
                this._activeman = value;
            }
        }

        public DateTime? closeDate
        {
            get
            {
                return this._closeDate;
            }
            set
            {
                this._closeDate = value;
            }
        }

        public string closeMan
        {
            get
            {
                return this._closeman;
            }
            set
            {
                this._closeman = value;
            }
        }
    }
}

