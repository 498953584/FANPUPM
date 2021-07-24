namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class MaterialApplyBillCollection : CollectionBase
    {
        public int Add(MaterialApplyBill value)
        {
            return base.List.Add(value);
        }

        public bool Contains(MaterialApplyBill value)
        {
            return base.List.Contains(value);
        }

        public int Index(MaterialApplyBill value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, MaterialApplyBill value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(MaterialApplyBill value)
        {
            base.List.Remove(value);
        }

        public MaterialApplyBill this[int index]
        {
            get
            {
                return (MaterialApplyBill) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

