namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class DepositoryClass
    {
        private string _classcode;
        private string _classname;
        private int _depoclassid;
        private int _i_childnum;
        private string _isvalid;

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

        public int DepoClassID
        {
            get
            {
                return this._depoclassid;
            }
            set
            {
                this._depoclassid = value;
            }
        }

        public int i_ChildNum
        {
            get
            {
                return this._i_childnum;
            }
            set
            {
                this._i_childnum = value;
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
    }
}

