namespace cn.justwin.opm.model
{
    using System;

    public class ExceptionInfoModel
    {
        private string _exceptionaddr;
        private int _exceptionid;
        private string _exceptionmessage;
        private string _exceptionsource;
        private DateTime _exceptiontime;
        private string _exceptiontrace;
        private string _submitdatas;
        private string _urlparams;
        private string _usercode;
        private string _userip;

        public string ExceptionAddr
        {
            get
            {
                return this._exceptionaddr;
            }
            set
            {
                this._exceptionaddr = value;
            }
        }

        public int ExceptionID
        {
            get
            {
                return this._exceptionid;
            }
            set
            {
                this._exceptionid = value;
            }
        }

        public string ExceptionMessage
        {
            get
            {
                return this._exceptionmessage;
            }
            set
            {
                this._exceptionmessage = value;
            }
        }

        public string ExceptionSource
        {
            get
            {
                return this._exceptionsource;
            }
            set
            {
                this._exceptionsource = value;
            }
        }

        public DateTime ExceptionTime
        {
            get
            {
                return this._exceptiontime;
            }
            set
            {
                this._exceptiontime = value;
            }
        }

        public string ExceptionTrace
        {
            get
            {
                return this._exceptiontrace;
            }
            set
            {
                this._exceptiontrace = value;
            }
        }

        public string SubmitDatas
        {
            get
            {
                return this._submitdatas;
            }
            set
            {
                this._submitdatas = value;
            }
        }

        public string UrlParams
        {
            get
            {
                return this._urlparams;
            }
            set
            {
                this._urlparams = value;
            }
        }

        public string UserCode
        {
            get
            {
                return this._usercode;
            }
            set
            {
                this._usercode = value;
            }
        }

        public string UserIP
        {
            get
            {
                return this._userip;
            }
            set
            {
                this._userip = value;
            }
        }
    }
}

