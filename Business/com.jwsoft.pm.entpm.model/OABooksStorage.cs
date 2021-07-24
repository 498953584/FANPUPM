namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class OABooksStorage
    {
        private string _author;
        private string _booktitle;
        private int _classid;
        private string _isbn;
        private int _leavecopy;
        private string _publishinghouse;
        private int _sumcopy;

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

        public int LeaveCopy
        {
            get
            {
                return this._leavecopy;
            }
            set
            {
                this._leavecopy = value;
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

        public int SumCopy
        {
            get
            {
                return this._sumcopy;
            }
            set
            {
                this._sumcopy = value;
            }
        }
    }
}

