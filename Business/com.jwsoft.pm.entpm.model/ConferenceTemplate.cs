namespace com.jwsoft.pm.entpm.Model
{
    using System;

    public class ConferenceTemplate
    {
        private int _classid;
        private string _content;
        private int _recordid;
        private string _templetname;

        public int ClassID
        {
            get
            {
                return this._classid;
            }
            set
            {
                this._classid = value;
            }
        }

        public string Content
        {
            get
            {
                return this._content;
            }
            set
            {
                this._content = value;
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

        public string TempletName
        {
            get
            {
                return this._templetname;
            }
            set
            {
                this._templetname = value;
            }
        }
    }
}

