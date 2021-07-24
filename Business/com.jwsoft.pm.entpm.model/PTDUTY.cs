namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Runtime.CompilerServices;

    public class PTDUTY
    {
        private string _c_sfyx;
        private string _dutycode;
        private int _dutynumber;
        private int _i_bmdm;
        private int _i_dutyid;
        private string _remark;
        private string _roletypecode;

        public string c_sfyx
        {
            get
            {
                return this._c_sfyx;
            }
            set
            {
                this._c_sfyx = value;
            }
        }

        public string DutyCode
        {
            get
            {
                return this._dutycode;
            }
            set
            {
                this._dutycode = value;
            }
        }

        public string DutyName { get; set; }

        public int DutyNumber
        {
            get
            {
                return this._dutynumber;
            }
            set
            {
                this._dutynumber = value;
            }
        }

        public int i_bmdm
        {
            get
            {
                return this._i_bmdm;
            }
            set
            {
                this._i_bmdm = value;
            }
        }

        public int I_DUTYID
        {
            get
            {
                return this._i_dutyid;
            }
            set
            {
                this._i_dutyid = value;
            }
        }

        public string Remark
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

        public string RoleTypeCode
        {
            get
            {
                return this._roletypecode;
            }
            set
            {
                this._roletypecode = value;
            }
        }
    }
}

