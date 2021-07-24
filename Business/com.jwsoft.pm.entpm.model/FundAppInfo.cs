namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class FundAppInfo
    {
        private string _auditstate;
        private string _content;
        private DateTime _fundappdate;
        private string _fundappuser;
        private decimal _fundnum;
        private Guid _guidflow;
        private int _id;
        private Guid _prjcode;

        public string AuditState
        {
            get
            {
                return this._auditstate;
            }
            set
            {
                this._auditstate = value;
            }
        }

        public string Content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
            }
        }

        public DateTime FundAppDate
        {
            get
            {
                return this._fundappdate;
            }
            set
            {
                this._fundappdate = value;
            }
        }

        public string FundAppUser
        {
            get
            {
                return this._fundappuser;
            }
            set
            {
                this._fundappuser = value;
            }
        }

        public decimal FundNum
        {
            get
            {
                return this._fundnum;
            }
            set
            {
                this._fundnum = value;
            }
        }

        public Guid GuidFlow
        {
            get
            {
                return this._guidflow;
            }
            set
            {
                this._guidflow = value;
            }
        }

        public int id
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

        public Guid PrjCode
        {
            get
            {
                return this._prjcode;
            }
            set
            {
                this._prjcode = value;
            }
        }
    }
}

