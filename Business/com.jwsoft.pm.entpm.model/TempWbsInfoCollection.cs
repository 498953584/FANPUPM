namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class TempWbsInfoCollection : CollectionBase
    {
        public int Add(TempWbsInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(TempWbsInfo value)
        {
            return base.List.Contains(value);
        }

        public int Index(TempWbsInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, TempWbsInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(TempWbsInfo value)
        {
            base.List.Remove(value);
        }

        public TempWbsInfo this[int index]
        {
            get
            {
                return (TempWbsInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

