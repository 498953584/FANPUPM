namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class SmWastageModel
    {
        private string _code;
        private string _explain;
        private int _flowstate;
        private string _id;
        private DateTime _inputDate;
        private string _inputPerson;
        private bool _isout;
        private DateTime? _isOutTime;
        private string _treasurycode;
        private string _treasuryName;

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

        public int Flowstate
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

        public string InputPerson
        {
            get
            {
                return this._inputPerson;
            }
            set
            {
                this._inputPerson = value;
            }
        }

        public bool Isout
        {
            get
            {
                return this._isout;
            }
            set
            {
                this._isout = value;
            }
        }

        public DateTime? IsOutTime
        {
            get
            {
                return this._isOutTime;
            }
            set
            {
                this._isOutTime = value;
            }
        }

        public string Treasurycode
        {
            get
            {
                return this._treasurycode;
            }
            set
            {
                this._treasurycode = value;
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

