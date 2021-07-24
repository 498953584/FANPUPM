namespace cn.justwin.Domain.Query
{
    using System;
    using System.Runtime.CompilerServices;

    public class OrderClause
    {
        public OrderClause(string propertyName, OrderRule rule)
        {
            this.PropertyName = propertyName;
            this.Criterion = rule;
        }

        public OrderRule Criterion { get; set; }

        public string PropertyName { get; set; }

        public enum OrderRule
        {
            Asc,
            Desc
        }
    }
}

