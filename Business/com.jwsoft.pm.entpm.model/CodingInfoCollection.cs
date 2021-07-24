namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class CodingInfoCollection : CollectionBase
    {
        public int Add(CodingInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(CodingInfo value)
        {
            return base.List.Contains(value);
        }

        public int Index(CodingInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, CodingInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(CodingInfo value)
        {
            base.List.Remove(value);
        }

        public CodingInfo this[int index]
        {
            get
            {
                return (CodingInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

