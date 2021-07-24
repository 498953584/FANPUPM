namespace cn.justwin.stockModel
{
    using System;

    public class ResourceType
    {
        private DateTime? _inputdate;
        private string _inputuser;
        private bool _isvalid;
        private string _parentid;
        private string _resourcetypecode;
        private string _resourcetypeid;
        private string _resourcetypename;

        public DateTime? InputDate
        {
            get
            {
                return this._inputdate;
            }
            set
            {
                this._inputdate = value;
            }
        }

        public string InputUser
        {
            get
            {
                return this._inputuser;
            }
            set
            {
                this._inputuser = value;
            }
        }

        public bool IsValid
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

        public string ParentId
        {
            get
            {
                return this._parentid;
            }
            set
            {
                this._parentid = value;
            }
        }

        public string ResourceTypeCode
        {
            get
            {
                return this._resourcetypecode;
            }
            set
            {
                this._resourcetypecode = value;
            }
        }

        public string ResourceTypeId
        {
            get
            {
                return this._resourcetypeid;
            }
            set
            {
                this._resourcetypeid = value;
            }
        }

        public string ResourceTypeName
        {
            get
            {
                return this._resourcetypename;
            }
            set
            {
                this._resourcetypename = value;
            }
        }
    }
}

