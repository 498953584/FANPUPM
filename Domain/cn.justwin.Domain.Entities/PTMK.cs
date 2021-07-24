namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class PTMK
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
            return (this.C_MKDM == ((PTMK) obj).C_MKDM);
        }

        public override int GetHashCode()
        {
            return this.C_MKDM.GetHashCode();
        }

        public override string ToString()
        {
            return this.C_MKDM.ToString();
        }

        [DataMember(Name="attributes", Order=6)]
        public virtual string Attributes { get; set; }

        [IgnoreDataMember]
        public virtual string C_BS { get; set; }

        [DataMember(Name="id", Order=1)]
        public virtual string C_MKDM { get; set; }

        [DataMember(Name="children", Order=5)]
        public virtual IList<PTMK> Children { get; set; }

        [IgnoreDataMember]
        public virtual string helppath { get; set; }

        [IgnoreDataMember]
        public virtual int? i_ChildNum { get; set; }

        [IgnoreDataMember]
        public virtual int I_ID { get; set; }

        [IgnoreDataMember]
        public virtual int? I_XH { get; set; }

        [IgnoreDataMember]
        public virtual string IsBasic { get; set; }

        [IgnoreDataMember]
        public virtual string Isdisplay { get; set; }

        [IgnoreDataMember]
        public virtual string IsMaintainable { get; set; }

        [DataMember(Name="state", Order=4)]
        public virtual string State { get; set; }

        [DataMember(Name="url", Order=3)]
        public virtual string V_CDLJ { get; set; }

        public virtual string V_IMG { get; set; }

        [DataMember(Name="text", Order=2)]
        public virtual string V_MKMC { get; set; }
    }
}

