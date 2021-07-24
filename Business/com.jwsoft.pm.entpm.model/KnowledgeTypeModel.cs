namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class KnowledgeTypeModel
    {
        private string _isDelete = "";
        private string _IsFixup = "";
        private string _IsValid = "";
        private string _IsVisible = "";
        private int _ParentId;
        private string _Remark = "";
        private int _TypeId;
        private string _TypeName = "";

        public string isDelete
        {
            get
            {
                return this._isDelete;
            }
            set
            {
                this._isDelete = value;
            }
        }

        public string IsFixup
        {
            get
            {
                return this._IsFixup;
            }
            set
            {
                this._IsFixup = value;
            }
        }

        public string IsValid
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

        public string IsVisible
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

        public int ParentId
        {
            get
            {
                return this._ParentId;
            }
            set
            {
                this._ParentId = value;
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

        public int TypeId
        {
            get
            {
                return this._TypeId;
            }
            set
            {
                this._TypeId = value;
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

