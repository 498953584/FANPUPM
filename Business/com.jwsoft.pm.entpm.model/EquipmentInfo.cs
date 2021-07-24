namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class EquipmentInfo
    {
        private int _CheckCycle;
        private string _ContactDept = string.Empty;
        private decimal _depreciation;
        private string _EquipmentManCode = "";
        private string _EquipmentName = "";
        private string _EquipmentType = "";
        private Guid _EquipmentUniqueCode = Guid.Empty;
        private string _FactoryCode = "";
        private DateTime _FactoryDate = DateTime.MinValue;
        private int _LocateDeptId;
        private string _Manufacturer = "";
        private decimal _OriginalPrice;
        private DateTime _PurchaseDate = DateTime.MinValue;
        private string _Remark = "";
        private int _ServiceYear;
        private string _Spec = "";
        private int _State = 1;
        private string _ThePrecision = "";
        private bool _UserState;
        private int noteSeqId;

        public int CheckCycle
        {
            get
            {
                return this._CheckCycle;
            }
            set
            {
                this._CheckCycle = value;
            }
        }

        public string ContactDept
        {
            get
            {
                return this._ContactDept;
            }
            set
            {
                this._ContactDept = value;
            }
        }

        public decimal depreciation
        {
            get
            {
                return this._depreciation;
            }
            set
            {
                this._depreciation = value;
            }
        }

        public string EquipmentManCode
        {
            get
            {
                return this._EquipmentManCode;
            }
            set
            {
                this._EquipmentManCode = value;
            }
        }

        public string EquipmentName
        {
            get
            {
                return this._EquipmentName;
            }
            set
            {
                this._EquipmentName = value;
            }
        }

        public string EquipmentType
        {
            get
            {
                return this._EquipmentType;
            }
            set
            {
                this._EquipmentType = value;
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

        public string FactoryCode
        {
            get
            {
                return this._FactoryCode;
            }
            set
            {
                this._FactoryCode = value;
            }
        }

        public DateTime FactoryDate
        {
            get
            {
                return this._FactoryDate;
            }
            set
            {
                this._FactoryDate = value;
            }
        }

        public int LocateDeptId
        {
            get
            {
                return this._LocateDeptId;
            }
            set
            {
                this._LocateDeptId = value;
            }
        }

        public string Manufacturer
        {
            get
            {
                return this._Manufacturer;
            }
            set
            {
                this._Manufacturer = value;
            }
        }

        public int NoteSeqId
        {
            get
            {
                return this.noteSeqId;
            }
            set
            {
                this.noteSeqId = value;
            }
        }

        public decimal OriginalPrice
        {
            get
            {
                return this._OriginalPrice;
            }
            set
            {
                this._OriginalPrice = value;
            }
        }

        public DateTime PurchaseDate
        {
            get
            {
                return this._PurchaseDate;
            }
            set
            {
                this._PurchaseDate = value;
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

        public int ServiceYear
        {
            get
            {
                return this._ServiceYear;
            }
            set
            {
                this._ServiceYear = value;
            }
        }

        public string Spec
        {
            get
            {
                return this._Spec;
            }
            set
            {
                this._Spec = value;
            }
        }

        public int State
        {
            get
            {
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string ThePrecision
        {
            get
            {
                return this._ThePrecision;
            }
            set
            {
                this._ThePrecision = value;
            }
        }

        public bool UserState
        {
            get
            {
                return this._UserState;
            }
            set
            {
                this._UserState = value;
            }
        }
    }
}

