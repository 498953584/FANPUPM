namespace cn.justwin.stockDAL
{
    using System;

    [Serializable]
    public class AllocationModel
    {
        private string _acode;
        private string _aid;
        private string _annx;
        private string _explain;
        private int _flowstate;
        private string _InAllocationPerson;
        private string _intime;
        private bool _isinb;
        private string _isInTime;
        private bool _isouta;
        private string _isOutTime;
        private string _OutAllocationPerson;
        private string _person;
        private string _tcodea;
        private string _tcodeb;

        public string Acode
        {
            get
            {
                return this._acode;
            }
            set
            {
                this._acode = value;
            }
        }

        public string Aid
        {
            get
            {
                return this._aid;
            }
            set
            {
                this._aid = value;
            }
        }

        public string Annex
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

        public string Explain
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

        public int FlowState
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

        public string InAllocationPerson
        {
            get
            {
                return this._InAllocationPerson;
            }
            set
            {
                this._InAllocationPerson = value;
            }
        }

        public string InTime
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

        public bool IsInB
        {
            get
            {
                return this._isinb;
            }
            set
            {
                this._isinb = value;
            }
        }

        public string IsInTime
        {
            get
            {
                return this._isInTime;
            }
            set
            {
                this._isInTime = value;
            }
        }

        public bool IsOutA
        {
            get
            {
                return this._isouta;
            }
            set
            {
                this._isouta = value;
            }
        }

        public string IsOutTime
        {
            get
            {
                return this._isOutTime;
            }
            set
            {
                this._isOutTime = value;
            }
        }

        public string OutAllocationPerson
        {
            get
            {
                return this._OutAllocationPerson;
            }
            set
            {
                this._OutAllocationPerson = value;
            }
        }

        public string Person
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

        public string TCodea
        {
            get
            {
                return this._tcodea;
            }
            set
            {
                this._tcodea = value;
            }
        }

        public string TCodeb
        {
            get
            {
                return this._tcodeb;
            }
            set
            {
                this._tcodeb = value;
            }
        }
    }
}

