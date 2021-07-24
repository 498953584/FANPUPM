namespace com.jwsoft.pm.entpm.model
{
    using System;

    [Serializable]
    public class TaskRelation
    {
        private string _BeginTaskCode = string.Empty;
        private string _EndTaskCode = string.Empty;
        private int _NoteID;
        private Guid _ProjectCode = Guid.Empty;
        private int _Relationship = -2147483648;
        private int _WaitDay = -2147483648;

        public string BeginTaskCode
        {
            get
            {
                return this._BeginTaskCode;
            }
            set
            {
                this._BeginTaskCode = value;
            }
        }

        public string EndTaskCode
        {
            get
            {
                return this._EndTaskCode;
            }
            set
            {
                this._EndTaskCode = value;
            }
        }

        public int NoteID
        {
            get
            {
                return this._NoteID;
            }
            set
            {
                this._NoteID = value;
            }
        }

        public Guid ProjectCode
        {
            get
            {
                return this._ProjectCode;
            }
            set
            {
                this._ProjectCode = value;
            }
        }

        public int Relationship
        {
            get
            {
                return this._Relationship;
            }
            set
            {
                this._Relationship = value;
            }
        }

        public int WaitDay
        {
            get
            {
                return this._WaitDay;
            }
            set
            {
                this._WaitDay = value;
            }
        }
    }
}

