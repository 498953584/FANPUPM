namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class InventoryDetail
    {
        private decimal _bookscalar;
        private decimal _factscalar;
        private int _inventorydetailid;
        private int _inventoryid;
        private int _materialid;
        private decimal _unitprice;

        public decimal BookScalar
        {
            get
            {
                return this._bookscalar;
            }
            set
            {
                this._bookscalar = value;
            }
        }

        public decimal FactScalar
        {
            get
            {
                return this._factscalar;
            }
            set
            {
                this._factscalar = value;
            }
        }

        public int InventoryDetailID
        {
            get
            {
                return this._inventorydetailid;
            }
            set
            {
                this._inventorydetailid = value;
            }
        }

        public int InventoryID
        {
            get
            {
                return this._inventoryid;
            }
            set
            {
                this._inventoryid = value;
            }
        }

        public int MaterialId
        {
            get
            {
                return this._materialid;
            }
            set
            {
                this._materialid = value;
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return this._unitprice;
            }
            set
            {
                this._unitprice = value;
            }
        }
    }
}

