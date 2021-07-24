namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class StuffFirstMainCollection : CollectionBase
    {
        public int Add(StuffFirstMain value)
        {
            return base.List.Add(value);
        }

        public bool Contains(StuffFirstMain value)
        {
            return base.List.Contains(value);
        }

        public int Index(StuffFirstMain value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, StuffFirstMain value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(StuffFirstMain value)
        {
            base.List.Remove(value);
        }

        public StuffFirstMain this[int index]
        {
            get
            {
                return (StuffFirstMain) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

