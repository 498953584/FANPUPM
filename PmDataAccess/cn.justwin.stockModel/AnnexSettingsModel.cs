namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class AnnexSettingsModel
    {
        private string _extname;
        private int _filenumber;
        private string _filepath;
        private int _filesize;
        private string _modulecode;
        private int _moduleid;
        private string _modulename;
        private string _remark;

        public string ExtName
        {
            get
            {
                return this._extname;
            }
            set
            {
                this._extname = value;
            }
        }

        public int FileNumber
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

        public int FileSize
        {
            get
            {
                return this._filesize;
            }
            set
            {
                this._filesize = value;
            }
        }

        public string ModuleCode
        {
            get
            {
                return this._modulecode;
            }
            set
            {
                this._modulecode = value;
            }
        }

        public int ModuleID
        {
            get
            {
                return this._moduleid;
            }
            set
            {
                this._moduleid = value;
            }
        }

        public string ModuleName
        {
            get
            {
                return this._modulename;
            }
            set
            {
                this._modulename = value;
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

