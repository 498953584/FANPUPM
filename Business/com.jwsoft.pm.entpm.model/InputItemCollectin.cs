namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class InputItemCollectin : CollectionBase
    {
        public int Add(InputItemInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(InputItemInfo value)
        {
            return base.List.Contains(value);
        }

        public int IndexOf(InputItemInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, InputItemInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(InputItemInfo value)
        {
            base.List.Remove(value);
        }

        public InputItemInfo this[int index]
        {
            get
            {
                return (InputItemInfo) base.List[index];
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

