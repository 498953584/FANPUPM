namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ResourceBasicCollection : CollectionBase
    {
        public int Add(ResourceBasicInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(ResourceBasicInfo value)
        {
            return base.List.Contains(value);
        }

        public int Index(ResourceBasicInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ResourceBasicInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ResourceBasicInfo value)
        {
            base.List.Remove(value);
        }

        public ResourceBasicInfo this[int index]
        {
            get
            {
                return (ResourceBasicInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

