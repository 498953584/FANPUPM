namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OAVotingOption
    {
        private string _option;
        private int _poll;
        private int _recordid;
        private string _remark;
        private int _votinginfoid;

        public string Option
        {
            get
            {
                return this._option;
            }
            set
            {
                this._option = value;
            }
        }

        public int Poll
        {
            get
            {
                return this._poll;
            }
            set
            {
                this._poll = value;
            }
        }

        public int RecordID
        {
            get
            {
                return this._recordid;
            }
            set
            {
                this._recordid = value;
            }
        }

        public string Remark
        {
            get
            {
                return this._remark;
            }
            set
            {
                this._remark = value;
            }
        }

        public int VotingInfoID
        {
            get
            {
                return this._votinginfoid;
            }
            set
            {
                this._votinginfoid = value;
            }
        }
    }
}

