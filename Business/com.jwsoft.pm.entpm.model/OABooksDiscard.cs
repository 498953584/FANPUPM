namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OABooksDiscard
    {
        private string _discardbookisbn;
        private int _discardcopy;
        private Guid _discardrecordid;

        public string DiscardBookISBN
        {
            get
            {
                return this._discardbookisbn;
            }
            set
            {
                this._discardbookisbn = value;
            }
        }

        public int DiscardCopy
        {
            get
            {
                return this._discardcopy;
            }
            set
            {
                this._discardcopy = value;
            }
        }

        public Guid DiscardRecordID
        {
            get
            {
                return this._discardrecordid;
            }
            set
            {
                this._discardrecordid = value;
            }
        }
    }
}

