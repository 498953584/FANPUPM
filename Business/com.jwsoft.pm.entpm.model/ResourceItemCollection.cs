namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ResourceItemCollection : CollectionBase
    {
        public int Add(ResourceItem value)
        {
            return base.List.Add(value);
        }

        public bool Contains(ResourceItem value)
        {
            return base.List.Contains(value);
        }

        public int Index(ResourceItem value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ResourceItem value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ResourceItem value)
        {
            base.List.Remove(value);
        }

        public ResourceItem this[int index]
        {
            get
            {
                return (ResourceItem) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

