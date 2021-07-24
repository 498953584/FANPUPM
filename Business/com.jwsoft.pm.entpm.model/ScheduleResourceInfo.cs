namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class ScheduleResourceInfo : BaseInfo
    {
        private int _NoteID;
        private decimal _Price;
        private decimal _Quantity;
        private string _ResourceCode;
        private string _ResourceName;
        private ResourceTypeStyle _ResourceStyle;
        private string _ResourceUnit;
        private string _ScheduleCode;
        private int _SourceType = 1;
        private string _Specification;
        private Guid _VersionCode = Guid.Empty;
        private decimal _Wastage;

        public int NoteID
        {
            get
            {
                return this._NoteID;
            }
            set
            {
                this._NoteID = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this._Price;
            }
            set
            {
                this._Price = value;
            }
        }

        public decimal Quantity
        {
            get
            {
                return this._Quantity;
            }
            set
            {
                this._Quantity = value;
            }
        }

        public string ResourceCode
        {
            get
            {
                return this._ResourceCode;
            }
            set
            {
                this._ResourceCode = value;
            }
        }

        public string ResourceName
        {
            get
            {
                return this._ResourceName;
            }
            set
            {
                this._ResourceName = value;
            }
        }

        public ResourceTypeStyle ResourceStyle
        {
            get
            {
                return this._ResourceStyle;
            }
            set
            {
                this._ResourceStyle = value;
            }
        }

        public string ResourceUnit
        {
            get
            {
                return this._ResourceUnit;
            }
            set
            {
                this._ResourceUnit = value;
            }
        }

        public string ScheduleCode
        {
            get
            {
                return this._ScheduleCode;
            }
            set
            {
                this._ScheduleCode = value;
            }
        }

        public int SourceType
        {
            get
            {
                return this._SourceType;
            }
            set
            {
                this._SourceType = value;
            }
        }

        public string Specification
        {
            get
            {
                return this._Specification;
            }
            set
            {
                this._Specification = value;
            }
        }

        public Guid VersionCode
        {
            get
            {
                return this._VersionCode;
            }
            set
            {
                this._VersionCode = value;
            }
        }

        public decimal Wastage
        {
            get
            {
                return this._Wastage;
            }
            set
            {
                this._Wastage = value;
            }
        }
    }
}

