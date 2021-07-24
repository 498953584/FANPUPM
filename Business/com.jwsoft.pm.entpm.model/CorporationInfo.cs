namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class CorporationInfo
    {
        private int _CorpID;
        private string _Corporation;
        private string _CorporationName;

        public int CorpID
        {
            get
            {
                return this._CorpID;
            }
            set
            {
                this._CorpID = value;
            }
        }

        public string Corporation
        {
            get
            {
                return this._Corporation;
            }
            set
            {
                this._Corporation = value;
            }
        }

        public string CorporationName
        {
            get
            {
                return this._CorporationName;
            }
            set
            {
                this._CorporationName = value;
            }
        }
    }
}

