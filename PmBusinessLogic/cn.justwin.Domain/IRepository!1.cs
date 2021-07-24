namespace cn.justwin.Domain
{
    using System;
    using System.Collections.Generic;

    public interface IRepository<T>
    {
        void Add(T item);
        void Delete(string id);
        void Delete(IList<string> idList);
        IList<T> GetAll();
        IList<T> GetAll(int pageSize, int index);
        T GetById(string id);
        int GetCount();
        void Update(T item);
    }
}

