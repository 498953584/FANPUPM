namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class KnowledgeDataModel
    {
        private DateTime _AddDate = DateTime.Now;
        private DateTime _AffirmDateTime = DateTime.Now;
        private string _AffirmInfo = "";
        private DateTime _AuditingDate = DateTime.Now;
        private string _Author = "";
        private string _DatumName = "";
        private string _IsAuditing = "";
        private string _IsValid = "";
        private Guid _KnowledgeCode = new Guid();
        private Guid _ProjectCode = new Guid();
        private int _TypeId;
        private DateTime _UpdateDate = DateTime.Now;
        private string _Viscera = "";

        public DateTime AddDate
        {
            get
            {
                return this._AddDate;
            }
            set
            {
                this._AddDate = value;
            }
        }

        public DateTime AffirmDateTime
        {
            get
            {
                return this._AffirmDateTime;
            }
            set
            {
                this._AffirmDateTime = value;
            }
        }

        public string AffirmInfo
        {
            get
            {
                return this._AffirmInfo;
            }
            set
            {
                this._AffirmInfo = value;
            }
        }

        public DateTime AuditingDate
        {
            get
            {
                return this._AuditingDate;
            }
            set
            {
                this._AuditingDate = value;
            }
        }

        public string Author
        {
            get
            {
                return this._Author;
            }
            set
            {
                this._Author = value;
            }
        }

        public string DatumName
        {
            get
            {
                return this._DatumName;
            }
            set
            {
                this._DatumName = value;
            }
        }

        public string IsAuditing
        {
            get
            {
                return this._IsAuditing;
            }
            set
            {
                this._IsAuditing = value;
            }
        }

        public string IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                this._IsValid = value;
            }
        }

        public Guid KnowledgeCode
        {
            get
            {
                return this._KnowledgeCode;
            }
            set
            {
                this._KnowledgeCode = value;
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

        public int TypeId
        {
            get
            {
                return this._TypeId;
            }
            set
            {
                this._TypeId = value;
            }
        }

        public DateTime UpdateDate
        {
            get
            {
                return this._UpdateDate;
            }
            set
            {
                this._UpdateDate = value;
            }
        }

        public string Viscera
        {
            get
            {
                return this._Viscera;
            }
            set
            {
                this._Viscera = value;
            }
        }
    }
}

