namespace cn.justwin.stockModel
{
    using System;

    public class ResourceGaugeModel
    {
        private string _isDefault;
        private string _isValid;
        private string _noteUniqueID;
        private string _owner;
        private string _quotiety;
        private string _resourceCode;
        private string _unitID;
        private string _versionCode;
        private string _versionTime;

        public string IsDefault
        {
            get
            {
                return this._isDefault;
            }
            set
            {
                this._isDefault = value;
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

        public string NoteUniqueId
        {
            get
            {
                return this._noteUniqueID;
            }
            set
            {
                this._noteUniqueID = value;
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

        public string Quantity
        {
            get
            {
                return this._quotiety;
            }
            set
            {
                this._quotiety = value;
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

        public string UnitId
        {
            get
            {
                return this._unitID;
            }
            set
            {
                this._unitID = value;
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

