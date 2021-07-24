namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class UserPointValue
    {
        private Guid _communionnique;
        private Guid _documentnique;
        private string _personcode;
        private decimal _pointvalue;
        private int _recodeindex;

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

        public string PersonCode
        {
            get
            {
                return this._personcode;
            }
            set
            {
                this._personcode = value;
            }
        }

        public decimal PointValue
        {
            get
            {
                return this._pointvalue;
            }
            set
            {
                this._pointvalue = value;
            }
        }

        public int RecodeIndex
        {
            get
            {
                return this._recodeindex;
            }
            set
            {
                this._recodeindex = value;
            }
        }
    }
}

