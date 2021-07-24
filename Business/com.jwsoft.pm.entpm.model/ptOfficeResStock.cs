namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ptOfficeResStock
    {
        private decimal _avgprice;
        private int _depotid;
        private int _materialid;
        private decimal _number;
        private int _recordid;

        public decimal AvgPrice
        {
            get
            {
                return this._avgprice;
            }
            set
            {
                this._avgprice = value;
            }
        }

        public int DepotID
        {
            get
            {
                return this._depotid;
            }
            set
            {
                this._depotid = value;
            }
        }

        public int MaterialID
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

        public decimal Number
        {
            get
            {
                return this._number;
            }
            set
            {
                this._number = value;
            }
        }

        public int RecordID
        {
            get
            {
                return this._recordid;
            }
            set
            {
                this._recordid = value;
            }
        }
    }
}

