namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class MeasureDataInfo
    {
        private string _AccessoriesName = "";
        private int _BigSort;
        private int _FlowState;
        private int _Id;
        private string _ItemName = "";
        private string _JoinPerson;
        private string _PrjCode = "";
        private string _ReceivePerson;
        private string _Remark = "";
        private string _SerialNumber = "";
        private int _SmallSort;
        private string _TechGuid;

        public string AccessoriesName
        {
            get
            {
                return this._AccessoriesName;
            }
            set
            {
                this._AccessoriesName = value;
            }
        }

        public int BigSort
        {
            get
            {
                return this._BigSort;
            }
            set
            {
                this._BigSort = value;
            }
        }

        public int FlowState
        {
            get
            {
                return this._FlowState;
            }
            set
            {
                this._FlowState = value;
            }
        }

        public int Id
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

        public string ItemName
        {
            get
            {
                return this._ItemName;
            }
            set
            {
                this._ItemName = value;
            }
        }

        public string JoinPerson
        {
            get
            {
                return this._JoinPerson;
            }
            set
            {
                this._JoinPerson = value;
            }
        }

        public string PrjCode
        {
            get
            {
                return this._PrjCode;
            }
            set
            {
                this._PrjCode = value;
            }
        }

        public string ReceivePerson
        {
            get
            {
                return this._ReceivePerson;
            }
            set
            {
                this._ReceivePerson = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        public string SerialNumber
        {
            get
            {
                return this._SerialNumber;
            }
            set
            {
                this._SerialNumber = value;
            }
        }

        public int SmallSort
        {
            get
            {
                return this._SmallSort;
            }
            set
            {
                this._SmallSort = value;
            }
        }

        public string TechGuid
        {
            get
            {
                return this._TechGuid;
            }
            set
            {
                this._TechGuid = value;
            }
        }
    }
}

