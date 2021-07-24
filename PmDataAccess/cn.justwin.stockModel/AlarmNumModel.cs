namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class AlarmNumModel
    {
        private decimal _alarmnum;
        private string _said;
        private string _scode;
        private DateTime _settime;
        private string _tcode;
        private string infocode;

        public decimal AlarmNum
        {
            get
            {
                return this._alarmnum;
            }
            set
            {
                this._alarmnum = value;
            }
        }

        public string InfoCode
        {
            get
            {
                return this.infocode;
            }
            set
            {
                this.infocode = value;
            }
        }

        public string said
        {
            get
            {
                return this._said;
            }
            set
            {
                this._said = value;
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

        public DateTime settime
        {
            get
            {
                return this._settime;
            }
            set
            {
                this._settime = value;
            }
        }

        public string tcode
        {
            get
            {
                return this._tcode;
            }
            set
            {
                this._tcode = value;
            }
        }
    }
}

