namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ProviderClassModel
    {
        private string _classcode;
        private int _classid;
        private string _classname;
        private string _comment;
        private int _parentid;
        private DateTime _recorddate;
        private int _state;
        private string _usercode;
        private string _wduuser;

        public string ClassCode
        {
            get
            {
                return this._classcode;
            }
            set
            {
                this._classcode = value;
            }
        }

        public int ClassId
        {
            get
            {
                return this._classid;
            }
            set
            {
                this._classid = value;
            }
        }

        public string ClassName
        {
            get
            {
                return this._classname;
            }
            set
            {
                this._classname = value;
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

        public int ParentId
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
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

        public string WDUUser
        {
            get
            {
                return this._wduuser;
            }
            set
            {
                this._wduuser = value;
            }
        }
    }
}

