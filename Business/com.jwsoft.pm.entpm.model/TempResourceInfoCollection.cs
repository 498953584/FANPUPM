namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class TempResourceInfoCollection : CollectionBase
    {
        public int Add(TempResourceInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(TempResourceInfo value)
        {
            return base.List.Contains(value);
        }

        public int Index(TempResourceInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, TempResourceInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(TempResourceInfo value)
        {
            base.List.Remove(value);
        }

        public TempResourceInfo this[int index]
        {
            get
            {
                return (TempResourceInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

