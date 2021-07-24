namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class eFileAuthorization
    {
        private int _filerecordid;
        private string _leadercode;
        private string _readercodes;

        public int FileRecordID
        {
            get
            {
                return this._filerecordid;
            }
            set
            {
                this._filerecordid = value;
            }
        }

        public string LeaderCode
        {
            get
            {
                return this._leadercode;
            }
            set
            {
                this._leadercode = value;
            }
        }

        public string ReaderCodes
        {
            get
            {
                return this._readercodes;
            }
            set
            {
                this._readercodes = value;
            }
        }
    }
}

