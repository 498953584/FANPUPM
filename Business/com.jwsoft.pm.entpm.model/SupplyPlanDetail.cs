namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class SupplyPlanDetail
    {
        private DateTime _arrivalDate;
        private decimal _factPrice;
        private int _imteid;
        private decimal _price;
        private decimal _stuffcount;
        private string _stuffname;
        private string _stuffspecs;
        private string _supplyplancode;
        private string _sutffcode;
        private string _unit;

        public DateTime ArrivalDate
        {
            get
            {
                return this._arrivalDate;
            }
            set
            {
                this._arrivalDate = value;
            }
        }

        public decimal FactPrice
        {
            get
            {
                return this._factPrice;
            }
            set
            {
                this._factPrice = value;
            }
        }

        public int ItemID
        {
            get
            {
                return this._imteid;
            }
            set
            {
                this._imteid = value;
            }
        }

        public string PlanCode
        {
            get
            {
                return this._supplyplancode;
            }
            set
            {
                this._supplyplancode = value;
            }
        }

        public string StuffCode
        {
            get
            {
                return this._sutffcode;
            }
            set
            {
                this._sutffcode = value;
            }
        }

        public decimal StuffCount
        {
            get
            {
                return this._stuffcount;
            }
            set
            {
                this._stuffcount = value;
            }
        }

        public string StuffName
        {
            get
            {
                return this._stuffname;
            }
            set
            {
                this._stuffname = value;
            }
        }

        public decimal StuffPrice
        {
            get
            {
                return this._price;
            }
            set
            {
                this._price = value;
            }
        }

        public string StuffSpecs
        {
            get
            {
                return this._stuffspecs;
            }
            set
            {
                this._stuffspecs = value;
            }
        }

        public string StuffUnit
        {
            get
            {
                return this._unit;
            }
            set
            {
                this._unit = value;
            }
        }
    }
}

