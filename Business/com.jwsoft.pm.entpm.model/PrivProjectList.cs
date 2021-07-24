namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class PrivProjectList
    {
        private string _bussinesscode;
        private int _projectid;
        private string _usercode;

        public string BussinessCode
        {
            get
            {
                return this._bussinesscode;
            }
            set
            {
                this._bussinesscode = value;
            }
        }

        public int ProjectID
        {
            get
            {
                return this._projectid;
            }
            set
            {
                this._projectid = value;
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
    }
}

