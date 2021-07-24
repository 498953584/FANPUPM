namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class KnowledgeTypeModelCollection : CollectionBase
    {
        public int Add(KnowledgeTypeModel value)
        {
            return base.List.Add(value);
        }

        public bool Contains(KnowledgeTypeModel value)
        {
            return base.List.Contains(value);
        }

        public int Index(KnowledgeTypeModel value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, KnowledgeTypeModel value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(KnowledgeTypeModel value)
        {
            base.List.Remove(value);
        }

        public KnowledgeTypeModel this[int index]
        {
            get
            {
                return (KnowledgeTypeModel) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

