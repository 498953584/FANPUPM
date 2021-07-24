namespace cn.justwin.stockModel
{
    using System;
    using System.Data.SqlTypes;

    [Serializable]
    public class OutReserveModel
    {
        private string _annx;
        private string _equipmentId;
        private string _explain;
        private int _flowstate;
        private DateTime _intime;
        private bool _isout;
        private SqlDateTime _isouttime;
        private string _orcode;
        private string _orid;
        private string _person;
        private string _pickingpeople;
        private string _pickingsector;
        private string _procode;
        private string _tcode;

        public string annx
        {
            get
            {
                return this._annx;
            }
            set
            {
                this._annx = value;
            }
        }

        public string EquipmentId
        {
            get
            {
                return this._equipmentId;
            }
            set
            {
                this._equipmentId = value;
            }
        }

        public string explain
        {
            get
            {
                return this._explain;
            }
            set
            {
                this._explain = value;
            }
        }

        public int flowstate
        {
            get
            {
                return this._flowstate;
            }
            set
            {
                this._flowstate = value;
            }
        }

        public DateTime intime
        {
            get
            {
                return this._intime;
            }
            set
            {
                this._intime = value;
            }
        }

        public bool isout
        {
            get
            {
                return this._isout;
            }
            set
            {
                this._isout = value;
            }
        }

        public SqlDateTime IsOutTime
        {
            get
            {
                return this._isouttime;
            }
            set
            {
                this._isouttime = value;
            }
        }

        public string orcode
        {
            get
            {
                return this._orcode;
            }
            set
            {
                this._orcode = value;
            }
        }

        public string orid
        {
            get
            {
                return this._orid;
            }
            set
            {
                this._orid = value;
            }
        }

        public string person
        {
            get
            {
                return this._person;
            }
            set
            {
                this._person = value;
            }
        }

        public string PickingPeople
        {
            get
            {
                return this._pickingpeople;
            }
            set
            {
                this._pickingpeople = value;
            }
        }

        public string PickingSector
        {
            get
            {
                return this._pickingsector;
            }
            set
            {
                this._pickingsector = value;
            }
        }

        public string procode
        {
            get
            {
                return this._procode;
            }
            set
            {
                this._procode = value;
            }
        }

        public string tcode
        {
            get
            {
                return this._tcode;
            }
            set
            {
                this._tcode = value;
            }
        }
    }
}

