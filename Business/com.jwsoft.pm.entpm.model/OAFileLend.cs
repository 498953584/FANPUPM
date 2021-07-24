namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class OAFileLend
    {
        private string _borrowman;
        private int _filerecordid;
        private DateTime _lenddate;
        private string _lendstate;
        private OAFileCatalog _OAFileCatalog = new OAFileCatalog();
        private DateTime _planreturndate;
        private int _recordid;
        private DateTime _returnapplydate;
        private DateTime _returndate;

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

        public OAFileCatalog FileCatalog
        {
            get
            {
                return this._OAFileCatalog;
            }
            set
            {
                this._OAFileCatalog = value;
            }
        }

        public int FileRecordID
        {
            get
            {
                return this._filerecordid;
            }
            set
            {
                this._filerecordid = value;
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

