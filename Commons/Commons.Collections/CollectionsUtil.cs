namespace Commons.Collections
{
    using System;
    using System.Collections;

    public class CollectionsUtil
    {
        public static object PutElement(Hashtable hashTable, object key, object newValue)
        {
            object obj2 = hashTable[key];
            hashTable[key] = newValue;
            return obj2;
        }
    }
}

