namespace cn.justwin.stockModel
{
    using System;

    [Serializable]
    public class sm_receiveNote
    {
        private string _explain;
        private string _remark;
        private string _rncode;
        private string _rnid;
        private DateTime? _rntime;
        private string _rnuser;
        private string _sAllocationId;
        private string _snid;
        private string _soID;
        private string _stId;

        public string Explain
        {
            get
            {
                return this._explain;
            }
            set
            {
                this._explain = value;
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

        public string rnCode
        {
            get
            {
                return this._rncode;
            }
            set
            {
                this._rncode = value;
            }
        }

        public string rnId
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

        public DateTime? rnTime
        {
            get
            {
                return this._rntime;
            }
            set
            {
                this._rntime = value;
            }
        }

        public string rnUser
        {
            get
            {
                return this._rnuser;
            }
            set
            {
                this._rnuser = value;
            }
        }

        public string SAllocationId
        {
            get
            {
                return this._sAllocationId;
            }
            set
            {
                this._sAllocationId = value;
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

        public string soId
        {
            get
            {
                return this._soID;
            }
            set
            {
                this._soID = value;
            }
        }

        public string stId
        {
            get
            {
                return this._stId;
            }
            set
            {
                this._stId = value;
            }
        }
    }
}

