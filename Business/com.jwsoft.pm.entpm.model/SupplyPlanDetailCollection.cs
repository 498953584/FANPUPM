namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class SupplyPlanDetailCollection : CollectionBase
    {
        public int Add(SupplyPlanDetail value)
        {
            return base.List.Add(value);
        }

        public ArrayList AllItems
        {
            get
            {
                return base.InnerList;
            }
        }

        public SupplyPlanDetail this[int index]
        {
            get
            {
                return (SupplyPlanDetail) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

