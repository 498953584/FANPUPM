namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class PrvdAccessoryModel
    {
        private int _accessoryid;
        private string _accessoryname;
        private string _comment;
        private string _filepath;
        private string _originalname;
        private int _providerid;
        private DateTime _recorddate;
        private string _usercode;

        public int AccessoryId
        {
            get
            {
                return this._accessoryid;
            }
            set
            {
                this._accessoryid = value;
            }
        }

        public string AccessoryName
        {
            get
            {
                return this._accessoryname;
            }
            set
            {
                this._accessoryname = value;
            }
        }

        public string Comment
        {
            get
            {
                return this._comment;
            }
            set
            {
                this._comment = value;
            }
        }

        public string FilePath
        {
            get
            {
                return this._filepath;
            }
            set
            {
                this._filepath = value;
            }
        }

        public string OriginalName
        {
            get
            {
                return this._originalname;
            }
            set
            {
                this._originalname = value;
            }
        }

        public int ProviderId
        {
            get
            {
                return this._providerid;
            }
            set
            {
                this._providerid = value;
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

