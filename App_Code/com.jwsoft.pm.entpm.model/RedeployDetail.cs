namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class RedeployDetail
    {
        private int _folddepositoryid;
        private int _materialid;
        private int _redeploydetailid;
        private int _redeployid;
        private decimal _scalar;
        private decimal _unitprice;

        public int FoldDepositoryID
        {
            get
            {
                return this._folddepositoryid;
            }
            set
            {
                this._folddepositoryid = value;
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

        public int RedeployDetailID
        {
            get
            {
                return this._redeploydetailid;
            }
            set
            {
                this._redeploydetailid = value;
            }
        }

        public int Redeployid
        {
            get
            {
                return this._redeployid;
            }
            set
            {
                this._redeployid = value;
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

