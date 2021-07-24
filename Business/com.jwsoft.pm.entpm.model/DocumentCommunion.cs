namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class DocumentCommunion
    {
        private string _communioncontent;
        private DateTime _communiondate;
        private Guid _communionnique;
        private string _communionperson;
        private Guid _documentnique;

        public string CommunionContent
        {
            get
            {
                return this._communioncontent;
            }
            set
            {
                this._communioncontent = value;
            }
        }

        public DateTime CommunionDate
        {
            get
            {
                return this._communiondate;
            }
            set
            {
                this._communiondate = value;
            }
        }

        public Guid CommunionNique
        {
            get
            {
                return this._communionnique;
            }
            set
            {
                this._communionnique = value;
            }
        }

        public string CommunionPerson
        {
            get
            {
                return this._communionperson;
            }
            set
            {
                this._communionperson = value;
            }
        }

        public Guid DocumentNique
        {
            get
            {
                return this._documentnique;
            }
            set
            {
                this._documentnique = value;
            }
        }
    }
}

