namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class MonthPlanResourceInfoCollection : CollectionBase
    {
        public int Add(MonthPlanResourceInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(MonthPlanResourceInfo value)
        {
            return base.List.Contains(value);
        }

        public int Index(MonthPlanResourceInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, MonthPlanResourceInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(MonthPlanResourceInfo value)
        {
            base.List.Remove(value);
        }

        public MonthPlanResourceInfo this[int index]
        {
            get
            {
                return (MonthPlanResourceInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

