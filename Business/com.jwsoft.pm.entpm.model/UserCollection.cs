namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class UserCollection : CollectionBase
    {
        public int Add(UserInfo value)
        {
            return base.List.Add(value);
        }

        public int IndexOf(UserInfo value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, UserInfo value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(UserInfo value)
        {
            base.List.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        public UserInfo this[int index]
        {
            get
            {
                return (UserInfo) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

