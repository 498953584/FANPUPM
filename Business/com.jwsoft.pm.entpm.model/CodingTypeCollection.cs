namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class CodingTypeCollection : CollectionBase
    {
        public int Add(CodingType value)
        {
            return base.List.Add(value);
        }

        public bool Contains(CodingType value)
        {
            return base.List.Contains(value);
        }

        public int Index(CodingType value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, CodingType value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(CodingType value)
        {
            base.List.Remove(value);
        }

        public CodingType this[int index]
        {
            get
            {
                return (CodingType) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

