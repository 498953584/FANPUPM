namespace cn.justwin.salaryModel
{
    using System;

    [Serializable]
    public class FormulaModel
    {
        private DateTime _adddate;
        private string _formula;
        private string _formulaid;
        private string _formulaname;

        public DateTime AddDate
        {
            get
            {
                return this._adddate;
            }
            set
            {
                this._adddate = value;
            }
        }

        public string Formula
        {
            get
            {
                return this._formula;
            }
            set
            {
                this._formula = value;
            }
        }

        public string FormulaID
        {
            get
            {
                return this._formulaid;
            }
            set
            {
                this._formulaid = value;
            }
        }

        public string FormulaName
        {
            get
            {
                return this._formulaname;
            }
            set
            {
                this._formulaname = value;
            }
        }
    }
}

