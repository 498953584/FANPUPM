namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class ScheduleCollection : CollectionBase
    {
        public int Add(ScheduleInfo value)
        {
            return base.List.Add(value);
        }

        public void Clear()
        {
            this.Clear();
        }

        public bool Contains(ScheduleInfo value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(ScheduleInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ScheduleInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ScheduleInfo value)
        {
            base.List.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        public ScheduleInfo this[int index]
        {
            get
            {
                return (ScheduleInfo) base.List[index];
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

