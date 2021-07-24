namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class SupplyPlanMainCollection : CollectionBase
    {
        private int _dbitemcount;

        public int Add(SupplyPlanMain value)
        {
            return base.List.Add(value);
        }

        public int DBRecourdCount
        {
            get
            {
                return this._dbitemcount;
            }
            set
            {
                this._dbitemcount = value;
            }
        }

        public SupplyPlanMain this[int index]
        {
            get
            {
                return (SupplyPlanMain) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }

        public ArrayList SupplyPlans
        {
            get
            {
                return base.InnerList;
            }
        }
    }
}

