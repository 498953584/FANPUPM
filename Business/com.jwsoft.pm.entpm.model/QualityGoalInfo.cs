namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class QualityGoalInfo
    {
        private int _filesType;
        private int _i_id;
        private int _mark;
        private Guid _PrjCode = Guid.Empty;
        private string _QualityGoal;
        private string _Remark;
        private string _ScheduleCode;
        private string _ScheduleName;

        public int FilesType
        {
            get
            {
                return this._filesType;
            }
            set
            {
                this._filesType = value;
            }
        }

        public int i_id
        {
            get
            {
                return this._i_id;
            }
            set
            {
                this._i_id = value;
            }
        }

        public int Mark
        {
            get
            {
                return this._mark;
            }
            set
            {
                this._mark = value;
            }
        }

        public Guid PrjCode
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

        public string QualityGoal
        {
            get
            {
                return this._QualityGoal;
            }
            set
            {
                this._QualityGoal = value;
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

        public string ScheduleCode
        {
            get
            {
                return this._ScheduleCode;
            }
            set
            {
                this._ScheduleCode = value;
            }
        }

        public string ScheduleName
        {
            get
            {
                return this._ScheduleName;
            }
            set
            {
                this._ScheduleName = value;
            }
        }
    }
}

