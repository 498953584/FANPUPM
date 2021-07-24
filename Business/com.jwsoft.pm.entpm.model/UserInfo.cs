namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class UserInfo
    {
        private string _departID;
        private string _departName;
        private string _loginID;
        private string _loginPwd;
        private string _userCode;
        private string _userName;
        private string _workArea;

        public string UserCode
        {
            get
            {
                return this._userCode;
            }
            set
            {
                this._userCode = value;
            }
        }

        public string UserDepartCode
        {
            get
            {
                return this._departID;
            }
            set
            {
                this._departID = value;
            }
        }

        public string UserDepartName
        {
            get
            {
                return this._departName;
            }
            set
            {
                this._departName = value;
            }
        }

        public string UserLoginID
        {
            get
            {
                return this._loginID;
            }
            set
            {
                this._loginID = value;
            }
        }

        public string UserLoginPwd
        {
            get
            {
                return this._loginPwd;
            }
            set
            {
                this._loginPwd = value;
            }
        }

        public string UserName
        {
            get
            {
                return this._userName;
            }
            set
            {
                this._userName = value;
            }
        }

        public string UserWorkArea
        {
            get
            {
                return this._workArea;
            }
            set
            {
                this._workArea = value;
            }
        }
    }
}

