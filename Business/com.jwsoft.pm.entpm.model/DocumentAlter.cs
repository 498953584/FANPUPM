namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class DocumentAlter
    {
        private DateTime _alterdate;
        private Guid _alternique;
        private string _alterpersoncode;
        private Guid _documentnique;
        private string _primarycontent;
        private string _primarytag;
        private string _primarytitle;

        public DateTime AlterDate
        {
            get
            {
                return this._alterdate;
            }
            set
            {
                this._alterdate = value;
            }
        }

        public Guid AlterNique
        {
            get
            {
                return this._alternique;
            }
            set
            {
                this._alternique = value;
            }
        }

        public string AlterPersonCode
        {
            get
            {
                return this._alterpersoncode;
            }
            set
            {
                this._alterpersoncode = value;
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

        public string PrimaryContent
        {
            get
            {
                return this._primarycontent;
            }
            set
            {
                this._primarycontent = value;
            }
        }

        public string PrimaryTag
        {
            get
            {
                return this._primarytag;
            }
            set
            {
                this._primarytag = value;
            }
        }

        public string PrimaryTitle
        {
            get
            {
                return this._primarytitle;
            }
            set
            {
                this._primarytitle = value;
            }
        }
    }
}

