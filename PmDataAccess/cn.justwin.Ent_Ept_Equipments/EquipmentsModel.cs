namespace cn.justwin.Ent_Ept_Equipments
{
    using System;

    [Serializable]
    public class EquipmentsModel
    {
        private int _checkcycle;
        private string _contactdept;
        private decimal? _depreciation = 0;
        private string _equipmentmanualcode;
        private string _equipmentname;
        private string _equipmenttype;
        private Guid _equipmentuniquecode;
        private string _factorycode;
        private DateTime _factorydate;
        private int _locatedeptid;
        private string _manufacturer;
        private int _notesequenceid;
        private decimal _originalprice;
        private string _project;
        private DateTime _purchasedate;
        private string _remark;
        private int _serviceyear;
        private string _spec;
        private int _state;
        private string _theprecision;

        public int CheckCycle
        {
            get
            {
                return this._checkcycle;
            }
            set
            {
                this._checkcycle = value;
            }
        }

        public string ContactDept
        {
            get
            {
                return this._contactdept;
            }
            set
            {
                this._contactdept = value;
            }
        }

        public decimal? depreciation
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

        public string EquipmentManualCode
        {
            get
            {
                return this._equipmentmanualcode;
            }
            set
            {
                this._equipmentmanualcode = value;
            }
        }

        public string EquipmentName
        {
            get
            {
                return this._equipmentname;
            }
            set
            {
                this._equipmentname = value;
            }
        }

        public string EquipmentType
        {
            get
            {
                return this._equipmenttype;
            }
            set
            {
                this._equipmenttype = value;
            }
        }

        public Guid EquipmentUniqueCode
        {
            get
            {
                return this._equipmentuniquecode;
            }
            set
            {
                this._equipmentuniquecode = value;
            }
        }

        public string FactoryCode
        {
            get
            {
                return this._factorycode;
            }
            set
            {
                this._factorycode = value;
            }
        }

        public DateTime FactoryDate
        {
            get
            {
                return this._factorydate;
            }
            set
            {
                this._factorydate = value;
            }
        }

        public int LocateDeptID
        {
            get
            {
                return this._locatedeptid;
            }
            set
            {
                this._locatedeptid = value;
            }
        }

        public string Manufacturer
        {
            get
            {
                return this._manufacturer;
            }
            set
            {
                this._manufacturer = value;
            }
        }

        public int NoteSequenceID
        {
            get
            {
                return this._notesequenceid;
            }
            set
            {
                this._notesequenceid = value;
            }
        }

        public decimal OriginalPrice
        {
            get
            {
                return this._originalprice;
            }
            set
            {
                this._originalprice = value;
            }
        }

        public string project
        {
            get
            {
                return this._project;
            }
            set
            {
                this._project = value;
            }
        }

        public DateTime PurchaseDate
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

        public int ServiceYear
        {
            get
            {
                return this._serviceyear;
            }
            set
            {
                this._serviceyear = value;
            }
        }

        public string Spec
        {
            get
            {
                return this._spec;
            }
            set
            {
                this._spec = value;
            }
        }

        public int State
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

        public string ThePrecision
        {
            get
            {
                return this._theprecision;
            }
            set
            {
                this._theprecision = value;
            }
        }
    }
}

