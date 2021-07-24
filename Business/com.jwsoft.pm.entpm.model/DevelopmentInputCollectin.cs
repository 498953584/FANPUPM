namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class DevelopmentInputCollectin : CollectionBase
    {
        public int Add(DevelopmentInputInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(DevelopmentInputInfo value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(DevelopmentInputInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, DevelopmentInputInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(DevelopmentInputInfo value)
        {
            base.List.Remove(value);
        }

        public DevelopmentInputInfo this[int index]
        {
            get
            {
                return (DevelopmentInputInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }

        public ArrayList PlanItems
        {
            get
            {
                return base.InnerList;
            }
        }
    }
}

