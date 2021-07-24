namespace cn.justwin.Domain.Query
{
    using cn.justwin.DAL;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;

    public sealed class Query<T>
    {
        private List<Criterion> criteria;
        private IList<OrderClause> orderClauses;

        public Query()
        {
            this.criteria = new List<Criterion>();
            this.orderClauses = new List<OrderClause>();
        }

        private string GenerateSQL()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("SELECT * FROM {0} WHERE 1 = 1 \n", this.ViewName);
            foreach (Criterion criterion in this.Criteria)
            {
                if (criterion.Operator == CriteriaOperator.Equal)
                {
                    builder.AppendFormat("\tAND {0} = '{1}' \n", criterion.PropertyName, criterion.Value);
                }
                if (criterion.Operator == CriteriaOperator.NotEqual)
                {
                    builder.AppendFormat("\tAND {0} != '{1}' \n", criterion.PropertyName, criterion.Value);
                }
                if (criterion.Operator == CriteriaOperator.GreaterThan)
                {
                    builder.AppendFormat("\tAND {0} > '{1}' \n", criterion.PropertyName, criterion.Value);
                }
                if (criterion.Operator == CriteriaOperator.LessThan)
                {
                    builder.AppendFormat("\tAND {0} < '{1}' \n", criterion.PropertyName, criterion.Value);
                }
                if (criterion.Operator == CriteriaOperator.GreaterThanOrEqual)
                {
                    builder.AppendFormat("\tAND {0} >= '{1}' \n", criterion.PropertyName, criterion.Value);
                }
                if (criterion.Operator == CriteriaOperator.LesserThanOrEqual)
                {
                    builder.AppendFormat("\tAND {0} <= '{1}' \n", criterion.PropertyName, criterion.Value);
                }
                if (criterion.Operator == CriteriaOperator.Like)
                {
                    builder.AppendFormat("\tAND {0} LIKE '%{1}%' \n", criterion.PropertyName, criterion.Value);
                }
                if (criterion.Operator == CriteriaOperator.NotLike)
                {
                    builder.AppendFormat("\tAND {0} NOT LIKE '%{1}%' \n", criterion.PropertyName, criterion.Value);
                }
                if (criterion.Operator == CriteriaOperator.IsNull)
                {
                    builder.AppendFormat("\tAND {0} IS NULL \n", criterion.PropertyName);
                }
                if (criterion.Operator == CriteriaOperator.IsNotNull)
                {
                    builder.AppendFormat("\tAND {0} IS NOT NULL \n", criterion.PropertyName);
                }
                if (criterion.Operator == CriteriaOperator.In)
                {
                    builder.AppendFormat("\tAND {0} IN ({1}) \n", criterion.PropertyName, criterion.Value);
                }
                if (criterion.Operator == CriteriaOperator.NotIn)
                {
                    builder.AppendFormat("\tAND {0} NOT IN ({1}) \n", criterion.PropertyName, criterion.Value);
                }
            }
            if (this.OrderClauses.Count > 0)
            {
                builder.Append(" ORDER BY ");
                foreach (OrderClause clause in this.OrderClauses)
                {
                    string str = (clause.Criterion == OrderClause.OrderRule.Asc) ? "ASC" : "DESC";
                    builder.AppendFormat(" {0} {1} ,", clause.PropertyName, str);
                }
                builder.Remove(builder.Length - 1, 1);
            }
            return builder.ToString();
        }

        public IList<T> GetByCriteria()
        {
            IList<T> list = new List<T>();
            using (pm2Entities entities = new pm2Entities())
            {
                return entities.ExecuteStoreQuery<T>(this.GenerateSQL(), new object[0]).ToList<T>();
            }
        }

        public IList<T> GetByCriteria(int pageSize, int index)
        {
            IList<T> list = new List<T>();
            using (pm2Entities entities = new pm2Entities())
            {
                foreach (T local in entities.ExecuteStoreQuery<T>(this.GenerateSQL(), new object[0]).Skip<T>((pageSize * (index - 1))).Take<T>(pageSize).ToList<T>())
                {
                    list.Add(local);
                }
            }
            return list;
        }

        public int GetCount()
        {
            using (pm2Entities entities = new pm2Entities())
            {
                return entities.ExecuteStoreQuery<T>(this.GenerateSQL(), new object[0]).Count<T>();
            }
        }

        public IList<Criterion> Criteria
        {
            get
            {
                return this.criteria;
            }
        }

        public IList<OrderClause> OrderClauses
        {
            get
            {
                return this.orderClauses;
            }
        }

        public string ViewName { get; set; }
    }
}

