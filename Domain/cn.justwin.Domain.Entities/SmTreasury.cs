namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class SmTreasury
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (base.GetType() != obj.GetType())
            {
                return false;
            }
            return (this.tcode == ((SmTreasury) obj).tcode);
        }

        public override int GetHashCode()
        {
            return this.tcode.GetHashCode();
        }

        public override string ToString()
        {
            return this.tcode.ToString();
        }

        public virtual bool? IsContrast { get; set; }

        public virtual string prjCode { get; set; }

        [DataMember]
        public virtual string ptcode { get; set; }

        public virtual string taddress { get; set; }

        [DataMember]
        public virtual string tcode { get; set; }

        public virtual string texplain { get; set; }

        [DataMember]
        public virtual string tflag { get; set; }

        [DataMember]
        public virtual string tid { get; set; }

        [DataMember]
        public virtual string tname { get; set; }
    }
}

