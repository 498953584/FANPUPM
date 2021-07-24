namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class QualityFileInfo
    {
        private string _bz;
        private int _Id;
        private int _modelkey;
        private string _prjcode;
        private string _title;
        private DateTime _uploadDate;
        private string _urlpath;

        public string bz
        {
            get
            {
                return this._bz;
            }
            set
            {
                this._bz = value;
            }
        }

        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

        public int modelkey
        {
            get
            {
                return this._modelkey;
            }
            set
            {
                this._modelkey = value;
            }
        }

        public string prjcode
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

        public string title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        public DateTime uploadDate
        {
            get
            {
                return this._uploadDate;
            }
            set
            {
                this._uploadDate = value;
            }
        }

        public string urlpath
        {
            get
            {
                return this._urlpath;
            }
            set
            {
                this._urlpath = value;
            }
        }
    }
}

