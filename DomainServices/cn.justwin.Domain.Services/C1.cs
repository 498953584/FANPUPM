namespace cn.justwin.Domain.Services
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class C1
    {
        [DataMember]
        public string id;
        [DataMember]
        public string name;
    }
}

