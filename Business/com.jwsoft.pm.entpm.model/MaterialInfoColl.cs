namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class MaterialInfoColl : CollectionBase
    {
        public int Add(MaterialInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(MaterialInfo value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(MaterialInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, MaterialInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(MaterialInfo value)
        {
            base.List.Remove(value);
        }

        public MaterialInfo this[int index]
        {
            get
            {
                return (MaterialInfo) base.List[index];
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

