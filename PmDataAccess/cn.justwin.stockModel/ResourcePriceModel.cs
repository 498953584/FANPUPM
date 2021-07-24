namespace cn.justwin.stockModel
{
    using System;

    public class ResourcePriceModel
    {
        private string _noteUniqueID;
        private string _owner;
        private string _price;
        private string _priceItemId;
        private string _resourceCode;
        private string _versionCode;
        private string _versionTime;

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

        public string Price
        {
            get
            {
                return this._price;
            }
            set
            {
                this._price = value;
            }
        }

        public string PriceItem
        {
            get
            {
                return this._priceItemId;
            }
            set
            {
                this._priceItemId = value;
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

