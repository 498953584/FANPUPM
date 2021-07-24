namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class ModifyStockModel
    {
        private DateTime? _arrivalDate;
        private string _corp;
        private string _id;
        private string _modifyId;
        private string _pscode;
        private string _purchaseId;
        private decimal _quantity;
        private string _scode;
        private decimal _sprice;

        public DateTime? ArrivalDate
        {
            get
            {
                return this._arrivalDate;
            }
            set
            {
                this._arrivalDate = value;
            }
        }

        public string Corp
        {
            get
            {
                return this._corp;
            }
            set
            {
                this._corp = value;
            }
        }

        public string Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string ModifyId
        {
            get
            {
                return this._modifyId;
            }
            set
            {
                this._modifyId = value;
            }
        }

        public string Pscode
        {
            get
            {
                return this._pscode;
            }
            set
            {
                this._pscode = value;
            }
        }

        public string PurchaseId
        {
            get
            {
                return this._purchaseId;
            }
            set
            {
                this._purchaseId = value;
            }
        }

        public decimal Quantity
        {
            get
            {
                return this._quantity;
            }
            set
            {
                this._quantity = value;
            }
        }

        public string Scode
        {
            get
            {
                return this._scode;
            }
            set
            {
                this._scode = value;
            }
        }

        public decimal Sprice
        {
            get
            {
                return this._sprice;
            }
            set
            {
                this._sprice = value;
            }
        }
    }
}

