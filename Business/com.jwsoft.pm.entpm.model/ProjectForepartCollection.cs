namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class ProjectForepartCollection : CollectionBase
    {
        public int Add(ProjectForepart value)
        {
            return base.List.Add(value);
        }

        public bool Contains(ProjectForepart value)
        {
            return base.List.Contains(value);
        }

        public int Index(ProjectForepart value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ProjectForepart value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ProjectForepart value)
        {
            base.List.Remove(value);
        }

        public ProjectForepart this[int index]
        {
            get
            {
                return (ProjectForepart) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

