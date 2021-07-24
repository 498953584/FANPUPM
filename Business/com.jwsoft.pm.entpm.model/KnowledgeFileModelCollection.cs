namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class KnowledgeFileModelCollection : CollectionBase
    {
        public int Add(KnowledgeDataModel value)
        {
            return base.List.Add(value);
        }

        public bool Contains(KnowledgeDataModel value)
        {
            return base.List.Contains(value);
        }

        public int Index(KnowledgeDataModel value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, KnowledgeDataModel value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(KnowledgeDataModel value)
        {
            base.List.Remove(value);
        }

        public KnowledgeDataModel this[int index]
        {
            get
            {
                return (KnowledgeDataModel) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

