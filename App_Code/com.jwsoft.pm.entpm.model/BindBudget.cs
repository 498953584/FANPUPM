namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class BindBudget
    {
        private int _noteid;
        private Guid _prjcode;
        private string _taskcode;
        private string _templatescode;

        public int NoteId
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

        public Guid PrjCode
        {
            get
            {
                return this._prjcode;
            }
            set
            {
                this._prjcode = value;
            }
        }

        public string TaskCode
        {
            get
            {
                return this._taskcode;
            }
            set
            {
                this._taskcode = value;
            }
        }

        public string Templatescode
        {
            get
            {
                return this._templatescode;
            }
            set
            {
                this._templatescode = value;
            }
        }
    }
}

