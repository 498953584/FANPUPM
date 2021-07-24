namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class IntendanceMaster
    {
        private DateTime _BookInDate;
        private Guid _IntendanceGuid;
        private string _OpYhdm;
        private Guid _PrjGuid;
        private int _QuestionTag;
        private string _QuestionTitle;
        private int _QuestionTypeId;
        private int _SettleState;
        private string _SettleToPerson;
        private string _SettleYhdm;

        public DateTime BookInDate
        {
            get
            {
                return this._BookInDate;
            }
            set
            {
                this._BookInDate = value;
            }
        }

        public Guid IntendanceGuid
        {
            get
            {
                return this._IntendanceGuid;
            }
            set
            {
                this._IntendanceGuid = value;
            }
        }

        public string OpYhdm
        {
            get
            {
                return this._OpYhdm;
            }
            set
            {
                this._OpYhdm = value;
            }
        }

        public Guid PrjGuid
        {
            get
            {
                return this._PrjGuid;
            }
            set
            {
                this._PrjGuid = value;
            }
        }

        public int QuestionTag
        {
            get
            {
                return this._QuestionTag;
            }
            set
            {
                this._QuestionTag = value;
            }
        }

        public string QuestionTitle
        {
            get
            {
                return this._QuestionTitle;
            }
            set
            {
                this._QuestionTitle = value;
            }
        }

        public int QuestionTypeId
        {
            get
            {
                return this._QuestionTypeId;
            }
            set
            {
                this._QuestionTypeId = value;
            }
        }

        public int SettleState
        {
            get
            {
                return this._SettleState;
            }
            set
            {
                this._SettleState = value;
            }
        }

        public string SettleToPerson
        {
            get
            {
                return this._SettleToPerson;
            }
            set
            {
                this._SettleToPerson = value;
            }
        }

        public string SettleYhdm
        {
            get
            {
                return this._SettleYhdm;
            }
            set
            {
                this._SettleYhdm = value;
            }
        }
    }
}

