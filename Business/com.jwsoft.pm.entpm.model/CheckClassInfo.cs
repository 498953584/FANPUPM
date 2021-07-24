namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class CheckClassInfo
    {
        private string _ItemInspectSortName;
        private string _Remark;
        private int _SortID;

        public string ItemInspectSortName
        {
            get
            {
                return this._ItemInspectSortName;
            }
            set
            {
                this._ItemInspectSortName = value;
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

        public int SortID
        {
            get
            {
                return this._SortID;
            }
            set
            {
                this._SortID = value;
            }
        }
    }
}

