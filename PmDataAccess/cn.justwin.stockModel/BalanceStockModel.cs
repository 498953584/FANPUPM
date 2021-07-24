namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class BalanceStockModel
    {
        private decimal _arrivaledQuantity;
        private string _balanceId;
        private string _id;
        private string _purchaseId;

        public decimal ArrivaledQuantity
        {
            get
            {
                return this._arrivaledQuantity;
            }
            set
            {
                this._arrivaledQuantity = value;
            }
        }

        public string BalanceId
        {
            get
            {
                return this._balanceId;
            }
            set
            {
                this._balanceId = value;
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
    }
}

