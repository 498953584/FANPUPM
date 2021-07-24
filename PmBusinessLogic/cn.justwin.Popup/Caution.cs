namespace cn.justwin.Popup
{
    using System;

    [Serializable]
    public abstract class Caution
    {
        private string content;
        private string handleUrl;
        private string id;
        private string module;
        private string title;

        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }

        public string HandleUrl
        {
            get
            {
                return this.handleUrl;
            }
            set
            {
                this.handleUrl = value;
            }
        }

        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string Module
        {
            get
            {
                return this.module;
            }
            set
            {
                this.module = value;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }
    }
}

