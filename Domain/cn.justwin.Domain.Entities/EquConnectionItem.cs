namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class EquConnectionItem
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
            EquConnectionItem item = obj as EquConnectionItem;
            return (this.ItemCode.Equals(item.ItemCode) && this.ConnectionId.Equals(item.ConnectionId));
        }

        public override int GetHashCode()
        {
            return this.ItemCode.GetHashCode();
        }

        public override string ToString()
        {
            return this.ItemCode.ToString();
        }

        public virtual string ConnectionId { get; set; }

        public virtual string ItemCode { get; set; }
    }
}

