namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class CodingType
    {
        private bool _IsValid = true;
        private bool _IsVisible = true;
        private string _Remark = "";
        private string _SignCode = "";
        private int _TypeID;
        private string _TypeName = "";

        public bool IsValid
        {
            get
            {
                return this._IsValid;
            }
            set
            {
                this._IsValid = value;
            }
        }

        public bool IsVisible
        {
            get
            {
                return this._IsVisible;
            }
            set
            {
                this._IsVisible = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._Remark;
            }
            set
            {
                this._Remark = value;
            }
        }

        public string SignCode
        {
            get
            {
                return this._SignCode;
            }
            set
            {
                this._SignCode = value;
            }
        }

        public int TypeID
        {
            get
            {
                return this._TypeID;
            }
            set
            {
                this._TypeID = value;
            }
        }

        public string TypeName
        {
            get
            {
                return this._TypeName;
            }
            set
            {
                this._TypeName = value;
            }
        }
    }
}

