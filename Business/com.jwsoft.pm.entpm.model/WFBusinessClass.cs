namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class WFBusinessClass
    {
        private string _businessclass;
        private string _businessclassname;
        private string _businesscode;

        public string BusinessClass
        {
            get
            {
                return this._businessclass;
            }
            set
            {
                this._businessclass = value;
            }
        }

        public string BusinessClassName
        {
            get
            {
                return this._businessclassname;
            }
            set
            {
                this._businessclassname = value;
            }
        }

        public string BusinessCode
        {
            get
            {
                return this._businesscode;
            }
            set
            {
                this._businesscode = value;
            }
        }
    }
}

