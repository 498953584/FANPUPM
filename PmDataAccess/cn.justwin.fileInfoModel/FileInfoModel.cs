namespace cn.justwin.fileInfoModel
{
    using System;

    [Serializable]
    public class FileInfoModel
    {
        private DateTime? _createtime;
        private string _filename;
        private string _fileowner;
        private string _filesize;
        private string _filetype;
        private string _id;
        private string _parentid;
        private string _usercodes;
        private string filenewname;

        public DateTime? CreateTime
        {
            get
            {
                return this._createtime;
            }
            set
            {
                this._createtime = value;
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

        public string FileNewName
        {
            get
            {
                return this.filenewname;
            }
            set
            {
                this.filenewname = value;
            }
        }

        public string FileOwner
        {
            get
            {
                return this._fileowner;
            }
            set
            {
                this._fileowner = value;
            }
        }

        public string FileSize
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

        public string Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string ParentId
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
            }
        }

        public string UserCodes
        {
            get
            {
                return this._usercodes;
            }
            set
            {
                this._usercodes = value;
            }
        }
    }
}

