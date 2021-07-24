namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class ConstructOrganizeCollectin : CollectionBase
    {
        public int Add(ConstructOrganizeInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(ConstructOrganizeInfo value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(ConstructOrganizeInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ConstructOrganizeInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ConstructOrganizeInfo value)
        {
            base.List.Remove(value);
        }

        public ConstructOrganizeInfo this[int index]
        {
            get
            {
                return (ConstructOrganizeInfo) base.List[index];
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

