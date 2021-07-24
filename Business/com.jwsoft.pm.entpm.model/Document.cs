namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class Document
    {
        private string _classid;
        private int _clickedcount;
        private string _content;
        private Guid _documentnique;
        private string _documenttag;
        private string _filepath;
        private bool _iselite;
        private bool _istop;
        private string _originalname;
        private DateTime _pushdate;
        private string _pushperson;
        private string _title;

        public string ClassID
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

        public int ClickedCount
        {
            get
            {
                return this._clickedcount;
            }
            set
            {
                this._clickedcount = value;
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

        public Guid DocumentNique
        {
            get
            {
                return this._documentnique;
            }
            set
            {
                this._documentnique = value;
            }
        }

        public string DocumentTag
        {
            get
            {
                return this._documenttag;
            }
            set
            {
                this._documenttag = value;
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

        public bool isElite
        {
            get
            {
                return this._iselite;
            }
            set
            {
                this._iselite = value;
            }
        }

        public bool isTop
        {
            get
            {
                return this._istop;
            }
            set
            {
                this._istop = value;
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

        public DateTime PushDate
        {
            get
            {
                return this._pushdate;
            }
            set
            {
                this._pushdate = value;
            }
        }

        public string PushPerson
        {
            get
            {
                return this._pushperson;
            }
            set
            {
                this._pushperson = value;
            }
        }

        public string Title
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
    }
}

