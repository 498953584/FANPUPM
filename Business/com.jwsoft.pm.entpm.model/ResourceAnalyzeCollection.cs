namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ResourceAnalyzeCollection : CollectionBase
    {
        public int Add(ResourceAnalyze value)
        {
            return base.List.Add(value);
        }

        public bool Contains(ResourceAnalyze value)
        {
            return base.List.Contains(value);
        }

        public int Index(ResourceAnalyze value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ResourceAnalyze value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ResourceAnalyze value)
        {
            base.List.Remove(value);
        }

        public ResourceAnalyze this[int index]
        {
            get
            {
                return (ResourceAnalyze) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

