namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class TaskRelationCollection : CollectionBase
    {
        public int Add(TaskRelation value)
        {
            return base.List.Add(value);
        }

        public bool Contains(TaskRelation value)
        {
            return base.List.Contains(value);
        }

        public int Index(TaskRelation value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, TaskRelation value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(TaskRelation value)
        {
            base.List.Remove(value);
        }

        public TaskRelation this[int index]
        {
            get
            {
                return (TaskRelation) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

