namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class CostInputPri
    {
        private DateTime _AuditDate;
        private string _AuditPeople;
        private int _AuditResult;
        private string _CostItemName;
        private string _FillPeople;
        private DateTime _HappenDate;
        private string _HappenUnit;
        private int _ID;
        private string _PrjCode;
        private Guid _RecordID;
        private string _Remark;
        private string _TouchMan;

        public DateTime AuditDate
        {
            get
            {
                return this._AuditDate;
            }
            set
            {
                this._AuditDate = value;
            }
        }

        public string AuditPeople
        {
            get
            {
                return this._AuditPeople;
            }
            set
            {
                this._AuditPeople = value;
            }
        }

        public int AuditResult
        {
            get
            {
                return this._AuditResult;
            }
            set
            {
                this._AuditResult = value;
            }
        }

        public string CostItemName
        {
            get
            {
                return this._CostItemName;
            }
            set
            {
                this._CostItemName = value;
            }
        }

        public string FillPeople
        {
            get
            {
                return this._FillPeople;
            }
            set
            {
                this._FillPeople = value;
            }
        }

        public DateTime HappenDate
        {
            get
            {
                return this._HappenDate;
            }
            set
            {
                this._HappenDate = value;
            }
        }

        public string HappenUnit
        {
            get
            {
                return this._HappenUnit;
            }
            set
            {
                this._HappenUnit = value;
            }
        }

        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this._ID = value;
            }
        }

        public string PrjCode
        {
            get
            {
                return this._PrjCode;
            }
            set
            {
                this._PrjCode = value;
            }
        }

        public Guid RecordID
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

        public string TouchMan
        {
            get
            {
                return this._TouchMan;
            }
            set
            {
                this._TouchMan = value;
            }
        }
    }
}

