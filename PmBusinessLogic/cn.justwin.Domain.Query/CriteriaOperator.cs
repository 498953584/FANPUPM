namespace cn.justwin.Domain.Query
{
    using System;

    public enum CriteriaOperator
    {
        Equal,
        NotEqual,
        GreaterThan,
        LessThan,
        GreaterThanOrEqual,
        LesserThanOrEqual,
        Like,
        NotLike,
        IsNull,
        IsNotNull,
        In,
        NotIn
    }
}

