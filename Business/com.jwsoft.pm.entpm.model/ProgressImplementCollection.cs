namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class ProgressImplementCollection : CollectionBase
    {
        public int Add(ProgressImplementInfo value)
        {
            return base.List.Add(value);
        }

        public void Clear()
        {
            this.Clear();
        }

        public bool Contains(ProgressImplementInfo value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(ProgressImplementInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ProgressImplementInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ProgressImplementInfo value)
        {
            base.List.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        public ProgressImplementInfo this[int index]
        {
            get
            {
                return (ProgressImplementInfo) base.List[index];
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

