namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class SmPurchaseplanStockModel
    {
        private string _arrivalDate;
        private decimal _number;
        private string _ppcode;
        private string _remark;
        private string _scode;
        private string _wpsid;

        public string ArrivalDate
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

        public decimal number
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

        public string ppcode
        {
            get
            {
                return this._ppcode;
            }
            set
            {
                this._ppcode = value;
            }
        }

        public string remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }

        public string scode
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

        public string wpsid
        {
            get
            {
                return this._wpsid;
            }
            set
            {
                this._wpsid = value;
            }
        }
    }
}

