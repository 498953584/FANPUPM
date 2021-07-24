namespace com.jwsoft.pm.entpm.model
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class QualitySafeFileModelCollection : CollectionBase
    {
        public int Add(QualitySafeFileModel value)
        {
            return base.List.Add(value);
        }

        public bool Contains(QualitySafeFileModel value)
        {
            return base.List.Contains(value);
        }

        public int Index(QualitySafeFileModel value)
        {
            return base.List.IndexOf(value);
        }

        public void Insert(int index, QualitySafeFileModel value)
        {
            base.List.Insert(index, value);
        }

        public void Remove(QualitySafeFileModel value)
        {
            base.List.Remove(value);
        }

        public QualitySafeFileModel this[int index]
        {
            get
            {
                return (QualitySafeFileModel) base.List[index];
            }
            set
            {
                base.List[index] = value;
            }
        }
    }
}

