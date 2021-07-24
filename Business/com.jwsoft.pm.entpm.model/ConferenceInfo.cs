namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class ConferenceInfo
    {
        private string _Content;
        private string _EquipmentName;
        private string _IsValid;
        private int _MeetingRoomID;
        private string _Model;
        private int _Number;
        private int _RecordID;

        public string Content
        {
            get
            {
                return this._Content;
            }
            set
            {
                this._Content = value;
            }
        }

        public string EquipmentName
        {
            get
            {
                return this._EquipmentName;
            }
            set
            {
                this._EquipmentName = value;
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

        public int MeetingRoomID
        {
            get
            {
                return this._MeetingRoomID;
            }
            set
            {
                this._MeetingRoomID = value;
            }
        }

        public string Model
        {
            get
            {
                return this._Model;
            }
            set
            {
                this._Model = value;
            }
        }

        public int Number
        {
            get
            {
                return this._Number;
            }
            set
            {
                this._Number = value;
            }
        }

        public int RecordID
        {
            get
            {
                return this._RecordID;
            }
            set
            {
                this._RecordID = value;
            }
        }
    }
}

