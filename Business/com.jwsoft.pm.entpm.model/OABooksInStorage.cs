namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OABooksInStorage
    {
        private string _author;
        private string _booktitle;
        private int _classid;
        private int _copy;
        private string _isbn;
        private string _librarycode;
        private string _publishinghouse;
        private DateTime _recorddate;
        private int _recordid;
        private string _usercode;

        public string Author
        {
            get
            {
                return this._author;
            }
            set
            {
                this._author = value;
            }
        }

        public string BookTitle
        {
            get
            {
                return this._booktitle;
            }
            set
            {
                this._booktitle = value;
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

        public int Copy
        {
            get
            {
                return this._copy;
            }
            set
            {
                this._copy = value;
            }
        }

        public string ISBN
        {
            get
            {
                return this._isbn;
            }
            set
            {
                this._isbn = value;
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

        public string PublishingHouse
        {
            get
            {
                return this._publishinghouse;
            }
            set
            {
                this._publishinghouse = value;
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

