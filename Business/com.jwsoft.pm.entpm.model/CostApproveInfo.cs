namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class CostApproveInfo
    {
        private string _CostType = string.Empty;
        private bool _IsSelected;
        private int _NoteID;
        private decimal _Quantity;
        private float _UnitPrice;

        public string CostType
        {
            get
            {
                return this._CostType;
            }
            set
            {
                this._CostType = value;
            }
        }

        public bool IsSelected
        {
            get
            {
                return this._IsSelected;
            }
            set
            {
                this._IsSelected = value;
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

        public float UnitPrice
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

