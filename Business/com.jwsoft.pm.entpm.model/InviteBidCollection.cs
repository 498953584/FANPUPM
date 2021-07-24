namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class InviteBidCollection : CollectionBase
    {
        public int Add(InviteBid value)
        {
            return base.List.Add(value);
        }

        public bool Contains(InviteBid value)
        {
            return base.List.Contains(value);
        }

        public int Index(InviteBid value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, InviteBid value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(InviteBid value)
        {
            base.List.Remove(value);
        }

        public InviteBid this[int index]
        {
            get
            {
                return (InviteBid) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

