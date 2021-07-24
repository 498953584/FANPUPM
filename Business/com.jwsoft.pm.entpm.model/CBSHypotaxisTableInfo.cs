namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class CBSHypotaxisTableInfo
    {
        private decimal _BudgetMoney;
        private string _BudgetMonth = string.Empty;
        private string _BudgetYear = string.Empty;
        private string _NodeCode = string.Empty;
        private string _NodeName = string.Empty;
        private Guid _ProjectCode = Guid.Empty;

        public decimal BudgetMoney
        {
            get
            {
                return this._BudgetMoney;
            }
            set
            {
                this._BudgetMoney = value;
            }
        }

        public string BudgetMonth
        {
            get
            {
                return this._BudgetMonth;
            }
            set
            {
                this._BudgetMonth = value;
            }
        }

        public string BudgetYear
        {
            get
            {
                return this._BudgetYear;
            }
            set
            {
                this._BudgetYear = value;
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

        public Guid ProjectCode
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
    }
}

