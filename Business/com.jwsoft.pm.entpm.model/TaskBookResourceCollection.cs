namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class TaskBookResourceCollection : CollectionBase
    {
        public int Add(TaskBookResource value)
        {
            return base.List.Add(value);
        }

        public bool Contains(TaskBookResource value)
        {
            return base.List.Contains(value);
        }

        public int Index(TaskBookResource value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, TaskBookResource value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(TaskBookResource value)
        {
            base.List.Remove(value);
        }

        public TaskBookResource this[int index]
        {
            get
            {
                return (TaskBookResource) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

