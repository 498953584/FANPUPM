namespace cn.justwin.opm.PM.prj_plan
{
    using System;
    using System.Runtime.Serialization;

    [Serializable, DataContract]
    public class Thumbnai
    {
        private DateTime addDate;
        private string imgPath;
        private string remark;
        private string showContent;
        private Guid thumbnaiCode;
        private string thumbnaImgPath;
        private string thumbnaName;

        [DataMember]
        public DateTime AddDate
        {
            get
            {
                return this.addDate;
            }
            set
            {
                this.addDate = value;
            }
        }

        [DataMember]
        public string ImgPath
        {
            get
            {
                return this.imgPath;
            }
            set
            {
                this.imgPath = value;
            }
        }

        [DataMember]
        public string Remark
        {
            get
            {
                return this.remark;
            }
            set
            {
                this.remark = value;
            }
        }

        [DataMember]
        public string ShowContent
        {
            get
            {
                return this.showContent;
            }
            set
            {
                this.showContent = value;
            }
        }

        [DataMember]
        public Guid ThumbnaiCode
        {
            get
            {
                return this.thumbnaiCode;
            }
            set
            {
                this.thumbnaiCode = value;
            }
        }

        [DataMember]
        public string ThumbnaImgPath
        {
            get
            {
                return this.thumbnaImgPath;
            }
            set
            {
                this.thumbnaImgPath = value;
            }
        }

        [DataMember]
        public string ThumbnaName
        {
            get
            {
                return this.thumbnaName;
            }
            set
            {
                this.thumbnaName = value;
            }
        }
    }
}

