namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ResourceImportM
    {
        private string _ResourceCode;
        private string _ResourceName;
        private decimal _ResourcePrice;
        private string _ResourceRemark;
        private string _ResourceSpec;
        private string _ResourceUnit;

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

        public decimal ResourcePrice
        {
            get
            {
                return this._ResourcePrice;
            }
            set
            {
                this._ResourcePrice = value;
            }
        }

        public string ResourceRemark
        {
            get
            {
                return this._ResourceRemark;
            }
            set
            {
                this._ResourceRemark = value;
            }
        }

        public string ResourceSpec
        {
            get
            {
                return this._ResourceSpec;
            }
            set
            {
                this._ResourceSpec = value;
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
    }
}

