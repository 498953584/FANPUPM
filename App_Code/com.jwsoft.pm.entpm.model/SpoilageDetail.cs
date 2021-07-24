namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class SpoilageDetail
    {
        private int _materialid;
        private decimal _scalar;
        private int _spoilagedetailid;
        private int _spoilageid;
        private decimal _unitprice;

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

        public decimal Scalar
        {
            get
            {
                return this._scalar;
            }
            set
            {
                this._scalar = value;
            }
        }

        public int SpoilageDetailID
        {
            get
            {
                return this._spoilagedetailid;
            }
            set
            {
                this._spoilagedetailid = value;
            }
        }

        public int SpoilageID
        {
            get
            {
                return this._spoilageid;
            }
            set
            {
                this._spoilageid = value;
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

