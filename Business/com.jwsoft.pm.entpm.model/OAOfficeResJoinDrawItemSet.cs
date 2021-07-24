namespace com.jwsoft.pm.entpm.model
{
    using System;

    public class OAOfficeResJoinDrawItemSet
    {
        private int _drawitemid;
        private int _number;
        private int _postlevel;
        private string _remark;

        public int DrawItemID
        {
            get
            {
                return this._drawitemid;
            }
            set
            {
                this._drawitemid = value;
            }
        }

        public int Number
        {
            get
            {
                return this._number;
            }
            set
            {
                this._number = value;
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
    }
}

