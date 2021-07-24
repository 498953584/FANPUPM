namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class TaskBookRationCollection : CollectionBase
    {
        public int Add(TaskBookRation value)
        {
            return base.List.Add(value);
        }

        public bool Contains(TaskBookRation value)
        {
            return base.List.Contains(value);
        }

        public int Index(TaskBookRation value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, TaskBookRation value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(TaskBookRation value)
        {
            base.List.Remove(value);
        }

        public TaskBookRation this[int index]
        {
            get
            {
                return (TaskBookRation) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

