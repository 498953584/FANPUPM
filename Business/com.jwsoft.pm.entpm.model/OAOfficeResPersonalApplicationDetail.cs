namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OAOfficeResPersonalApplicationDetail
    {
        private int _applynumber;
        private string _ispass;
        private int _materialid;
        private int _padrecordid;
        private int _parecordid;

        public int ApplyNumber
        {
            get
            {
                return this._applynumber;
            }
            set
            {
                this._applynumber = value;
            }
        }

        public string IsPass
        {
            get
            {
                return this._ispass;
            }
            set
            {
                this._ispass = value;
            }
        }

        public int MaterialID
        {
            get
            {
                return this._materialid;
            }
            set
            {
                this._materialid = value;
            }
        }

        public int PADRecordID
        {
            get
            {
                return this._padrecordid;
            }
            set
            {
                this._padrecordid = value;
            }
        }

        public int PARecordID
        {
            get
            {
                return this._parecordid;
            }
            set
            {
                this._parecordid = value;
            }
        }
    }
}

