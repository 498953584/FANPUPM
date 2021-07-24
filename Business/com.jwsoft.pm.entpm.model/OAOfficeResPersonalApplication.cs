namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OAOfficeResPersonalApplication
    {
        private Guid _acrecordid;
        private DateTime _applydate;
        private string _applyman;
        private string _corpcode;
        private string _iscomplete;
        private string _issubmit;
        private int _parecordid;
        private DateTime _recorddate;
        private string _useman;
        private string _usercode;

        public Guid ACRecordID
        {
            get
            {
                return this._acrecordid;
            }
            set
            {
                this._acrecordid = value;
            }
        }

        public DateTime ApplyDate
        {
            get
            {
                return this._applydate;
            }
            set
            {
                this._applydate = value;
            }
        }

        public string ApplyMan
        {
            get
            {
                return this._applyman;
            }
            set
            {
                this._applyman = value;
            }
        }

        public string CorpCode
        {
            get
            {
                return this._corpcode;
            }
            set
            {
                this._corpcode = value;
            }
        }

        public string IsComplete
        {
            get
            {
                return this._iscomplete;
            }
            set
            {
                this._iscomplete = value;
            }
        }

        public string IsSubmit
        {
            get
            {
                return this._issubmit;
            }
            set
            {
                this._issubmit = value;
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

        public DateTime RecordDate
        {
            get
            {
                return this._recorddate;
            }
            set
            {
                this._recorddate = value;
            }
        }

        public string UseMan
        {
            get
            {
                return this._useman;
            }
            set
            {
                this._useman = value;
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

