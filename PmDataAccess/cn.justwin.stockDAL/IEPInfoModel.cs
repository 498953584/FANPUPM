namespace cn.justwin.stockDAL
{
    using System;

    public class IEPInfoModel
    {
        private DateTime? _adddate;
        private string _addpeople;
        private string _conid;
        private string _id;
        private string _iepid;
        private string _infonum;
        private decimal? _money;
        private string _remark;
        private int? _type;

        public DateTime? addDate
        {
            get
            {
                return this._adddate;
            }
            set
            {
                this._adddate = value;
            }
        }

        public string addPeople
        {
            get
            {
                return this._addpeople;
            }
            set
            {
                this._addpeople = value;
            }
        }

        public string conId
        {
            get
            {
                return this._conid;
            }
            set
            {
                this._conid = value;
            }
        }

        public string ID
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

        public string IEPid
        {
            get
            {
                return this._iepid;
            }
            set
            {
                this._iepid = value;
            }
        }

        public string infoNum
        {
            get
            {
                return this._infonum;
            }
            set
            {
                this._infonum = value;
            }
        }

        public decimal? Money
        {
            get
            {
                return this._money;
            }
            set
            {
                this._money = value;
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

        public int? type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}

