namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class EquipmentCheckInfo : BaseInfo
    {
        private string _CheckBillCode = "";
        private DateTime _CheckDate = DateTime.MinValue;
        private string _CheckDept = "";
        private string _CheckPerson = "";
        private Guid _EquipmentUniqueCode = Guid.Empty;
        private int _InOrOut;
        private int _IsLast;
        private int _NoteSequenceID;
        private string _Remark = "";
        private int _Result;
        private string _ResultDescript = "";

        public string CheckBillCode
        {
            get
            {
                return this._CheckBillCode;
            }
            set
            {
                this._CheckBillCode = value;
            }
        }

        public DateTime CheckDate
        {
            get
            {
                return this._CheckDate;
            }
            set
            {
                this._CheckDate = value;
            }
        }

        public string CheckDept
        {
            get
            {
                return this._CheckDept;
            }
            set
            {
                this._CheckDept = value;
            }
        }

        public string CheckPerson
        {
            get
            {
                return this._CheckPerson;
            }
            set
            {
                this._CheckPerson = value;
            }
        }

        public Guid EquipmentUniqueCode
        {
            get
            {
                return this._EquipmentUniqueCode;
            }
            set
            {
                this._EquipmentUniqueCode = value;
            }
        }

        public int InOrOut
        {
            get
            {
                return this._InOrOut;
            }
            set
            {
                this._InOrOut = value;
            }
        }

        public int IsLast
        {
            get
            {
                return this._IsLast;
            }
            set
            {
                this._IsLast = value;
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

        public int Result
        {
            get
            {
                return this._Result;
            }
            set
            {
                this._Result = value;
            }
        }

        public string ResultDescript
        {
            get
            {
                return this._ResultDescript;
            }
            set
            {
                this._ResultDescript = value;
            }
        }
    }
}

