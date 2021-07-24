namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class UnitTypeCollection : CollectionBase
    {
        public int Add(UnitType value)
        {
            return base.List.Add(value);
        }

        public bool Contains(UnitType value)
        {
            return base.List.Contains(value);
        }

        public int Index(UnitType value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, UnitType value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(UnitType value)
        {
            base.List.Remove(value);
        }

        public UnitType this[int index]
        {
            get
            {
                return (UnitType) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

