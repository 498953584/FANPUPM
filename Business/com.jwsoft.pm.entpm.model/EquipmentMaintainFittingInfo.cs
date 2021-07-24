namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class EquipmentMaintainFittingInfo
    {
        private string _FittingsName = "";
        private Guid _MaintainUniqueCode = Guid.Empty;
        private int _NoteSequenceID;
        private string _Operation = "";
        private int _Quality;
        private string _Remark = "";
        private string _Spec = "";

        public string FittingsName
        {
            get
            {
                return this._FittingsName;
            }
            set
            {
                this._FittingsName = value;
            }
        }

        public Guid MaintainUniqueCode
        {
            get
            {
                return this._MaintainUniqueCode;
            }
            set
            {
                this._MaintainUniqueCode = value;
            }
        }

        public int NoteSequenceID
        {
            get
            {
                return this._NoteSequenceID;
            }
            set
            {
                this._NoteSequenceID = value;
            }
        }

        public string Operation
        {
            get
            {
                return this._Operation;
            }
            set
            {
                this._Operation = value;
            }
        }

        public int Quality
        {
            get
            {
                return this._Quality;
            }
            set
            {
                this._Quality = value;
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

        public string Spec
        {
            get
            {
                return this._Spec;
            }
            set
            {
                this._Spec = value;
            }
        }
    }
}

