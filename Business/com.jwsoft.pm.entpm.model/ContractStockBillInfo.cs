namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ContractStockBillInfo
    {
        private string _ContractCode = "";
        private string _ContractType = "";
        private int _ID;
        private DateTime _LimitTime = DateTime.MinValue;
        private string _logo = "";
        private decimal _Num;
        private decimal _Price;
        private string _producer = "";
        private string _ProjectCode;
        private string _Remark = "";
        private string _ResourceCode = "";
        private string _ResourceName = "";
        private string _Specification = "";
        private string _Unit = "";

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

        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        public DateTime LimitTime
        {
            get
            {
                return this._LimitTime;
            }
            set
            {
                this._LimitTime = value;
            }
        }

        public string logo
        {
            get
            {
                return this._logo;
            }
            set
            {
                this._logo = value;
            }
        }

        public decimal Num
        {
            get
            {
                return this._Num;
            }
            set
            {
                this._Num = value;
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

        public string producer
        {
            get
            {
                return this._producer;
            }
            set
            {
                this._producer = value;
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

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
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

        public string Unit
        {
            get
            {
                return this._Unit;
            }
            set
            {
                this._Unit = value;
            }
        }
    }
}

