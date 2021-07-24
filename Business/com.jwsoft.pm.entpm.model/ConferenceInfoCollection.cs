namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    [Serializable]
    public class ConferenceInfoCollection : CollectionBase
    {
        public int Add(ConferenceInfo value)
        {
            return base.List.Add(value);
        }

        public bool Contains(ConferenceInfo value)
        {
            return base.List.Contains(value);
        }

        public int Index(ConferenceInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ConferenceInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ConferenceInfo value)
        {
            base.List.Remove(value);
        }

        public ConferenceInfo this[int index]
        {
            get
            {
                return (ConferenceInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

