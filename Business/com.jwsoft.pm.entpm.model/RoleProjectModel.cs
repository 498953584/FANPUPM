namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class RoleProjectModel
    {
        private string _projectcodes;
        private int? _roleid;
        private int _roleprojectid;
        private string _usercode;

        public string ProjectCodes
        {
            get
            {
                return this._projectcodes;
            }
            set
            {
                this._projectcodes = value;
            }
        }

        public int? RoleID
        {
            get
            {
                return this._roleid;
            }
            set
            {
                this._roleid = value;
            }
        }

        public int RoleProjectID
        {
            get
            {
                return this._roleprojectid;
            }
            set
            {
                this._roleprojectid = value;
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

