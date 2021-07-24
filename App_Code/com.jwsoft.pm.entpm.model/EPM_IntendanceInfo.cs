namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class EPM_IntendanceInfo
    {
        private DateTime? _askquestionsdate;
        private string _askquestionsyhdm;
        private Guid _intendanceguid;
        private Guid _noteid;
        private string _questionexplain;
        private int? _questiontag;
        private string _sdate;
        private DateTime? _settledate;
        private string _settleexplain;
        private string _settletoperson;
        private string _settleyhdm;
        private string _tocause;

        public DateTime? AskQuestionsDate
        {
            get
            {
                return this._askquestionsdate;
            }
            set
            {
                this._askquestionsdate = value;
            }
        }

        public string AskQuestionsYhdm
        {
            get
            {
                return this._askquestionsyhdm;
            }
            set
            {
                this._askquestionsyhdm = value;
            }
        }

        public Guid IntendanceGuid
        {
            get
            {
                return this._intendanceguid;
            }
            set
            {
                this._intendanceguid = value;
            }
        }

        public Guid NoteId
        {
            get
            {
                return this._noteid;
            }
            set
            {
                this._noteid = value;
            }
        }

        public string QuestionExplain
        {
            get
            {
                return this._questionexplain;
            }
            set
            {
                this._questionexplain = value;
            }
        }

        public int? QuestionTag
        {
            get
            {
                return this._questiontag;
            }
            set
            {
                this._questiontag = value;
            }
        }

        public string Sdate
        {
            get
            {
                return this._sdate;
            }
            set
            {
                this._sdate = value;
            }
        }

        public DateTime? SettleDate
        {
            get
            {
                return this._settledate;
            }
            set
            {
                this._settledate = value;
            }
        }

        public string SettleExplain
        {
            get
            {
                return this._settleexplain;
            }
            set
            {
                this._settleexplain = value;
            }
        }

        public string SettleToPerson
        {
            get
            {
                return this._settletoperson;
            }
            set
            {
                this._settletoperson = value;
            }
        }

        public string SettleYhdm
        {
            get
            {
                return this._settleyhdm;
            }
            set
            {
                this._settleyhdm = value;
            }
        }

        public string ToCause
        {
            get
            {
                return this._tocause;
            }
            set
            {
                this._tocause = value;
            }
        }
    }
}

