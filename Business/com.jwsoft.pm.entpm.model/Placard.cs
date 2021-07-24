namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class Placard
    {
        private DateTime _cutoffdate;
        private string _placardcontent;
        private int _placardno;
        private string _placardtitle;
        private DateTime _pushdate;
        private string _pushperson;

        public DateTime CutOffDate
        {
            get
            {
                return this._cutoffdate;
            }
            set
            {
                this._cutoffdate = value;
            }
        }

        public string PlaCardContent
        {
            get
            {
                return this._placardcontent;
            }
            set
            {
                this._placardcontent = value;
            }
        }

        public int PlaCardNo
        {
            get
            {
                return this._placardno;
            }
            set
            {
                this._placardno = value;
            }
        }

        public string PlaCardTitle
        {
            get
            {
                return this._placardtitle;
            }
            set
            {
                this._placardtitle = value;
            }
        }

        public DateTime PushDate
        {
            get
            {
                return this._pushdate;
            }
            set
            {
                this._pushdate = value;
            }
        }

        public string PushPerson
        {
            get
            {
                return this._pushperson;
            }
            set
            {
                this._pushperson = value;
            }
        }
    }
}

