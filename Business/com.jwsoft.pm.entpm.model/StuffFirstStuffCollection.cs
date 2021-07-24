namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class StuffFirstStuffCollection : CollectionBase
    {
        public int Add(StuffFirstStuff value)
        {
            return base.List.Add(value);
        }

        public bool Contains(StuffFirstStuff value)
        {
            return base.List.Contains(value);
        }

        public int Index(StuffFirstStuff value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, StuffFirstStuff value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(StuffFirstStuff value)
        {
            base.List.Remove(value);
        }

        public StuffFirstStuff this[int index]
        {
            get
            {
                return (StuffFirstStuff) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

