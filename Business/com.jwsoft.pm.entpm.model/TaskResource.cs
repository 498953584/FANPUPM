namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class TaskResource
    {
        private int _NoteID;
        private Guid _ProjectCode = Guid.Empty;
        private decimal _Quantity;
        private string _RationItem = string.Empty;
        private string _ResourceCode = string.Empty;
        private int _ResourceStyle = -2147483648;
        private string _StepCode = string.Empty;
        private string _TaskCode = string.Empty;
        private decimal _UnitPrice;
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

        public Guid ProjectCode
        {
            get
            {
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
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

        public string RationItem
        {
            get
            {
                return this._RationItem;
            }
            set
            {
                this._RationItem = value;
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

        public int ResourceStyle
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

        public string StepCode
        {
            get
            {
                return this._StepCode;
            }
            set
            {
                this._StepCode = value;
            }
        }

        public string TaskCode
        {
            get
            {
                return this._TaskCode;
            }
            set
            {
                this._TaskCode = value;
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return this._UnitPrice;
            }
            set
            {
                this._UnitPrice = value;
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

