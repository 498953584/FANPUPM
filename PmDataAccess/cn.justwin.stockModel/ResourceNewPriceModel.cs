namespace cn.justwin.stockModel
{
    using System;

    public class ResourceNewPriceModel
    {
        private string _price;
        private string _rcode;
        private string _rpid;
        private string _rptcode;

        public string Price
        {
            get
            {
                return this._price;
            }
            set
            {
                this._price = value;
            }
        }

        public string rCode
        {
            get
            {
                return this._rcode;
            }
            set
            {
                this._rcode = value;
            }
        }

        public string rptCode
        {
            get
            {
                return this._rptcode;
            }
            set
            {
                this._rptcode = value;
            }
        }

        public string rptId
        {
            get
            {
                return this._rpid;
            }
            set
            {
                this._rpid = value;
            }
        }
    }
}

