namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class SalaryIncomeTaxModel
    {
        private int? _deduct;
        private int? _lowerlimit;
        private decimal? _taxrate;
        private int _taxrateid;
        private int? _upperlimit;

        public int? Deduct
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

        public int? LowerLimit
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

        public decimal? TaxRate
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

        public int TaxRateID
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

        public int? UpperLimit
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

