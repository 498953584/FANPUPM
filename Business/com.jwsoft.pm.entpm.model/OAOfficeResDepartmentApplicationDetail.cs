namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OAOfficeResDepartmentApplicationDetail
    {
        private int _applynumber;
        private int _dadrecordid;
        private Guid _darecordid;
        private string _ispass;
        private int _materialid;

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

        public int DADRecordID
        {
            get
            {
                return this._dadrecordid;
            }
            set
            {
                this._dadrecordid = value;
            }
        }

        public Guid DARecordID
        {
            get
            {
                return this._darecordid;
            }
            set
            {
                this._darecordid = value;
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
    }
}

