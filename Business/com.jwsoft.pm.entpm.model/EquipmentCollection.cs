namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class EquipmentCollection : CollectionBase
    {
        public int Add(EquipmentInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(EquipmentInfo value)
        {
            return base.List.Contains(value);
        }

        public int Index(EquipmentInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, EquipmentInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(EquipmentInfo value)
        {
            base.List.Remove(value);
        }

        public ArrayList EquipmentList
        {
            get
            {
                return base.InnerList;
            }
        }

        public EquipmentInfo this[int index]
        {
            get
            {
                return (EquipmentInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

