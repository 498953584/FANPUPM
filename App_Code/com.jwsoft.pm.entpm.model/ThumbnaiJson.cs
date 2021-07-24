namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class ThumbnaiJson : Thumbnai
    {
        private string addDate1;

        [DataMember]
        public string AddDate1
        {
            get
            {
                return this.addDate1;
            }
            set
            {
                this.addDate1 = value;
            }
        }
    }
}

