namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class OpenBidInfoCollection : CollectionBase
    {
        public int Add(OpenBidInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(OpenBidInfo value)
        {
            return base.List.Contains(value);
        }

        public int Index(OpenBidInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, OpenBidInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(OpenBidInfo value)
        {
            base.List.Remove(value);
        }

        public OpenBidInfo this[int index]
        {
            get
            {
                return (OpenBidInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

