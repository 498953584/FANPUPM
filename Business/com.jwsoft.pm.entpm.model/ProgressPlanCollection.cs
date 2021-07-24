namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class ProgressPlanCollection : CollectionBase
    {
        public int Add(ProgressPlanInfo value)
        {
            return base.List.Add(value);
        }

        public void Clear()
        {
            this.Clear();
        }

        public bool Contains(ProgressPlanInfo value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(ProgressPlanInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ProgressPlanInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ProgressPlanInfo value)
        {
            base.List.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        public ProgressPlanInfo this[int index]
        {
            get
            {
                return (ProgressPlanInfo) base.List[index];
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

