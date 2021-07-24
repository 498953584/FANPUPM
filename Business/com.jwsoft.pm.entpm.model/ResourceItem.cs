namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ResourceItem : IResource
    {
        private string _CategoryCode = "";
        private bool _IsValid = true;
        private string _ResourceCode = "";
        private string _ResourceName = "";
        private int _ResourceType;
        private ResourceStyle _RStyle;
        private string _Specification = "";
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

        public string ResourceCode
        {
            get
            {
                return this._ResourceCode;
            }
            set
            {
                this._ResourceCode = value;
            }
        }

        public string ResourceName
        {
            get
            {
                return this._ResourceName;
            }
            set
            {
                this._ResourceName = value;
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

        public ResourceStyle RStyle
        {
            get
            {
                return this._RStyle;
            }
            set
            {
                this._RStyle = value;
            }
        }

        public string Specification
        {
            get
            {
                return this._Specification;
            }
            set
            {
                this._Specification = value;
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

