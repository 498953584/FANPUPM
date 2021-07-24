namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class DeptCollection : CollectionBase
    {
        public int Add(DeptInfo value)
        {
            return base.List.Add(value);
        }

        public int IndexOf(DeptInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, DeptInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(DeptInfo value)
        {
            base.List.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        public DeptInfo this[int index]
        {
            get
            {
                return (DeptInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

