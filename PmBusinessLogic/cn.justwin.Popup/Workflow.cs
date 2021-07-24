namespace cn.justwin.Popup
{
    using System;

    public class Workflow : Caution
    {
        private string businessClass;
        private string businessCode;
        private string isAllPass;
        private string nodeId;
        private string noteId;

        public string BusinessClass
        {
            get
            {
                return this.businessClass;
            }
            set
            {
                this.businessClass = value;
            }
        }

        public string BusinessCode
        {
            get
            {
                return this.businessCode;
            }
            set
            {
                this.businessCode = value;
            }
        }

        public string IsAllPass
        {
            get
            {
                return this.isAllPass;
            }
            set
            {
                this.isAllPass = value;
            }
        }

        public string NodeID
        {
            get
            {
                return this.nodeId;
            }
            set
            {
                this.nodeId = value;
            }
        }

        public string NoteId
        {
            get
            {
                return this.noteId;
            }
            set
            {
                this.noteId = value;
            }
        }
    }
}

