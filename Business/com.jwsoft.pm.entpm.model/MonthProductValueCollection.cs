namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class MonthProductValueCollection : CollectionBase
    {
        public int Add(MonthProductValue value)
        {
            return base.List.Add(value);
        }

        public bool Contains(MonthProductValue value)
        {
            return base.List.Contains(value);
        }

        public int Index(MonthProductValue value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, MonthProductValue value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(MonthProductValue value)
        {
            base.List.Remove(value);
        }

        public MonthProductValue this[int index]
        {
            get
            {
                return (MonthProductValue) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

