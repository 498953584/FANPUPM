namespace cn.justwin.VehiclesModel
{
    using System;

    [Serializable]
    public class TypeAndState
    {
        private string _code;
        private Guid _guid;
        private string _name;
        private Guid _parentguid;
        private int? _type;

        public string code
        {
            get
            {
                return this._code;
            }
            set
            {
                this._code = value;
            }
        }

        public Guid guid
        {
            get
            {
                return this._guid;
            }
            set
            {
                this._guid = value;
            }
        }

        public string Name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        public Guid ParentGuid
        {
            get
            {
                return this._parentguid;
            }
            set
            {
                this._parentguid = value;
            }
        }

        public int? Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}

