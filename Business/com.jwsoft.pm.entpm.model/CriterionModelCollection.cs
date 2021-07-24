namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class CriterionModelCollection : CollectionBase
    {
        public int Add(CriterionModel value)
        {
            return base.List.Add(value);
        }

        public bool Contains(CriterionModel value)
        {
            return base.List.Contains(value);
        }

        public int Index(CriterionModel value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, CriterionModel value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(CriterionModel value)
        {
            base.List.Remove(value);
        }

        public CriterionModel this[int index]
        {
            get
            {
                return (CriterionModel) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

