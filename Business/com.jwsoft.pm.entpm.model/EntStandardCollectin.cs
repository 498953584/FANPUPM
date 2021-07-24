namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class EntStandardCollectin : CollectionBase
    {
        public int Add(EntStandardInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(EntStandardInfo value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(EntStandardInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, EntStandardInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(EntStandardInfo value)
        {
            base.List.Remove(value);
        }

        public EntStandardInfo this[int index]
        {
            get
            {
                return (EntStandardInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }

        public ArrayList PlanItems
        {
            get
            {
                return base.InnerList;
            }
        }
    }
}

