namespace cn.justwin.fileInfoModel
{
    using System;

    [Serializable]
    public class FileTypeModel
    {
        private string _typeid;
        private string _typeimg;
        private string _typename;
        private string _typesuffix;

        public string TypeId
        {
            get
            {
                return this._typeid;
            }
            set
            {
                this._typeid = value;
            }
        }

        public string TypeImg
        {
            get
            {
                return this._typeimg;
            }
            set
            {
                this._typeimg = value;
            }
        }

        public string TypeName
        {
            get
            {
                return this._typename;
            }
            set
            {
                this._typename = value;
            }
        }

        public string TypeSuffix
        {
            get
            {
                return this._typesuffix;
            }
            set
            {
                this._typesuffix = value;
            }
        }
    }
}

