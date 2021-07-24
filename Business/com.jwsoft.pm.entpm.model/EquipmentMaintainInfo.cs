namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class EquipmentMaintainInfo
    {
        private string _Appraisal = "";
        private DateTime _AppraisalDate = DateTime.MinValue;
        private string _Appraiser = "";
        private Guid _EquipmentUniqueCode = Guid.Empty;
        private string _Fault = "";
        private string _MaintainceMan = "";
        private string _MaintainContent = "";
        private decimal _MaintainCost;
        private DateTime _MaintainDate = DateTime.MinValue;
        private string _MaintainType = "";
        private Guid _MaintainUniqueCode = Guid.Empty;
        private DateTime _RecordDate = DateTime.MinValue;

        public string Appraisal
        {
            get
            {
                return this._Appraisal;
            }
            set
            {
                this._Appraisal = value;
            }
        }

        public DateTime AppraisalDate
        {
            get
            {
                return this._AppraisalDate;
            }
            set
            {
                this._AppraisalDate = value;
            }
        }

        public string Appraiser
        {
            get
            {
                return this._Appraiser;
            }
            set
            {
                this._Appraiser = value;
            }
        }

        public Guid EquipmentUniqueCode
        {
            get
            {
                return this._EquipmentUniqueCode;
            }
            set
            {
                this._EquipmentUniqueCode = value;
            }
        }

        public string Fault
        {
            get
            {
                return this._Fault;
            }
            set
            {
                this._Fault = value;
            }
        }

        public string MaintainceMan
        {
            get
            {
                return this._MaintainceMan;
            }
            set
            {
                this._MaintainceMan = value;
            }
        }

        public string MaintainContent
        {
            get
            {
                return this._MaintainContent;
            }
            set
            {
                this._MaintainContent = value;
            }
        }

        public decimal MaintainCost
        {
            get
            {
                return this._MaintainCost;
            }
            set
            {
                this._MaintainCost = value;
            }
        }

        public DateTime MaintainDate
        {
            get
            {
                return this._MaintainDate;
            }
            set
            {
                this._MaintainDate = value;
            }
        }

        public string MaintainType
        {
            get
            {
                return this._MaintainType;
            }
            set
            {
                this._MaintainType = value;
            }
        }

        public Guid MaintainUniqueCode
        {
            get
            {
                return this._MaintainUniqueCode;
            }
            set
            {
                this._MaintainUniqueCode = value;
            }
        }

        public DateTime RecordDate
        {
            get
            {
                return this._RecordDate;
            }
            set
            {
                this._RecordDate = value;
            }
        }
    }
}

