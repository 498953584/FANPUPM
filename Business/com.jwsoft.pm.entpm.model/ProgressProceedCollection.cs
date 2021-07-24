namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class ProgressProceedCollection : CollectionBase
    {
        public int Add(ProgressProceedInfo value)
        {
            return base.List.Add(value);
        }

        public void Clear()
        {
            this.Clear();
        }

        public bool Contains(ProgressProceedInfo value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(ProgressProceedInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ProgressProceedInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ProgressProceedInfo value)
        {
            base.List.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        public ProgressProceedInfo this[int index]
        {
            get
            {
                return (ProgressProceedInfo) base.List[index];
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

