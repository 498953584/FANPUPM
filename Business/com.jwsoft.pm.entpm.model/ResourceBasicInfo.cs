namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class ResourceBasicInfo : IResource, IUnitType, IResourcePrice, IPriceItem
    {
        private string _CategoryCode = "";
        private string _CategoryName = "";
        private decimal _Price;
        private int _PriceItemID;
        private string _PriceItemName = "";
        private string _ResourceCode = "";
        private string _ResourceName = "";
        private string _ResourceStyle = "2";
        private string _Specification = "";
        private int _UnitID;
        private string _UnitName = "";

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

        public string ResourceStyle
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

        public int UnitID
        {
            get
            {
                return this._UnitID;
            }
            set
            {
                this._UnitID = value;
            }
        }

        public string UnitName
        {
            get
            {
                return this._UnitName;
            }
            set
            {
                this._UnitName = value;
            }
        }
    }
}

