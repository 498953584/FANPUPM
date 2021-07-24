namespace cn.justwin.Domain.Query
{
    using System;
    using System.Collections.Generic;

    public interface IQueryCriterion<T>
    {
        IList<T> GetByCriteria(cn.justwin.Domain.Query.Query<T> query);
        IList<T> GetByCriteria(cn.justwin.Domain.Query.Query<T> query, int pageSize, int index);
        int GetCount(cn.justwin.Domain.Query.Query<T> query);
    }
}

