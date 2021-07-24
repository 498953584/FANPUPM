namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class BasicSerialNumber
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
            return (this.No == ((BasicSerialNumber) obj).No);
        }

        public override int GetHashCode()
        {
            return this.No.GetHashCode();
        }

        public override string ToString()
        {
            return this.No.ToString();
        }

        public virtual DateTime InTime { get; set; }

        public virtual string KeyValue { get; set; }

        public virtual string No { get; set; }

        public virtual string TableName { get; set; }
    }
}

