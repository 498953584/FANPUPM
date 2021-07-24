namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ConstructTaskBookCollection : CollectionBase
    {
        public int Add(ConstructTaskBook value)
        {
            return base.List.Add(value);
        }

        public bool Contains(ConstructTaskBook value)
        {
            return base.List.Contains(value);
        }

        public int Index(ConstructTaskBook value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ConstructTaskBook value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ConstructTaskBook value)
        {
            base.List.Remove(value);
        }

        public ConstructTaskBook this[int index]
        {
            get
            {
                return (ConstructTaskBook) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

