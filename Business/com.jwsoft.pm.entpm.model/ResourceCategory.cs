namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ResourceCategory
    {
        private string _CategoryCode = "";
        private string _CategoryName = "";
        private string _CategoryParentCode = "";
        private int _ChildNumber;
        private bool _IsValid = true;
        private com.jwsoft.pm.entpm.model.ResourceStyle _ResourceStyle = com.jwsoft.pm.entpm.model.ResourceStyle.Material;
        private int _ResourceType;
        private Guid _VersionCode = Guid.Empty;

        public string CategoryCode
        {
            get
            {
                return this._CategoryCode;
            }
            set
            {
                this._CategoryCode = value;
            }
        }

        public string CategoryName
        {
            get
            {
                return this._CategoryName;
            }
            set
            {
                this._CategoryName = value;
            }
        }

        public string CategoryParentCode
        {
            get
            {
                return this._CategoryParentCode;
            }
            set
            {
                this._CategoryParentCode = value;
            }
        }

        public int ChildNumber
        {
            get
            {
                return this._ChildNumber;
            }
            set
            {
                this._ChildNumber = value;
            }
        }

        public bool IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                this._IsValid = value;
            }
        }

        public com.jwsoft.pm.entpm.model.ResourceStyle ResourceStyle
        {
            get
            {
                return this._ResourceStyle;
            }
            set
            {
                this._ResourceStyle = value;
            }
        }

        public int ResourceType
        {
            get
            {
                return this._ResourceType;
            }
            set
            {
                this._ResourceType = value;
            }
        }

        public Guid VersionCode
        {
            get
            {
                return this._VersionCode;
            }
            set
            {
                this._VersionCode = value;
            }
        }
    }
}

