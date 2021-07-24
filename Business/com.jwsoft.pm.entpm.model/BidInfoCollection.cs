namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class BidInfoCollection : CollectionBase
    {
        public int Add(BidInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(BidInfo value)
        {
            return base.List.Contains(value);
        }

        public int Index(BidInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, BidInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(BidInfo value)
        {
            base.List.Remove(value);
        }

        public BidInfo this[int index]
        {
            get
            {
                return (BidInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

