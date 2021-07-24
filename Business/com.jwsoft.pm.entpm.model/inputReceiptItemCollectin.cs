namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class inputReceiptItemCollectin : CollectionBase
    {
        public int Add(inputReceiptItem value)
        {
            return base.List.Add(value);
        }

        public bool Contains(inputReceiptItem value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(inputReceiptItem value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, inputReceiptItem value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(inputReceiptItem value)
        {
            base.List.Remove(value);
        }

        public inputReceiptItem this[int index]
        {
            get
            {
                return (inputReceiptItem) base.List[index];
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

