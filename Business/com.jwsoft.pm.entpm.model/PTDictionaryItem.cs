namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class PTDictionaryItem
    {
        private int _classid;
        private string _displayvalue;
        private string _isvalid;
        private int _keyvalue;

        public int ClassID
        {
            get
            {
                return this._classid;
            }
            set
            {
                this._classid = value;
            }
        }

        public string DisplayValue
        {
            get
            {
                return this._displayvalue;
            }
            set
            {
                this._displayvalue = value;
            }
        }

        public string IsValid
        {
            get
            {
                return this._isvalid;
            }
            set
            {
                this._isvalid = value;
            }
        }

        public int KeyValue
        {
            get
            {
                return this._keyvalue;
            }
            set
            {
                this._keyvalue = value;
            }
        }
    }
}

