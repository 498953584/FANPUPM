namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class BidCorpCollection : CollectionBase
    {
        public int Add(BidCorp value)
        {
            return base.List.Add(value);
        }

        public bool Contains(BidCorp value)
        {
            return base.List.Contains(value);
        }

        public int Index(BidCorp value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, BidCorp value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(BidCorp value)
        {
            base.List.Remove(value);
        }

        public BidCorp this[int index]
        {
            get
            {
                return (BidCorp) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

