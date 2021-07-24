namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class PTMultiClasses
    {
        private string _classcode;
        private int _classid;
        private string _classname;
        private string _classtypecode;
        private string _corpcode;
        private string _isvalid;
        private string _parentclasscode;
        private string _remark;

        public string ClassCode
        {
            get
            {
                return this._classcode;
            }
            set
            {
                this._classcode = value;
            }
        }

        public int ClassID
        {
            get
            {
                return this._classid;
            }
            set
            {
                this._classid = value;
            }
        }

        public string ClassName
        {
            get
            {
                return this._classname;
            }
            set
            {
                this._classname = value;
            }
        }

        public string ClassTypeCode
        {
            get
            {
                return this._classtypecode;
            }
            set
            {
                this._classtypecode = value;
            }
        }

        public string CorpCode
        {
            get
            {
                return this._corpcode;
            }
            set
            {
                this._corpcode = value;
            }
        }

        public string IsValid
        {
            get
            {
                return this._isvalid;
            }
            set
            {
                this._isvalid = value;
            }
        }

        public string ParentClassCode
        {
            get
            {
                return this._parentclasscode;
            }
            set
            {
                this._parentclasscode = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }
    }
}

