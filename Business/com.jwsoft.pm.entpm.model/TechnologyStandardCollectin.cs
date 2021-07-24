namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class TechnologyStandardCollectin : CollectionBase
    {
        public int Add(TechnologyStandardInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(TechnologyStandardInfo value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(TechnologyStandardInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, TechnologyStandardInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(TechnologyStandardInfo value)
        {
            base.List.Remove(value);
        }

        public TechnologyStandardInfo this[int index]
        {
            get
            {
                return (TechnologyStandardInfo) base.List[index];
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

