namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class UnitType : IUnitType
    {
        private bool _IsValid = true;
        private int _UnitID;
        private string _UnitName = "";

        public bool IsValid
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

        public int UnitID
        {
            get
            {
                return this._UnitID;
            }
            set
            {
                this._UnitID = value;
            }
        }

        public string UnitName
        {
            get
            {
                return this._UnitName;
            }
            set
            {
                this._UnitName = value;
            }
        }
    }
}

