namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class KnowledgeFileModel
    {
        private DateTime _AddDate = DateTime.Now;
        private Guid _AnnexCode = Guid.Empty;
        private string _AnnexName = "";
        private int _AnnexType;
        private string _FileCode = "";
        private string _FilePath = "";
        private int _FileSize;
        private string _HumanCode = "";
        private int _ModuleID;
        private string _OriginalName = "";
        private Guid _ProjectCode = Guid.Empty;
        private Guid _RecordCode = Guid.Empty;
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

        public int AnnexType
        {
            get
            {
                return this._AnnexType;
            }
            set
            {
                this._AnnexType = value;
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

        public int ModuleID
        {
            get
            {
                return this._ModuleID;
            }
            set
            {
                this._ModuleID = value;
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

        public Guid ProjectCode
        {
            get
            {
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }

        public Guid RecordCode
        {
            get
            {
                return this._RecordCode;
            }
            set
            {
                this._RecordCode = value;
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

