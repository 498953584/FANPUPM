namespace cn.justwin.contractModel
{
    using System;

    public class InvoiceTypeInfo
    {
        private string codeName;
        private string noteID;

        public string CodeName
        {
            get
            {
                return this.codeName;
            }
            set
            {
                this.codeName = value;
            }
        }

        public string NoteID
        {
            get
            {
                return this.noteID;
            }
            set
            {
                this.noteID = value;
            }
        }
    }
}

