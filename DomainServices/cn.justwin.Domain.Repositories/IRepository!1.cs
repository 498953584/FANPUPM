namespace cn.justwin.Domain.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public interface IRepository<T> : IQueryable<T>, IEnumerable<T>, IQueryable, IEnumerable
    {
        void Add(T item);
        void Delete(T item);
        void Update(T item);
    }
}

