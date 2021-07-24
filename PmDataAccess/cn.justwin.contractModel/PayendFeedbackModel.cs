namespace cn.justwin.contractModel
{
    using System;

    [Serializable]
    public class PayendFeedbackModel
    {
        private string _annex;
        private string _contractid;
        private string _feedbackopinion;
        private string _feedbackperson;
        private DateTime? _feedbacktime;
        private string _id;
        private string _inperson;
        private DateTime? _intime;
        private string feedbackstate;

        public string Annex
        {
            get
            {
                return this._annex;
            }
            set
            {
                this._annex = value;
            }
        }

        public string ContractID
        {
            get
            {
                return this._contractid;
            }
            set
            {
                this._contractid = value;
            }
        }

        public string FeedbackOpinion
        {
            get
            {
                return this._feedbackopinion;
            }
            set
            {
                this._feedbackopinion = value;
            }
        }

        public string FeedbackPerson
        {
            get
            {
                return this._feedbackperson;
            }
            set
            {
                this._feedbackperson = value;
            }
        }

        public string FeedbackState
        {
            get
            {
                return this.feedbackstate;
            }
            set
            {
                this.feedbackstate = value;
            }
        }

        public DateTime? FeedbackTime
        {
            get
            {
                return this._feedbacktime;
            }
            set
            {
                this._feedbacktime = value;
            }
        }

        public string ID
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public string InPerson
        {
            get
            {
                return this._inperson;
            }
            set
            {
                this._inperson = value;
            }
        }

        public DateTime? InTime
        {
            get
            {
                return this._intime;
            }
            set
            {
                this._intime = value;
            }
        }
    }
}

