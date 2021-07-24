namespace cn.justwin.stockModel
{
    using System;

    public class ResourceModel
    {
        private string _categoryCode;
        private string _imageUrl = "";
        private string _isValid;
        private string _owner;
        private string _resourceCode;
        private string _resourceName;
        private string _resourceRemark;
        private string _resourceStyle;
        private string _resourceType;
        private string _specification;
        private string _versionCode;
        private string _versionTime;

        public string CategoryCode
        {
            get
            {
                return this._categoryCode;
            }
            set
            {
                this._categoryCode = value;
            }
        }

        public string IsValid
        {
            get
            {
                return this._isValid;
            }
            set
            {
                this._isValid = value;
            }
        }

        public string Owner
        {
            get
            {
                return this._owner;
            }
            set
            {
                this._owner = value;
            }
        }

        public string ResourceCode
        {
            get
            {
                return this._resourceCode;
            }
            set
            {
                this._resourceCode = value;
            }
        }

        public string ResourceImageUrl
        {
            get
            {
                return this._imageUrl;
            }
            set
            {
                this._imageUrl = value;
            }
        }

        public string ResourceName
        {
            get
            {
                return this._resourceName;
            }
            set
            {
                this._resourceName = value;
            }
        }

        public string ResourceRemark
        {
            get
            {
                return this._resourceRemark;
            }
            set
            {
                this._resourceRemark = value;
            }
        }

        public string ResourceStyle
        {
            get
            {
                return this._resourceStyle;
            }
            set
            {
                this._resourceStyle = value;
            }
        }

        public string ResourceType
        {
            get
            {
                return this._resourceType;
            }
            set
            {
                this._resourceType = value;
            }
        }

        public string Specification
        {
            get
            {
                return this._specification;
            }
            set
            {
                this._specification = value;
            }
        }

        public string VersionCode
        {
            get
            {
                return this._versionCode;
            }
            set
            {
                this._versionCode = value;
            }
        }

        public string VersionTime
        {
            get
            {
                return this._versionTime;
            }
            set
            {
                this._versionTime = value;
            }
        }
    }
}

