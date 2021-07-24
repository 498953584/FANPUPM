namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class StocktakeModel
    {
        private DateTime _beginDate;
        private string _code;
        private DateTime _endDate;
        private int _flowState;
        private string _id;
        private DateTime _inputDate;
        private string _inputUser;
        private DateTime _lockDate;
        private string _name;
        private string _note;
        private int _state;
        private string _stateName;
        private string _stocktakeDate;
        private string _treasuryCode;
        private string _treasuryName;

        public DateTime BeginDate
        {
            get
            {
                return this._beginDate;
            }
            set
            {
                this._beginDate = value;
            }
        }

        public string Code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this._endDate;
            }
            set
            {
                this._endDate = value;
            }
        }

        public int FlowState
        {
            get
            {
                return this._flowState;
            }
            set
            {
                this._flowState = value;
            }
        }

        public string Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public DateTime InputDate
        {
            get
            {
                return this._inputDate;
            }
            set
            {
                this._inputDate = value;
            }
        }

        public string InputUser
        {
            get
            {
                return this._inputUser;
            }
            set
            {
                this._inputUser = value;
            }
        }

        public DateTime LockDate
        {
            get
            {
                return this._lockDate;
            }
            set
            {
                this._lockDate = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public string Note
        {
            get
            {
                return this._note;
            }
            set
            {
                this._note = value;
            }
        }

        public int State
        {
            get
            {
                return this._state;
            }
            set
            {
                this._state = value;
            }
        }

        public string StateName
        {
            get
            {
                return this._stateName;
            }
            set
            {
                this._stateName = value;
            }
        }

        public string StocktakeDate
        {
            get
            {
                return this._stocktakeDate;
            }
            set
            {
                this._stocktakeDate = value;
            }
        }

        public string TreasuryCode
        {
            get
            {
                return this._treasuryCode;
            }
            set
            {
                this._treasuryCode = value;
            }
        }

        public string TreasuryName
        {
            get
            {
                return this._treasuryName;
            }
            set
            {
                this._treasuryName = value;
            }
        }
    }
}

