namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class MonthPlanInfoCollection : CollectionBase
    {
        public int Add(MonthPlanInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(MonthPlanInfo value)
        {
            return base.List.Contains(value);
        }

        public int Index(MonthPlanInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, MonthPlanInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(MonthPlanInfo value)
        {
            base.List.Remove(value);
        }

        public MonthPlanInfo this[int index]
        {
            get
            {
                return (MonthPlanInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

