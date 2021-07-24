namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class EquipmentAttemperCollection : CollectionBase
    {
        public int Add(EquipmentAttemperInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(EquipmentAttemperInfo value)
        {
            return base.List.Contains(value);
        }

        public int Index(EquipmentAttemperInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, EquipmentAttemperInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(EquipmentAttemperInfo value)
        {
            base.List.Remove(value);
        }

        public EquipmentAttemperInfo this[int index]
        {
            get
            {
                return (EquipmentAttemperInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

