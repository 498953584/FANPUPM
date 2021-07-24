namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class inputReceiptYearCollectin : CollectionBase
    {
        public int Add(inputReceiptYear value)
        {
            return base.List.Add(value);
        }

        public bool Contains(inputReceiptYear value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(inputReceiptYear value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, inputReceiptYear value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(inputReceiptYear value)
        {
            base.List.Remove(value);
        }

        public inputReceiptYear this[int index]
        {
            get
            {
                return (inputReceiptYear) base.List[index];
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

