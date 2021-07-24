namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ContactCorpCollection : CollectionBase
    {
        public int Add(ContactCorp value)
        {
            return base.List.Add(value);
        }

        public bool Contains(ContactCorp value)
        {
            return base.List.Contains(value);
        }

        public int Index(ContactCorp value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, ContactCorp value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(ContactCorp value)
        {
            base.List.Remove(value);
        }

        public ContactCorp this[int index]
        {
            get
            {
                return (ContactCorp) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

