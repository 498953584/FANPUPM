namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class CostCbsInfo
    {
        private string _CostType = string.Empty;
        private int _IsValid = -2147483648;
        private decimal _Money;
        private string _NodeCode = string.Empty;
        private int _NodeLayer = -2147483648;
        private string _NodeName = string.Empty;
        private string _PNode = string.Empty;
        private string _PrjCode = string.Empty;
        private string _Remark = string.Empty;
        private decimal _TargetMoney;

        public string CostType
        {
            get
            {
                return this._CostType;
            }
            set
            {
                this._CostType = value;
            }
        }

        public int IsValid
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

        public decimal Money
        {
            get
            {
                return this._Money;
            }
            set
            {
                this._Money = value;
            }
        }

        public string NodeCode
        {
            get
            {
                return this._NodeCode;
            }
            set
            {
                this._NodeCode = value;
            }
        }

        public int NodeLayer
        {
            get
            {
                return this._NodeLayer;
            }
            set
            {
                this._NodeLayer = value;
            }
        }

        public string NodeName
        {
            get
            {
                return this._NodeName;
            }
            set
            {
                this._NodeName = value;
            }
        }

        public string PNode
        {
            get
            {
                return this._PNode;
            }
            set
            {
                this._PNode = value;
            }
        }

        public string PrjCode
        {
            get
            {
                return this._PrjCode;
            }
            set
            {
                this._PrjCode = value;
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

        public decimal TargetMoney
        {
            get
            {
                return this._TargetMoney;
            }
            set
            {
                this._TargetMoney = value;
            }
        }
    }
}

