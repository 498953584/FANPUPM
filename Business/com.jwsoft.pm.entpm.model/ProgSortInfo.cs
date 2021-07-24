namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class ProgSortInfo
    {
        private int _ProgSortCode;
        private string _ProgSortName;
        private string _Remark;

        public int ProgSortCode
        {
            get
            {
                return this._ProgSortCode;
            }
            set
            {
                this._ProgSortCode = value;
            }
        }

        public string ProgSortName
        {
            get
            {
                return this._ProgSortName;
            }
            set
            {
                this._ProgSortName = value;
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

