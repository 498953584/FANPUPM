namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class inputReceiptItem
    {
        private int _ChildMainID;
        private string _DepartCode;
        private string _DepartmentName;
        private decimal _DevotionMoney;
        private decimal _DevotionOutput;
        private decimal _IncomeMoney;
        private decimal _IncomeOutput;
        private int _MainId;

        public int ChildMainID
        {
            get
            {
                return this._ChildMainID;
            }
            set
            {
                this._ChildMainID = value;
            }
        }

        public string DepartCode
        {
            get
            {
                return this._DepartCode;
            }
            set
            {
                this._DepartCode = value;
            }
        }

        public string DepartmentName
        {
            get
            {
                return this._DepartmentName;
            }
            set
            {
                this._DepartmentName = value;
            }
        }

        public decimal DevotionCompleteRate
        {
            get
            {
                if (this._DevotionMoney > 0M)
                {
                    return (decimal.Round(this._DevotionOutput / this._DevotionMoney, 4) * 100M);
                }
                return 0M;
            }
        }

        public decimal DevotionMoney
        {
            get
            {
                return this._DevotionMoney;
            }
            set
            {
                this._DevotionMoney = value;
            }
        }

        public decimal DevotionOutput
        {
            get
            {
                return this._DevotionOutput;
            }
            set
            {
                this._DevotionOutput = value;
            }
        }

        public decimal IncomeCompleteRate
        {
            get
            {
                if (this._IncomeMoney > 0M)
                {
                    return (decimal.Round(this._IncomeOutput / this._IncomeMoney, 4) * 100M);
                }
                return 0M;
            }
        }

        public decimal IncomeMoney
        {
            get
            {
                return this._IncomeMoney;
            }
            set
            {
                this._IncomeMoney = value;
            }
        }

        public decimal IncomeOutput
        {
            get
            {
                return this._IncomeOutput;
            }
            set
            {
                this._IncomeOutput = value;
            }
        }

        public int MainId
        {
            get
            {
                return this._MainId;
            }
            set
            {
                this._MainId = value;
            }
        }
    }
}

