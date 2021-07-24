namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class HROrgPostLevel
    {
        private int _positionlevel;
        private string _postandrank;
        private int _postlevel;
        private int _recordid;
        private string _remark;
        private string _type;

        public int PositionLevel
        {
            get
            {
                return this._positionlevel;
            }
            set
            {
                this._positionlevel = value;
            }
        }

        public string PostAndRank
        {
            get
            {
                return this._postandrank;
            }
            set
            {
                this._postandrank = value;
            }
        }

        public int PostLevel
        {
            get
            {
                return this._postlevel;
            }
            set
            {
                this._postlevel = value;
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

        public string Type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }
}

