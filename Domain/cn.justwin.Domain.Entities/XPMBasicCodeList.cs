namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class XPMBasicCodeList
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
            return (this.NoteID == ((XPMBasicCodeList) obj).NoteID);
        }

        public override int GetHashCode()
        {
            return this.NoteID.GetHashCode();
        }

        public override string ToString()
        {
            return this.NoteID.ToString();
        }

        public virtual int ChildNumber { get; set; }

        public virtual int CodeID { get; set; }

        public virtual string CodeName { get; set; }

        public virtual bool IsDefault { get; set; }

        public virtual bool IsFixed { get; set; }

        public virtual bool IsValid { get; set; }

        public virtual bool? IsVisible { get; set; }

        public virtual int? IXh { get; set; }

        public virtual int NoteID { get; set; }

        public virtual string Owner { get; set; }

        public virtual int ParentCodeID { get; set; }

        public virtual string ParentCodeList { get; set; }

        public virtual int TypeID { get; set; }

        public virtual DateTime? VersionTime { get; set; }
    }
}

