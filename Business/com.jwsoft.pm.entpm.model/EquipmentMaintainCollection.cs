namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class EquipmentMaintainCollection : CollectionBase
    {
        public int Add(EquipmentMaintainInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(EquipmentMaintainInfo value)
        {
            return base.List.Contains(value);
        }

        public int Index(EquipmentMaintainInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, EquipmentMaintainInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(EquipmentMaintainInfo value)
        {
            base.List.Remove(value);
        }

        public EquipmentMaintainInfo this[int index]
        {
            get
            {
                return (EquipmentMaintainInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

