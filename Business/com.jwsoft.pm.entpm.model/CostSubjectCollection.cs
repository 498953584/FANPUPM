namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class CostSubjectCollection : CollectionBase
    {
        public int Add(CostSubjectInfo value)
        {
            return base.List.Add(value);
        }

        public void Clear()
        {
            this.Clear();
        }

        public bool Contains(CostSubjectInfo value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(CostSubjectInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, CostSubjectInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(CostSubjectInfo value)
        {
            base.List.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        public CostSubjectInfo this[int index]
        {
            get
            {
                return (CostSubjectInfo) base.List[index];
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

