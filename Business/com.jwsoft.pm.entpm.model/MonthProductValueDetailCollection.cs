namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class MonthProductValueDetailCollection : CollectionBase
    {
        public int Add(MonthProductValueDetail value)
        {
            return base.List.Add(value);
        }

        public bool Contains(MonthProductValueDetail value)
        {
            return base.List.Contains(value);
        }

        public int Index(MonthProductValueDetail value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, MonthProductValueDetail value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(MonthProductValueDetail value)
        {
            base.List.Remove(value);
        }

        public MonthProductValueDetail this[int index]
        {
            get
            {
                return (MonthProductValueDetail) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

