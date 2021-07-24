namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class OAFileCatalog
    {
        private string _boxnumber;
        private int _classid;
        private string _content;
        private int _fileage;
        private string _filecode;
        private string _filename;
        private string _filenumber;
        private string _filetype;
        private string _isvalid;
        private string _lendstate;
        private string _librarycode;
        private int _pagenumber;
        private DateTime _pigeonholedate;
        private string _principal;
        private Guid _prjcode;
        private string _prjname;
        private DateTime _recorddate;
        private int _recordid;
        private string _saveplace;
        private int _secretlevel;
        private int _timelimit;
        private string _usercode;
        private string _volume;

        public string BoxNumber
        {
            get
            {
                return this._boxnumber;
            }
            set
            {
                this._boxnumber = value;
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

        public int FileAge
        {
            get
            {
                return this._fileage;
            }
            set
            {
                this._fileage = value;
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

        public string FileName
        {
            get
            {
                return this._filename;
            }
            set
            {
                this._filename = value;
            }
        }

        public string FileNumber
        {
            get
            {
                return this._filenumber;
            }
            set
            {
                this._filenumber = value;
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

        public int PageNumber
        {
            get
            {
                return this._pagenumber;
            }
            set
            {
                this._pagenumber = value;
            }
        }

        public DateTime PigeonholeDate
        {
            get
            {
                return this._pigeonholedate;
            }
            set
            {
                this._pigeonholedate = value;
            }
        }

        public string Principal
        {
            get
            {
                return this._principal;
            }
            set
            {
                this._principal = value;
            }
        }

        public Guid PrjCode
        {
            get
            {
                return this._prjcode;
            }
            set
            {
                this._prjcode = value;
            }
        }

        public string PrjName
        {
            get
            {
                return this._prjname;
            }
            set
            {
                this._prjname = value;
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

        public string SavePlace
        {
            get
            {
                return this._saveplace;
            }
            set
            {
                this._saveplace = value;
            }
        }

        public int SecretLevel
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

        public int TimeLimit
        {
            get
            {
                return this._timelimit;
            }
            set
            {
                this._timelimit = value;
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

        public string Volume
        {
            get
            {
                return this._volume;
            }
            set
            {
                this._volume = value;
            }
        }
    }
}

