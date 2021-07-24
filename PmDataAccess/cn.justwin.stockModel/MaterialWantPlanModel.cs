namespace cn.justwin.stockModel
{
    using System;

    public class MaterialWantPlanModel
    {
        private int _acceptstate;
        private string _annx;
        private bool _containpending;
        private string _equipmentId;
        private string _explain;
        private int _flowstate = -1;
        private string _intime;
        private string _person;
        private string _procode;
        private string _swcode;
        private string _swid;

        public int acceptstate
        {
            get
            {
                return this._acceptstate;
            }
            set
            {
                this._acceptstate = value;
            }
        }

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

        public bool ContainPending
        {
            get
            {
                return this._containpending;
            }
            set
            {
                this._containpending = value;
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

        public string intime
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

        public string swcode
        {
            get
            {
                return this._swcode;
            }
            set
            {
                this._swcode = value;
            }
        }

        public string swid
        {
            get
            {
                return this._swid;
            }
            set
            {
                this._swid = value;
            }
        }
    }
}

