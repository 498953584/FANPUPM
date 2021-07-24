namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ContractTaskInfo
    {
        private string _ContractCode;
        private decimal _ContractPrice;
        private string _ContractType;
        private string _ProjectCode;
        private decimal _Quantity;
        private string _QuantityUnit;
        private decimal _subtotal;
        private string _TaskCode;
        private string _TaskName;

        public string ContractCode
        {
            get
            {
                return this._ContractCode;
            }
            set
            {
                this._ContractCode = value;
            }
        }

        public decimal ContractPrice
        {
            get
            {
                return this._ContractPrice;
            }
            set
            {
                this._ContractPrice = value;
            }
        }

        public string ContractType
        {
            get
            {
                return this._ContractType;
            }
            set
            {
                this._ContractType = value;
            }
        }

        public string ProjectCode
        {
            get
            {
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }

        public decimal Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                this._Quantity = value;
            }
        }

        public string QuantityUnit
        {
            get
            {
                return this._QuantityUnit;
            }
            set
            {
                this._QuantityUnit = value;
            }
        }

        public decimal subtotal
        {
            get
            {
                return this._subtotal;
            }
            set
            {
                this._subtotal = value;
            }
        }

        public string TaskCode
        {
            get
            {
                return this._TaskCode;
            }
            set
            {
                this._TaskCode = value;
            }
        }

        public string TaskName
        {
            get
            {
                return this._TaskName;
            }
            set
            {
                this._TaskName = value;
            }
        }
    }
}

