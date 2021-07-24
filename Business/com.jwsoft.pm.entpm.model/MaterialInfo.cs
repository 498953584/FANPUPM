namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class MaterialInfo
    {
        private decimal _Amount;
        private string _ItemCode = "";
        private string _PlanCode = "";
        private decimal _Price;
        private string _ResourceCode = "";
        private string _ResourceName = "";
        private string _Standard = "";
        private string _UnitName = "";

        public decimal Amount
        {
            get
            {
                return this._Amount;
            }
            set
            {
                this._Amount = value;
            }
        }

        public string ItemCode
        {
            get
            {
                return this._ItemCode;
            }
            set
            {
                this._ItemCode = value;
            }
        }

        public string PlanCode
        {
            get
            {
                return this._PlanCode;
            }
            set
            {
                this._PlanCode = value;
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

        public string Standard
        {
            get
            {
                return this._Standard;
            }
            set
            {
                this._Standard = value;
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

