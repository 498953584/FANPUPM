namespace cn.justwin.opm.Complete
{
    using System;

    public class BusinessTypeModel
    {
        private string _fID;
        private string _tableName;
        private string _typeCode;
        private int _typeId;
        private string _typeName;

        public string FID
        {
            get
            {
                return this._fID;
            }
            set
            {
                this._fID = value;
            }
        }

        public string TableName
        {
            get
            {
                return this._tableName;
            }
            set
            {
                this._tableName = value;
            }
        }

        public string TypeCode
        {
            get
            {
                return this._typeCode;
            }
            set
            {
                this._typeCode = value;
            }
        }

        public int TypeId
        {
            get
            {
                return this._typeId;
            }
            set
            {
                this._typeId = value;
            }
        }

        public string TypeName
        {
            get
            {
                return this._typeName;
            }
            set
            {
                this._typeName = value;
            }
        }
    }
}

