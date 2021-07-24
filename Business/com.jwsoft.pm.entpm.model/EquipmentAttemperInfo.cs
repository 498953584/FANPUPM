namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class EquipmentAttemperInfo : BaseInfo
    {
        private string _AttemperBillCode;
        private DateTime _BeginDate;
        private DateTime _EndDate;
        private string _Equipmentname;
        private Guid _EquipmentUniqueCode;
        private int _IntendingTime;
        private int _NoteSequenceID;
        private string _Remark;
        private string _Suite;
        private Guid _ToProjectUniqueCode;
        private decimal _UnitPrice;

        public string AttemperBillCode
        {
            get
            {
                return this._AttemperBillCode;
            }
            set
            {
                this._AttemperBillCode = value;
            }
        }

        public DateTime BeginDate
        {
            get
            {
                return this._BeginDate;
            }
            set
            {
                this._BeginDate = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return this._EndDate;
            }
            set
            {
                this._EndDate = value;
            }
        }

        public string Equipmentname
        {
            get
            {
                return this._Equipmentname;
            }
            set
            {
                this._Equipmentname = value;
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

        public int IntendingTime
        {
            get
            {
                return this._IntendingTime;
            }
            set
            {
                this._IntendingTime = value;
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

        public string Suite
        {
            get
            {
                return this._Suite;
            }
            set
            {
                this._Suite = value;
            }
        }

        public Guid ToProjectUniqueCode
        {
            get
            {
                return this._ToProjectUniqueCode;
            }
            set
            {
                this._ToProjectUniqueCode = value;
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return this._UnitPrice;
            }
            set
            {
                this._UnitPrice = value;
            }
        }
    }
}

