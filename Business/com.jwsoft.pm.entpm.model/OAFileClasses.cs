namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OAFileClasses
    {
        private string _classcode;
        private int _classid;
        private string _classname;
        private string _isvalid;
        private string _librarycode;
        private string _remark;

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

        public int ClassID
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

        public string LibraryCode
        {
            get
            {
                return this._librarycode;
            }
            set
            {
                this._librarycode = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }
    }
}

