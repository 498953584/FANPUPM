namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class IntendancePhotoList
    {
        private Guid _InfoGuid;
        private Guid _NoteId;
        private string _PhotoExplain;
        private string _PhotoName;
        private string _PhotoNumber;
        private string _PhotoPath;
        private int _PhotoType;
        private string _ThumbnaImgPath;
        private string _ThumbnaName;
        private string _UserCode;

        public Guid InfoGuid
        {
            get
            {
                return this._InfoGuid;
            }
            set
            {
                this._InfoGuid = value;
            }
        }

        public Guid NoteId
        {
            get
            {
                return this._NoteId;
            }
            set
            {
                this._NoteId = value;
            }
        }

        public string PhotoExplain
        {
            get
            {
                return this._PhotoExplain;
            }
            set
            {
                this._PhotoExplain = value;
            }
        }

        public string PhotoName
        {
            get
            {
                return this._PhotoName;
            }
            set
            {
                this._PhotoName = value;
            }
        }

        public string PhotoNumber
        {
            get
            {
                return this._PhotoNumber;
            }
            set
            {
                this._PhotoNumber = value;
            }
        }

        public string PhotoPath
        {
            get
            {
                return this._PhotoPath;
            }
            set
            {
                this._PhotoPath = value;
            }
        }

        public int PhotoType
        {
            get
            {
                return this._PhotoType;
            }
            set
            {
                this._PhotoType = value;
            }
        }

        public string ThumbnaImgPath
        {
            get
            {
                return this._ThumbnaImgPath;
            }
            set
            {
                this._ThumbnaImgPath = value;
            }
        }

        public string ThumbnaName
        {
            get
            {
                return this._ThumbnaName;
            }
            set
            {
                this._ThumbnaName = value;
            }
        }

        public string UserCode
        {
            get
            {
                return this._UserCode;
            }
            set
            {
                this._UserCode = value;
            }
        }
    }
}

