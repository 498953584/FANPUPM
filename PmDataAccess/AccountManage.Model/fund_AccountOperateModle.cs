namespace AccountManage.Model
{
    using System;

    [Serializable]
    public class fund_AccountOperateModle
    {
        private DateTime? _accountime;
        private string _accountman;
        private string _AccountMark;
        private decimal? _accountmony;
        private string _accountnum;
        private int? _accountype;
        private string _acredence;
        private int _aoid;
        private string _contracnum;
        private string _depid;
        private int? _isaccount;
        private string _projnum;
        private decimal? _realmony;
        private DateTime? _sumitime;
        private string _sumitman;

        public DateTime? AccounTime
        {
            get
            {
                return this._accountime;
            }
            set
            {
                this._accountime = value;
            }
        }

        public string AccountMan
        {
            get
            {
                return this._accountman;
            }
            set
            {
                this._accountman = value;
            }
        }

        public string AccountMark
        {
            get
            {
                return this._AccountMark;
            }
            set
            {
                this._AccountMark = value;
            }
        }

        public decimal? AccountMony
        {
            get
            {
                return this._accountmony;
            }
            set
            {
                this._accountmony = value;
            }
        }

        public string AccountNum
        {
            get
            {
                return this._accountnum;
            }
            set
            {
                this._accountnum = value;
            }
        }

        public int? AccounType
        {
            get
            {
                return this._accountype;
            }
            set
            {
                this._accountype = value;
            }
        }

        public string Acredence
        {
            get
            {
                return this._acredence;
            }
            set
            {
                this._acredence = value;
            }
        }

        public int AoID
        {
            get
            {
                return this._aoid;
            }
            set
            {
                this._aoid = value;
            }
        }

        public string contracnum
        {
            get
            {
                return this._contracnum;
            }
            set
            {
                this._contracnum = value;
            }
        }

        public string DepID
        {
            get
            {
                return this._depid;
            }
            set
            {
                this._depid = value;
            }
        }

        public int? IsAccount
        {
            get
            {
                return this._isaccount;
            }
            set
            {
                this._isaccount = value;
            }
        }

        public string projnum
        {
            get
            {
                return this._projnum;
            }
            set
            {
                this._projnum = value;
            }
        }

        public decimal? RealMony
        {
            get
            {
                return this._realmony;
            }
            set
            {
                this._realmony = value;
            }
        }

        public DateTime? SumiTime
        {
            get
            {
                return this._sumitime;
            }
            set
            {
                this._sumitime = value;
            }
        }

        public string SumitMan
        {
            get
            {
                return this._sumitman;
            }
            set
            {
                this._sumitman = value;
            }
        }
    }
}

