namespace cn.justwin.VehiclesModel
{
    using System;

    [Serializable]
    public class Reimbursement
    {
        private DateTime? _date;
        private string _destination;
        private decimal? _fuelcosts;
        private System.Guid _guid;
        private decimal? _maintenancecosts;
        private string _reimbursementcode;
        private string _remark;
        private decimal? _repairs;
        private decimal? _tolls;
        private string _username;
        private System.Guid _vehicleguid;

        public DateTime? Date
        {
            get
            {
                return this._date;
            }
            set
            {
                this._date = value;
            }
        }

        public string Destination
        {
            get
            {
                return this._destination;
            }
            set
            {
                this._destination = value;
            }
        }

        public decimal? FuelCosts
        {
            get
            {
                return this._fuelcosts;
            }
            set
            {
                this._fuelcosts = value;
            }
        }

        public System.Guid Guid
        {
            get
            {
                return this._guid;
            }
            set
            {
                this._guid = value;
            }
        }

        public decimal? MaintenanceCosts
        {
            get
            {
                return this._maintenancecosts;
            }
            set
            {
                this._maintenancecosts = value;
            }
        }

        public string ReimbursementCode
        {
            get
            {
                return this._reimbursementcode;
            }
            set
            {
                this._reimbursementcode = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }

        public decimal? Repairs
        {
            get
            {
                return this._repairs;
            }
            set
            {
                this._repairs = value;
            }
        }

        public decimal? Tolls
        {
            get
            {
                return this._tolls;
            }
            set
            {
                this._tolls = value;
            }
        }

        public string UserName
        {
            get
            {
                return this._username;
            }
            set
            {
                this._username = value;
            }
        }

        public System.Guid VehicleGuid
        {
            get
            {
                return this._vehicleguid;
            }
            set
            {
                this._vehicleguid = value;
            }
        }
    }
}

