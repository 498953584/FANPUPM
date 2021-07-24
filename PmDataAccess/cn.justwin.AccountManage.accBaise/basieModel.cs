namespace cn.justwin.AccountManage.accBaise
{
    using System;

    public class basieModel
    {
        private int? _accallot;
        private int? _authority;
        private decimal? _borrowrate;
        private decimal? _fundratio;
        private int _id;
        private int? _israte;
        private int? _startMoney;

        public int? accAllot
        {
            get
            {
                return this._accallot;
            }
            set
            {
                this._accallot = value;
            }
        }

        public int? authority
        {
            get
            {
                return this._authority;
            }
            set
            {
                this._authority = value;
            }
        }

        public decimal? borrowRate
        {
            get
            {
                return this._borrowrate;
            }
            set
            {
                this._borrowrate = value;
            }
        }

        public decimal? fundRatio
        {
            get
            {
                return this._fundratio;
            }
            set
            {
                this._fundratio = value;
            }
        }

        public int id
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

        public int? isRate
        {
            get
            {
                return this._israte;
            }
            set
            {
                this._israte = value;
            }
        }

        public int? startMoney
        {
            get
            {
                return this._startMoney;
            }
            set
            {
                this._startMoney = value;
            }
        }
    }
}

