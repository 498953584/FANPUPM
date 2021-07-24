namespace cn.justwin.salaryModel
{
    using System;

    public class TaxStartPointModel
    {
        private DateTime? _adddate;
        private decimal? _deduct;
        private decimal _lowerlimit;
        private decimal _taxrate;
        private string _taxrateid;
        private decimal _taxstartpoint;
        private decimal _upperlimit;

        public DateTime? AddDate
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

        public decimal? Deduct
        {
            get
            {
                return this._deduct;
            }
            set
            {
                this._deduct = value;
            }
        }

        public decimal LowerLimit
        {
            get
            {
                return this._lowerlimit;
            }
            set
            {
                this._lowerlimit = value;
            }
        }

        public decimal TaxRate
        {
            get
            {
                return this._taxrate;
            }
            set
            {
                this._taxrate = value;
            }
        }

        public string TaxRateID
        {
            get
            {
                return this._taxrateid;
            }
            set
            {
                this._taxrateid = value;
            }
        }

        public decimal TaxStartPoint
        {
            get
            {
                return this._taxstartpoint;
            }
            set
            {
                this._taxstartpoint = value;
            }
        }

        public decimal UpperLimit
        {
            get
            {
                return this._upperlimit;
            }
            set
            {
                this._upperlimit = value;
            }
        }
    }
}

