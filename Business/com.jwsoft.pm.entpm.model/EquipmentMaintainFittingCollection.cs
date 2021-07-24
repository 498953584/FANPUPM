namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class EquipmentMaintainFittingCollection : CollectionBase
    {
        public int Add(EquipmentMaintainFittingInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(EquipmentMaintainFittingInfo value)
        {
            return base.List.Contains(value);
        }

        public int Index(EquipmentMaintainFittingInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, EquipmentMaintainFittingInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(EquipmentMaintainFittingInfo value)
        {
            base.List.Remove(value);
        }

        public EquipmentMaintainFittingInfo this[int index]
        {
            get
            {
                return (EquipmentMaintainFittingInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

