namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class AnnexModuleSettingsInfo
    {
        private string _ExtName;
        private int _FileNumber;
        private string _FilePath;
        private int _FileSize;
        private string _ModuleCode;
        private int _ModuleID;
        private string _ModuleName;
        private string _Remark;

        public string ExtName
        {
            get
            {
                return this._ExtName;
            }
            set
            {
                this._ExtName = value;
            }
        }

        public int FileNumber
        {
            get
            {
                return this._FileNumber;
            }
            set
            {
                this._FileNumber = value;
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

        public string ModuleCode
        {
            get
            {
                return this._ModuleCode;
            }
            set
            {
                this._ModuleCode = value;
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

        public string ModuleName
        {
            get
            {
                return this._ModuleName;
            }
            set
            {
                this._ModuleName = value;
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
    }
}

