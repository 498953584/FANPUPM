namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OABooksApply
    {
        private Guid _applyid;
        private string _author;
        private string _booktitle;
        private int _classid;
        private int _copy;
        private string _isbn;
        private string _isexist;
        private string _isinstorage;
        private decimal _price;
        private string _publishinghouse;

        public Guid ApplyID
        {
            get
            {
                return this._applyid;
            }
            set
            {
                this._applyid = value;
            }
        }

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

        public string IsExist
        {
            get
            {
                return this._isexist;
            }
            set
            {
                this._isexist = value;
            }
        }

        public string IsInStorage
        {
            get
            {
                return this._isinstorage;
            }
            set
            {
                this._isinstorage = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this._price;
            }
            set
            {
                this._price = value;
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
    }
}

