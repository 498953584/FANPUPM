namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ResourceCategoryCollection : CollectionBase
    {
        public int Add(ResourceCategory value)
        {
            return base.List.Add(value);
        }

        public bool Contains(ResourceCategory value)
        {
            return base.List.Contains(value);
        }

        public int Index(ResourceCategory value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ResourceCategory value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ResourceCategory value)
        {
            base.List.Remove(value);
        }

        public ResourceCategory this[int index]
        {
            get
            {
                return (ResourceCategory) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

