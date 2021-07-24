namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class ProgressEvaluateCollection : CollectionBase
    {
        public int Add(ProgressEvaluateInfo value)
        {
            return base.List.Add(value);
        }

        public void Clear()
        {
            this.Clear();
        }

        public bool Contains(ProgressEvaluateInfo value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(ProgressEvaluateInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ProgressEvaluateInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ProgressEvaluateInfo value)
        {
            base.List.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        public ProgressEvaluateInfo this[int index]
        {
            get
            {
                return (ProgressEvaluateInfo) base.List[index];
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

