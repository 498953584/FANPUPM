namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OAVotingRecord
    {
        private string _selectoptions;
        private DateTime _votedate;
        private string _voter;
        private int _votingrecordid;

        public string SelectOptions
        {
            get
            {
                return this._selectoptions;
            }
            set
            {
                this._selectoptions = value;
            }
        }

        public DateTime VoteDate
        {
            get
            {
                return this._votedate;
            }
            set
            {
                this._votedate = value;
            }
        }

        public string Voter
        {
            get
            {
                return this._voter;
            }
            set
            {
                this._voter = value;
            }
        }

        public int VotingRecordID
        {
            get
            {
                return this._votingrecordid;
            }
            set
            {
                this._votingrecordid = value;
            }
        }
    }
}

