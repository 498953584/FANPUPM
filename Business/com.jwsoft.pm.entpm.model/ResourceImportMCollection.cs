namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ResourceImportMCollection : CollectionBase
    {
        public int Add(ResourceImportM value)
        {
            return base.List.Add(value);
        }

        public bool Contains(ResourceImportM value)
        {
            return base.List.Contains(value);
        }

        public int Index(ResourceImportM value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ResourceImportM value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ResourceImportM value)
        {
            base.List.Remove(value);
        }

        public ResourceImportM this[int index]
        {
            get
            {
                return (ResourceImportM) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

