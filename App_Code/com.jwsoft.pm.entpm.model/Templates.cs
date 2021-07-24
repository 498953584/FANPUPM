namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class Templates
    {
        private string _parentcode;
        private string _templatescode;
        private string _templatesname;

        public string ParentCode
        {
            get
            {
                return this._parentcode;
            }
            set
            {
                this._parentcode = value;
            }
        }

        public string TemplatesCode
        {
            get
            {
                return this._templatescode;
            }
            set
            {
                this._templatescode = value;
            }
        }

        public string TemplatesName
        {
            get
            {
                return this._templatesname;
            }
            set
            {
                this._templatesname = value;
            }
        }
    }
}

