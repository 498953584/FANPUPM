namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class AnnexBaseInfo
    {
        private DateTime _AddDate = DateTime.Today;
        private Guid _AnnexCode;
        private string _AnnexName = "";
        private string _FileCode = "";
        private string _FilePath = "";
        private int _FileSize;
        private string _HumanCode = "";
        private string _OriginalName = "";
        private string _Remark = "";
        private int _State;

        public DateTime AddDate
        {
            get
            {
                return this._AddDate;
            }
            set
            {
                this._AddDate = value;
            }
        }

        public Guid AnnexCode
        {
            get
            {
                return this._AnnexCode;
            }
            set
            {
                this._AnnexCode = value;
            }
        }

        public string AnnexName
        {
            get
            {
                return this._AnnexName;
            }
            set
            {
                this._AnnexName = value;
            }
        }

        public string FileCode
        {
            get
            {
                return this._FileCode;
            }
            set
            {
                this._FileCode = value;
            }
        }

        public string FilePath
        {
            get
            {
                return this._FilePath;
            }
            set
            {
                this._FilePath = value;
            }
        }

        public int FileSize
        {
            get
            {
                return this._FileSize;
            }
            set
            {
                this._FileSize = value;
            }
        }

        public string HumanCode
        {
            get
            {
                return this._HumanCode;
            }
            set
            {
                this._HumanCode = value;
            }
        }

        public string OriginalName
        {
            get
            {
                return this._OriginalName;
            }
            set
            {
                this._OriginalName = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        public int State
        {
            get
            {
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }
    }
}

