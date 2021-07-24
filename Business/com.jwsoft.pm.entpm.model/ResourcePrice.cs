namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class ResourcePrice : IResourcePrice, IPriceItem
    {
        private bool _IsReadonly;
        private decimal _Price;
        private int _PriceItemID;
        private string _PriceItemName = "";
        private string _ResourceCode = "";

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

        public decimal Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                this._Price = value;
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
    }
}

