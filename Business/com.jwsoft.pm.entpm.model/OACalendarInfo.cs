namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OACalendarInfo
    {
        private string _content;
        private Guid _infoguid;
        private string _isremind;
        private DateTime _recorddate;
        private int _recordid;
        private string _title;
        private string _usercode;

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

        public Guid InfoGuid
        {
            get
            {
                return this._infoguid;
            }
            set
            {
                this._infoguid = value;
            }
        }

        public string IsRemind
        {
            get
            {
                return this._isremind;
            }
            set
            {
                this._isremind = value;
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
    }
}

