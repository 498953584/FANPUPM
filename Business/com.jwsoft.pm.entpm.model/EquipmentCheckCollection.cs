namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class EquipmentCheckCollection : CollectionBase
    {
        public int Add(EquipmentCheckInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(EquipmentCheckInfo value)
        {
            return base.List.Contains(value);
        }

        public int Index(EquipmentCheckInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, EquipmentCheckInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(EquipmentCheckInfo value)
        {
            base.List.Remove(value);
        }

        public EquipmentCheckInfo this[int index]
        {
            get
            {
                return (EquipmentCheckInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

