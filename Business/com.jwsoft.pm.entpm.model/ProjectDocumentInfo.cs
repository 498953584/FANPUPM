namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ProjectDocumentInfo
    {
        private DateTime _Adddate = DateTime.Today;
        private int _ClassCode;
        private Guid _DocumentCode = Guid.Empty;
        private string _DocumentName = "";
        private string _Filecode = "";
        private Guid _ProjectCode = Guid.Empty;
        private string _Remark = "";
        private string _RollCode = "";
        private int _State;
        private string _Storage = "";

        public DateTime AddDate
        {
            get
            {
                return this._Adddate;
            }
            set
            {
                this._Adddate = value;
            }
        }

        public int ClassCode
        {
            get
            {
                return this._ClassCode;
            }
            set
            {
                this._ClassCode = value;
            }
        }

        public Guid DocumentCode
        {
            get
            {
                return this._DocumentCode;
            }
            set
            {
                this._DocumentCode = value;
            }
        }

        public string DocumentName
        {
            get
            {
                return this._DocumentName;
            }
            set
            {
                this._DocumentName = value;
            }
        }

        public string FileCode
        {
            get
            {
                return this._Filecode;
            }
            set
            {
                this._Filecode = value;
            }
        }

        public Guid ProjectCode
        {
            get
            {
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        public string RollCode
        {
            get
            {
                return this._RollCode;
            }
            set
            {
                this._RollCode = value;
            }
        }

        public int State
        {
            get
            {
                return this._State;
            }
            set
            {
                this._State = value;
            }
        }

        public string Storage
        {
            get
            {
                return this._Storage;
            }
            set
            {
                this._Storage = value;
            }
        }
    }
}

