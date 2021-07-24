namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class PriceItem : IPriceItem
    {
        private bool _IsDefault;
        private bool _IsFixed;
        private bool _IsReadonly;
        private bool _IsValid = true;
        private int _PriceItemID;
        private string _PriceItemName = "";

        public bool IsDefault
        {
            get
            {
                return this._IsDefault;
            }
            set
            {
                this._IsDefault = value;
            }
        }

        public bool IsFixed
        {
            get
            {
                return this._IsFixed;
            }
            set
            {
                this._IsFixed = value;
            }
        }

        public bool IsReadonly
        {
            get
            {
                return this._IsReadonly;
            }
            set
            {
                this._IsReadonly = value;
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

        public int PriceItemID
        {
            get
            {
                return this._PriceItemID;
            }
            set
            {
                this._PriceItemID = value;
            }
        }

        public string PriceItemName
        {
            get
            {
                return this._PriceItemName;
            }
            set
            {
                this._PriceItemName = value;
            }
        }
    }
}

