namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class CheckInfo
    {
        private string _AcceptCheckAnswerForPerson;
        private string _AcceptCheckContent;
        private DateTime _AcceptCheckDate;
        private string _AcceptCheckGist;
        private string _AcceptCheckResult;
        private int _AcceptCheckSort;
        private string _AcceptCheckUnit;
        private int _CertifiResult;
        private int _CheckResult;
        private int _CheckResults;
        private string _CompleteApplyContent;
        private DateTime _ExamineApproveDate;
        private string _ExamineApproveIdea;
        private string _ExamineApprovePerson;
        private int _ExamineApproveResult;
        private string _ExamineUnit;
        private DateTime _factfinishtime = DateTime.Today;
        private string _factresult;
        private int _Flags;
        private int _ID;
        private bool _IsRectified;
        private DateTime _planfinishtime = DateTime.Today;
        private string _PrjCode;
        private string _prjplan;
        private string _Remark;
        private DateTime _requestfinishtime = DateTime.Today;
        private int filesType;
        private int mark;
        private string rectifyMarkID;

        public string AcceptCheckAnswerForPerson
        {
            get
            {
                return this._AcceptCheckAnswerForPerson;
            }
            set
            {
                this._AcceptCheckAnswerForPerson = value;
            }
        }

        public string AcceptCheckContent
        {
            get
            {
                return this._AcceptCheckContent;
            }
            set
            {
                this._AcceptCheckContent = value;
            }
        }

        public DateTime AcceptCheckDate
        {
            get
            {
                return this._AcceptCheckDate;
            }
            set
            {
                this._AcceptCheckDate = value;
            }
        }

        public string AcceptCheckGist
        {
            get
            {
                return this._AcceptCheckGist;
            }
            set
            {
                this._AcceptCheckGist = value;
            }
        }

        public string AcceptCheckResult
        {
            get
            {
                return this._AcceptCheckResult;
            }
            set
            {
                this._AcceptCheckResult = value;
            }
        }

        public int AcceptCheckSort
        {
            get
            {
                return this._AcceptCheckSort;
            }
            set
            {
                this._AcceptCheckSort = value;
            }
        }

        public string AcceptCheckUnit
        {
            get
            {
                return this._AcceptCheckUnit;
            }
            set
            {
                this._AcceptCheckUnit = value;
            }
        }

        public int CertifiResult
        {
            get
            {
                return this._CertifiResult;
            }
            set
            {
                this._CertifiResult = value;
            }
        }

        public int CheckResult
        {
            get
            {
                return this._CheckResult;
            }
            set
            {
                this._CheckResult = value;
            }
        }

        public int checkResults
        {
            get
            {
                return this._CheckResults;
            }
            set
            {
                this._CheckResults = value;
            }
        }

        public string CompleteApplyContent
        {
            get
            {
                return this._CompleteApplyContent;
            }
            set
            {
                this._CompleteApplyContent = value;
            }
        }

        public DateTime ExamineApproveDate
        {
            get
            {
                return this._ExamineApproveDate;
            }
            set
            {
                this._ExamineApproveDate = value;
            }
        }

        public string ExamineApproveIdea
        {
            get
            {
                return this._ExamineApproveIdea;
            }
            set
            {
                this._ExamineApproveIdea = value;
            }
        }

        public string ExamineApprovePerson
        {
            get
            {
                return this._ExamineApprovePerson;
            }
            set
            {
                this._ExamineApprovePerson = value;
            }
        }

        public int ExamineApproveResult
        {
            get
            {
                return this._ExamineApproveResult;
            }
            set
            {
                this._ExamineApproveResult = value;
            }
        }

        public string ExamineUnit
        {
            get
            {
                return this._ExamineUnit;
            }
            set
            {
                this._ExamineUnit = value;
            }
        }

        public DateTime factfinishtime
        {
            get
            {
                return this._factfinishtime;
            }
            set
            {
                this._factfinishtime = value;
            }
        }

        public string factresult
        {
            get
            {
                return this._factresult;
            }
            set
            {
                this._factresult = value;
            }
        }

        public int FilesType
        {
            get
            {
                return this.filesType;
            }
            set
            {
                this.filesType = value;
            }
        }

        public int Flags
        {
            get
            {
                return this._Flags;
            }
            set
            {
                this._Flags = value;
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

        public bool IsRectified
        {
            get
            {
                return this._IsRectified;
            }
            set
            {
                this._IsRectified = value;
            }
        }

        public int Mark
        {
            get
            {
                return this.mark;
            }
            set
            {
                this.mark = value;
            }
        }

        public DateTime planfinishtime
        {
            get
            {
                return this._planfinishtime;
            }
            set
            {
                this._planfinishtime = value;
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

        public string prjplan
        {
            get
            {
                return this._prjplan;
            }
            set
            {
                this._prjplan = value;
            }
        }

        public string RectifyMarkID
        {
            get
            {
                return this.rectifyMarkID;
            }
            set
            {
                this.rectifyMarkID = value;
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

        public DateTime requestfinishtime
        {
            get
            {
                return this._requestfinishtime;
            }
            set
            {
                this._requestfinishtime = value;
            }
        }
    }
}

