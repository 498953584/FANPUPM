namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class ResourceInfo
    {
        private decimal _BudgetPrice;
        private string _ResourceName = "";
        private string _ResourceNo = "";
        private ResourceTypeStyle _ResourceStyle;
        private string _ResourceType = "";
        private string _ResourceUnit = "";
        private string _Specification = "";

        public decimal BudgetPrice
        {
            get
            {
                return this._BudgetPrice;
            }
            set
            {
                this._BudgetPrice = value;
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

        public string ResourceNo
        {
            get
            {
                return this._ResourceNo;
            }
            set
            {
                this._ResourceNo = value;
            }
        }

        public ResourceTypeStyle ResourceStyle
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

        public string ResourceType
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

        public string ResourceUnit
        {
            get
            {
                return this._ResourceUnit;
            }
            set
            {
                this._ResourceUnit = value;
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
    }
}

