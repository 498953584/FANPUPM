namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OABooksLibrary
    {
        private string _content;
        private string _isvalid;
        private string _librarycode;
        private string _libraryname;
        private string _manager;

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

        public string LibraryName
        {
            get
            {
                return this._libraryname;
            }
            set
            {
                this._libraryname = value;
            }
        }

        public string Manager
        {
            get
            {
                return this._manager;
            }
            set
            {
                this._manager = value;
            }
        }
    }
}

