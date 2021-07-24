namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class CorpTypeInfo
    {
        private string _CorpClass;
        private string _CorpClassCode;

        public string CorpClass
        {
            get
            {
                return this._CorpClass;
            }
            set
            {
                this._CorpClass = value;
            }
        }

        public string CorpClassCode
        {
            get
            {
                return this._CorpClassCode;
            }
            set
            {
                this._CorpClassCode = value;
            }
        }
    }
}

