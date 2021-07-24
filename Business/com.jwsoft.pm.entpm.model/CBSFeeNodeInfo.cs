namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class CBSFeeNodeInfo : BaseInfo
    {
        private string _CostType = string.Empty;
        private bool _IsHaveChild;
        private int _IsValid = -2147483648;
        private decimal _Money;
        private string _NodeCode = string.Empty;
        private int _NodeLayer = -2147483648;
        private string _NodeName = string.Empty;
        private decimal _PlanMoney;
        private string _PNode = string.Empty;
        private string _Remark = string.Empty;
        private DateTime _StartDate = DateTime.Now;
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

        public bool IsHaveChild
        {
            get
            {
                return this._IsHaveChild;
            }
            set
            {
                this._IsHaveChild = value;
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

        public decimal PlanMoney
        {
            get
            {
                return this._PlanMoney;
            }
            set
            {
                this._PlanMoney = value;
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

        public DateTime StartDate
        {
            get
            {
                return this._StartDate;
            }
            set
            {
                this._StartDate = value;
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

