namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class ExpertProjectCollectin : CollectionBase
    {
        public int Add(ExpertProjectInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(ExpertProjectInfo value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(ExpertProjectInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ExpertProjectInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ExpertProjectInfo value)
        {
            base.List.Remove(value);
        }

        public ExpertProjectInfo this[int index]
        {
            get
            {
                return (ExpertProjectInfo) base.List[index];
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

