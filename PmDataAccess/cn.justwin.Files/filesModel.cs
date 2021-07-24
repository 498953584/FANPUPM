namespace cn.justwin.Files
{
    using System;

    [Serializable]
    public class filesModel
    {
        private string _Content;
        private string _file_info;
        private int? _file_mark;
        private string _file_name;
        private string _file_sid;
        private Guid _id;
        private string _original_table;
        private string _prjcode;
        private string _sid_ColumnName;
        private int _sid_ColumnType;

        public string Content
        {
            get
            {
                return this._Content;
            }
            set
            {
                this._Content = value;
            }
        }

        public string file_info
        {
            get
            {
                return this._file_info;
            }
            set
            {
                this._file_info = value;
            }
        }

        public int? file_mark
        {
            get
            {
                return this._file_mark;
            }
            set
            {
                this._file_mark = value;
            }
        }

        public string file_name
        {
            get
            {
                return this._file_name;
            }
            set
            {
                this._file_name = value;
            }
        }

        public string file_sid
        {
            get
            {
                return this._file_sid;
            }
            set
            {
                this._file_sid = value;
            }
        }

        public Guid ID
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

        public string Original_table
        {
            get
            {
                return this._original_table;
            }
            set
            {
                this._original_table = value;
            }
        }

        public string Prjcode
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

        public string Sid_ColumnName
        {
            get
            {
                return this._sid_ColumnName;
            }
            set
            {
                this._sid_ColumnName = value;
            }
        }

        public int Sid_ColumnType
        {
            get
            {
                return this._sid_ColumnType;
            }
            set
            {
                this._sid_ColumnType = value;
            }
        }
    }
}

