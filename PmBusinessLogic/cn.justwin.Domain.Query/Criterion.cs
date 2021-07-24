namespace cn.justwin.Domain.Query
{
    using System;
    using System.Runtime.CompilerServices;

    public class Criterion
    {
        public Criterion(string propertyName, object value, CriteriaOperator _operator)
        {
            this.PropertyName = propertyName;
            this.Value = value;
            this.Operator = _operator;
        }

        public CriteriaOperator Operator { get; set; }

        public string PropertyName { get; set; }

        public object Value { get; set; }
    }
}

