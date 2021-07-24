namespace cn.justwin.VehiclesModel
{
    using System;

    [Serializable]
    public class InsuranceAndReviewModel
    {
        private string _code;
        private string _date;
        private System.Guid _guid;
        private int? _type;
        private System.Guid _vehiclecode;

        public string code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
            }
        }

        public string Date
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

        public int? Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        public System.Guid VehicleCode
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
    }
}

