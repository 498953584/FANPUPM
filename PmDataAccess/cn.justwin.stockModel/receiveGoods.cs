namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class receiveGoods
    {
        private decimal? _number;
        private decimal? _price;
        private string _rnid;
        private string _scode;
        private string _sgid;
        private string _suppycode;

        public decimal? number
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

        public decimal? price
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

        public string rnID
        {
            get
            {
                return this._rnid;
            }
            set
            {
                this._rnid = value;
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

        public string sgId
        {
            get
            {
                return this._sgid;
            }
            set
            {
                this._sgid = value;
            }
        }

        public string suppyCode
        {
            get
            {
                return this._suppycode;
            }
            set
            {
                this._suppycode = value;
            }
        }
    }
}

