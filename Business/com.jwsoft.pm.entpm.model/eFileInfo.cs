namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class eFileInfo
    {
        private int _classid;
        private string _corpcode;
        private string _filecode;
        private string _filecopy;
        private string _filepath;
        private string _filetitle;
        private string _filetype;
        private string _originalname;
        private Guid _prjguid;
        private DateTime _recorddate;
        private int _recordid;
        private string _recordtype;
        private string _remark;
        private string _savelimit;
        private string _secretlevel;
        private DateTime _submitdate;
        private string _submitman;
        private string _usercode;

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

        public string CorpCode
        {
            get
            {
                return this._corpcode;
            }
            set
            {
                this._corpcode = value;
            }
        }

        public string FileCode
        {
            get
            {
                return this._filecode;
            }
            set
            {
                this._filecode = value;
            }
        }

        public string FileCopy
        {
            get
            {
                return this._filecopy;
            }
            set
            {
                this._filecopy = value;
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

        public string FileTitle
        {
            get
            {
                return this._filetitle;
            }
            set
            {
                this._filetitle = value;
            }
        }

        public string FileType
        {
            get
            {
                return this._filetype;
            }
            set
            {
                this._filetype = value;
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

        public Guid PrjGuid
        {
            get
            {
                return this._prjguid;
            }
            set
            {
                this._prjguid = value;
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

        public string RecordType
        {
            get
            {
                return this._recordtype;
            }
            set
            {
                this._recordtype = value;
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

        public string SaveLimit
        {
            get
            {
                return this._savelimit;
            }
            set
            {
                this._savelimit = value;
            }
        }

        public string SecretLevel
        {
            get
            {
                return this._secretlevel;
            }
            set
            {
                this._secretlevel = value;
            }
        }

        public DateTime SubmitDate
        {
            get
            {
                return this._submitdate;
            }
            set
            {
                this._submitdate = value;
            }
        }

        public string SubmitMan
        {
            get
            {
                return this._submitman;
            }
            set
            {
                this._submitman = value;
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

