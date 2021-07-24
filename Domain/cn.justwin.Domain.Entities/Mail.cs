namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class Mail
    {
        private PTyhmc _mailFromYhmc;
        private PTyhmc _mailToYhmc;

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
            return (this.MailId == ((Mail) obj).MailId);
        }

        public override int GetHashCode()
        {
            return this.MailId.GetHashCode();
        }

        public override string ToString()
        {
            return this.MailId.ToString();
        }

        [DataMember]
        public virtual string AllCopyto { get; set; }

        [DataMember]
        public virtual string AllCopytoCode { get; set; }

        [DataMember]
        public virtual string AllMailTo { get; set; }

        [DataMember]
        public virtual string AllMailToCode { get; set; }

        [DataMember]
        public virtual string AnnexId { get; set; }

        [DataMember]
        public virtual DateTime InputDate { get; set; }

        [DataMember]
        public virtual bool IsReaded { get; set; }

        [DataMember]
        public virtual bool IsValid { get; set; }

        [DataMember]
        public virtual string MailContent { get; set; }

        [DataMember]
        public virtual string MailFrom { get; set; }

        [DataMember]
        public virtual PTyhmc MailFromYhmc
        {
            get
            {
                return this._mailFromYhmc;
            }
            set
            {
                this._mailFromYhmc = value;
            }
        }

        [DataMember]
        public virtual string MailId { get; set; }

        [DataMember]
        public virtual string MailName { get; set; }

        [DataMember]
        public virtual string MailTo { get; set; }

        [DataMember]
        public virtual PTyhmc MailToYhmc
        {
            get
            {
                return this._mailToYhmc;
            }
            set
            {
                this._mailToYhmc = value;
            }
        }

        [DataMember]
        public virtual string MailType { get; set; }

        [DataMember]
        public virtual DateTime? ReadTime { get; set; }

        [DataMember]
        public virtual string ToMailId { get; set; }
    }
}

