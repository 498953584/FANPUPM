namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class CodeLibTypeInfo
    {
        private int _ID;
        private string _Name;
        private string _Remark;

        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
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
    }
}

