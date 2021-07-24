namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ProjectForepartExtendCollection : CollectionBase
    {
        public int Add(ProjectForepartExtend value)
        {
            return base.List.Add(value);
        }

        public bool Contains(ProjectForepartExtend value)
        {
            return base.List.Contains(value);
        }

        public int Index(ProjectForepartExtend value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ProjectForepartExtend value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ProjectForepartExtend value)
        {
            base.List.Remove(value);
        }

        public ProjectForepartExtend this[int index]
        {
            get
            {
                return (ProjectForepartExtend) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

