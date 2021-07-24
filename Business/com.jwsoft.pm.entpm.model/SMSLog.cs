namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class SMSLog
    {
        private string _i_xgid = "";
        private int _id;
        private string _message;
        private string _receiveuser;
        private DateTime _sendtime;
        private string _senduser;
        private string _v_lxbm = "";

        public string I_XGID
        {
            get
            {
                return this._i_xgid;
            }
            set
            {
                this._i_xgid = value;
            }
        }

        public int ID
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

        public string Message
        {
            get
            {
                return this._message;
            }
            set
            {
                this._message = value;
            }
        }

        public string ReceiveUser
        {
            get
            {
                return this._receiveuser;
            }
            set
            {
                this._receiveuser = value;
            }
        }

        public DateTime SendTime
        {
            get
            {
                return this._sendtime;
            }
            set
            {
                this._sendtime = value;
            }
        }

        public string SendUser
        {
            get
            {
                return this._senduser;
            }
            set
            {
                this._senduser = value;
            }
        }

        public string V_LXBM
        {
            get
            {
                return this._v_lxbm;
            }
            set
            {
                this._v_lxbm = value;
            }
        }
    }
}

