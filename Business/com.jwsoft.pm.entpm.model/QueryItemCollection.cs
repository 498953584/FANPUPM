namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class QueryItemCollection : CollectionBase
    {
        public int Add(QueryItem value)
        {
            return base.List.Add(value);
        }

        public bool Contains(QueryItem value)
        {
            return base.List.Contains(value);
        }

        public int Index(QueryItem value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, QueryItem value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(QueryItem value)
        {
            base.List.Remove(value);
        }

        public QueryItem this[int index]
        {
            get
            {
                return (QueryItem) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

