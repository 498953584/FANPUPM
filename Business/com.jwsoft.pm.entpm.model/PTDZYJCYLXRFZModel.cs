namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class PTDZYJCYLXRFZModel
    {
        private string _groupname;
        private string _isvalid;
        private int _recordid;
        private string _usercode;

        public string GroupName
        {
            get
            {
                return this._groupname;
            }
            set
            {
                this._groupname = value;
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

