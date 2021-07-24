namespace cn.justwin.contractModel
{
    using System;

    [Serializable]
    public class ConfigModel
    {
        private string _id;
        private string _notes;
        private string _paraname;
        private string _paravalue;

        public string ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string Notes
        {
            get
            {
                return this._notes;
            }
            set
            {
                this._notes = value;
            }
        }

        public string ParaName
        {
            get
            {
                return this._paraname;
            }
            set
            {
                this._paraname = value;
            }
        }

        public string ParaValue
        {
            get
            {
                return this._paravalue;
            }
            set
            {
                this._paravalue = value;
            }
        }
    }
}

