namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class MaterialApplyResourceCollection : CollectionBase
    {
        public int Add(MaterialApplyResource value)
        {
            return base.List.Add(value);
        }

        public bool Contains(MaterialApplyResource value)
        {
            return base.List.Contains(value);
        }

        public int Index(MaterialApplyResource value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, MaterialApplyResource value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(MaterialApplyResource value)
        {
            base.List.Remove(value);
        }

        public MaterialApplyResource this[int index]
        {
            get
            {
                return (MaterialApplyResource) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

