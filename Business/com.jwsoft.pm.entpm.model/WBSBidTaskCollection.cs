namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class WBSBidTaskCollection : CollectionBase
    {
        public int Add(WBSBidTask value)
        {
            return base.List.Add(value);
        }

        public bool Contains(WBSBidTask value)
        {
            return base.List.Contains(value);
        }

        public int Index(WBSBidTask value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, WBSBidTask value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(WBSBidTask value)
        {
            base.List.Remove(value);
        }

        public WBSBidTask this[int index]
        {
            get
            {
                return (WBSBidTask) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

