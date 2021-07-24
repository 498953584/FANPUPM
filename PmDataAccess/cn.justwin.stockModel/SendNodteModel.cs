namespace cn.justwin.stockModel
{
    using System;

    public class SendNodteModel
    {
        private string _limits;
        private Guid _prjcode;
        private string _remark;
        private int? _sendstate;
        private DateTime? _snaddtime;
        private string _snadduser;
        private string _sncode;
        private string _snid;

        public string Limits
        {
            get
            {
                return this._limits;
            }
            set
            {
                this._limits = value;
            }
        }

        public Guid prjCode
        {
            get
            {
                return this._prjcode;
            }
            set
            {
                this._prjcode = value;
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

        public int? sendState
        {
            get
            {
                return this._sendstate;
            }
            set
            {
                this._sendstate = value;
            }
        }

        public DateTime? snAddTime
        {
            get
            {
                return this._snaddtime;
            }
            set
            {
                this._snaddtime = value;
            }
        }

        public string snAddUser
        {
            get
            {
                return this._snadduser;
            }
            set
            {
                this._snadduser = value;
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

        public string snId
        {
            get
            {
                return this._snid;
            }
            set
            {
                this._snid = value;
            }
        }
    }
}

