namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class PriceItemCollection : CollectionBase
    {
        public int Add(PriceItem value)
        {
            return base.List.Add(value);
        }

        public bool Contains(PriceItem value)
        {
            return base.List.Contains(value);
        }

        public int Index(PriceItem value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, PriceItem value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(PriceItem value)
        {
            base.List.Remove(value);
        }

        public PriceItem this[int index]
        {
            get
            {
                return (PriceItem) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

