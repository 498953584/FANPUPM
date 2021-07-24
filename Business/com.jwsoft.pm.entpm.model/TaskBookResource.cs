namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class TaskBookResource
    {
        private decimal _FactQuantity;
        private int _NoteID;
        private Guid _ProjectCode = Guid.Empty;
        private decimal _Quantity;
        private string _ResourceCode;
        private string _ResourceName = "";
        private int _ResourceStyle;
        private string _Specification = "";
        private string _TaskBookCode;
        private string _TaskCode = string.Empty;
        private string _UnitName = "";
        private double _UnitPrice;

        public decimal FactQuantity
        {
            get
            {
                return this._FactQuantity;
            }
            set
            {
                this._FactQuantity = value;
            }
        }

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

        public string TaskBookCode
        {
            get
            {
                return this._TaskBookCode;
            }
            set
            {
                this._TaskBookCode = value;
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

        public string UnitName
        {
            get
            {
                return this._UnitName;
            }
            set
            {
                this._UnitName = value;
            }
        }

        public double UnitPrice
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
    }
}

