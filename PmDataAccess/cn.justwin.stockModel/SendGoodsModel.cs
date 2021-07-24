namespace cn.justwin.stockModel
{
    using System;

    public class SendGoodsModel
    {
        private decimal? _number;
        private decimal? _price;
        private string _scode;
        private string _sgid;
        private string _sncode;
        private string _suppycode;

        public decimal? Number
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

        public decimal? Price
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

        public string snCode
        {
            get
            {
                return this._sncode;
            }
            set
            {
                this._sncode = value;
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

