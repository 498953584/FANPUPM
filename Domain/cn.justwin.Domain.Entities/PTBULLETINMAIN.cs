namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class PTBULLETINMAIN
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
            return (this.I_BULLETINID == ((PTBULLETINMAIN) obj).I_BULLETINID);
        }

        public override int GetHashCode()
        {
            return this.I_BULLETINID.GetHashCode();
        }

        public override string ToString()
        {
            return this.I_BULLETINID.ToString();
        }

        public virtual int? AuditState { get; set; }

        public virtual string CorpCode { get; set; }

        public virtual string DeptRange { get; set; }

        [DataMember]
        public virtual DateTime? DTM_EXPRIESDATE { get; set; }

        [DataMember]
        public virtual DateTime? DTM_RELEASETIME { get; set; }

        [DataMember]
        public virtual Guid I_BULLETINID { get; set; }

        public virtual int? I_RELEASEBOUND { get; set; }

        public virtual string URL { get; set; }

        [DataMember]
        public virtual string V_CONTENT { get; set; }

        public virtual string V_RELEASEUSER { get; set; }

        public virtual string V_RELUSERCODE { get; set; }

        [DataMember]
        public virtual string V_TITLE { get; set; }
    }
}

