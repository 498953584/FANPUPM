namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class AnnexInfo : AnnexBaseInfo
    {
        private int _AnnexType;
        private int _ModuleID;
        private string _RecordCode = "";

        public int AnnexType
        {
            get
            {
                return this._AnnexType;
            }
            set
            {
                this._AnnexType = value;
            }
        }

        public int ModuleID
        {
            get
            {
                return this._ModuleID;
            }
            set
            {
                this._ModuleID = value;
            }
        }

        public string RecordCode
        {
            get
            {
                return this._RecordCode;
            }
            set
            {
                this._RecordCode = value;
            }
        }
    }
}

