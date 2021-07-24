namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OAVotingInfo
    {
        private string _content;
        private string _isvalid;
        private string _range;
        private DateTime _recorddate;
        private int _recordid;
        private string _State;
        private string _title;
        private string _usercode;
        private string _votetype;

        public string Content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
            }
        }

        public string IsValid
        {
            get
            {
                return this._isvalid;
            }
            set
            {
                this._isvalid = value;
            }
        }

        public string Range
        {
            get
            {
                return this._range;
            }
            set
            {
                this._range = value;
            }
        }

        public DateTime RecordDate
        {
            get
            {
                return this._recorddate;
            }
            set
            {
                this._recorddate = value;
            }
        }

        public int RecordID
        {
            get
            {
                return this._recordid;
            }
            set
            {
                this._recordid = value;
            }
        }

        public string State
        {
            get
            {
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string Title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        public string UserCode
        {
            get
            {
                return this._usercode;
            }
            set
            {
                this._usercode = value;
            }
        }

        public string VoteType
        {
            get
            {
                return this._votetype;
            }
            set
            {
                this._votetype = value;
            }
        }
    }
}

