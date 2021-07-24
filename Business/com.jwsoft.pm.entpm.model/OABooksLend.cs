namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class OABooksLend
    {
        private OABooksStorage _BooksStorage;
        private string _booktitle;
        private string _borrowman;
        private int _copy;
        private string _isbn;
        private DateTime _lenddate;
        private string _lendstate;
        private DateTime _planreturndate;
        private int _recordid;
        private DateTime _returnapplydate;
        private DateTime _returndate;

        public OABooksStorage BooksStorage
        {
            get
            {
                return this._BooksStorage;
            }
            set
            {
                this._BooksStorage = value;
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

        public string BorrowMan
        {
            get
            {
                return this._borrowman;
            }
            set
            {
                this._borrowman = value;
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

        public DateTime LendDate
        {
            get
            {
                return this._lenddate;
            }
            set
            {
                this._lenddate = value;
            }
        }

        public string LendState
        {
            get
            {
                return this._lendstate;
            }
            set
            {
                this._lendstate = value;
            }
        }

        public DateTime PlanReturnDate
        {
            get
            {
                return this._planreturndate;
            }
            set
            {
                this._planreturndate = value;
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

        public DateTime ReturnApplyDate
        {
            get
            {
                return this._returnapplydate;
            }
            set
            {
                this._returnapplydate = value;
            }
        }

        public DateTime ReturnDate
        {
            get
            {
                return this._returndate;
            }
            set
            {
                this._returndate = value;
            }
        }
    }
}

