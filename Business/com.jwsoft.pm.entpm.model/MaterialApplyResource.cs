namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class MaterialApplyResource
    {
        private Guid _ApplyUniqueCode;
        private DateTime _ArrivalDate;
        private bool _IsShow;
        private int _NoteID;
        private decimal _Quantity;
        private string _ResourceCode;
        private int _ResourceStyle;
        private decimal _SumPrice;
        private int _Supply;
        private decimal _UnitPrice;

        public Guid ApplyUniqueCode
        {
            get
            {
                return this._ApplyUniqueCode;
            }
            set
            {
                this._ApplyUniqueCode = value;
            }
        }

        public DateTime ArrivalDate
        {
            get
            {
                return this._ArrivalDate;
            }
            set
            {
                this._ArrivalDate = value;
            }
        }

        public bool IsShow
        {
            get
            {
                return this._IsShow;
            }
            set
            {
                this._IsShow = value;
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

        public decimal SumPrice
        {
            get
            {
                return this._SumPrice;
            }
            set
            {
                this._SumPrice = value;
            }
        }

        public int Supply
        {
            get
            {
                return this._Supply;
            }
            set
            {
                this._Supply = value;
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
    }
}

