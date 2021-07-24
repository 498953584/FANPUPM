namespace cn.justwin.VehiclesModel
{
    using System;

    [Serializable]
    public class MainModel
    {
        private string _ability;
        private string _address;
        private decimal? _depreciationrate;
        private string _enginecode;
        private decimal? _fatfare;
        private Guid _guid;
        private DateTime? _inspectiondate;
        private DateTime? _insurancedate;
        private int? _isshare;
        private string _manufacturedate;
        private DateTime? _onhouserdate;
        private DateTime? _purchasedate;
        private string _purchaser;
        private decimal? _recordedprice;
        private string _remark;
        private string _sparekey;
        private string _specification;
        private Guid _state;
        private string _vehiclecode;
        private string _vehicleidentify;
        private string _vehiclename;
        private Guid _vehicletype;

        public string Ability
        {
            get
            {
                return this._ability;
            }
            set
            {
                this._ability = value;
            }
        }

        public string Address
        {
            get
            {
                return this._address;
            }
            set
            {
                this._address = value;
            }
        }

        public decimal? DepreciationRate
        {
            get
            {
                return this._depreciationrate;
            }
            set
            {
                this._depreciationrate = value;
            }
        }

        public string EngineCode
        {
            get
            {
                return this._enginecode;
            }
            set
            {
                this._enginecode = value;
            }
        }

        public decimal? Fatfare
        {
            get
            {
                return this._fatfare;
            }
            set
            {
                this._fatfare = value;
            }
        }

        public Guid guid
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

        public DateTime? InspectionDate
        {
            get
            {
                return this._inspectiondate;
            }
            set
            {
                this._inspectiondate = value;
            }
        }

        public DateTime? InsuranceDate
        {
            get
            {
                return this._insurancedate;
            }
            set
            {
                this._insurancedate = value;
            }
        }

        public int? IsShare
        {
            get
            {
                return this._isshare;
            }
            set
            {
                this._isshare = value;
            }
        }

        public string ManufactureDate
        {
            get
            {
                return this._manufacturedate;
            }
            set
            {
                this._manufacturedate = value;
            }
        }

        public DateTime? OnHouserDate
        {
            get
            {
                return this._onhouserdate;
            }
            set
            {
                this._onhouserdate = value;
            }
        }

        public DateTime? PurchaseDate
        {
            get
            {
                return this._purchasedate;
            }
            set
            {
                this._purchasedate = value;
            }
        }

        public string Purchaser
        {
            get
            {
                return this._purchaser;
            }
            set
            {
                this._purchaser = value;
            }
        }

        public decimal? Recordedprice
        {
            get
            {
                return this._recordedprice;
            }
            set
            {
                this._recordedprice = value;
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

        public string Sparekey
        {
            get
            {
                return this._sparekey;
            }
            set
            {
                this._sparekey = value;
            }
        }

        public string Specification
        {
            get
            {
                return this._specification;
            }
            set
            {
                this._specification = value;
            }
        }

        public Guid State
        {
            get
            {
                return this._state;
            }
            set
            {
                this._state = value;
            }
        }

        public string VehicleCode
        {
            get
            {
                return this._vehiclecode;
            }
            set
            {
                this._vehiclecode = value;
            }
        }

        public string VehicleIdentify
        {
            get
            {
                return this._vehicleidentify;
            }
            set
            {
                this._vehicleidentify = value;
            }
        }

        public string VehicleName
        {
            get
            {
                return this._vehiclename;
            }
            set
            {
                this._vehiclename = value;
            }
        }

        public Guid VehicleType
        {
            get
            {
                return this._vehicletype;
            }
            set
            {
                this._vehicletype = value;
            }
        }
    }
}

