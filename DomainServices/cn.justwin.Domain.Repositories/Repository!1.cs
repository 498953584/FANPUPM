namespace cn.justwin.Domain.Repositories
{
    using cn.justwin.Domain.Services;
    using NHibernate;
    using NHibernate.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;

    public class Repository<T> : IRepository<T>, IQueryable<T>, IEnumerable<T>, IQueryable, IEnumerable
    {
        public void Add(T item)
        {
            ISession session = SessionHelper.GetSession();
            session.Save(item);
            session.Flush();
        }

        public void AddOrUpdate(T item)
        {
            ISession session = SessionHelper.GetSession();
            session.SaveOrUpdate(item);
            session.Flush();
        }

        public void Delete(T item)
        {
            ISession session = SessionHelper.GetSession();
            session.Delete(item);
            session.Flush();
        }

        public IList<object> ExcuteSql(string sql)
        {
            return SessionHelper.GetSession().CreateSQLQuery(sql).List<object>();
        }

        public DataTable ExecuteQuery(string cmdText, params SqlParameter[] parameters)
        {
            DataTable table;
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["sql"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    this.PrepareCommand(command, connection, null, CommandType.Text, cmdText, parameters);
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataSet dataSet = new DataSet();
                        adapter.Fill(dataSet, "ds");
                        command.Parameters.Clear();
                        table = dataSet.Tables[0];
                    }
                }
            }
            return table;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return SessionHelper.GetSession().Query<T>().AsEnumerable<T>().GetEnumerator();
        }

        public string GetInParameterSql(IList<string> arr)
        {
            StringBuilder builder = new StringBuilder();
            if (arr.Count == 0)
            {
                builder.Append("''");
            }
            else
            {
                builder.Append("'");
                foreach (string str in arr)
                {
                    builder.Append(str).Append("','");
                }
                builder.Remove(builder.Length - 2, 2);
            }
            return builder.ToString();
        }

        private void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
            {
                cmd.Transaction = trans;
            }
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Update(T item)
        {
            ISession session = SessionHelper.GetSession();
            session.Update(item);
            session.Flush();
        }

        public Type ElementType
        {
            get
            {
                return SessionHelper.GetSession().Query<T>().ElementType;
            }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get
            {
                return SessionHelper.GetSession().Query<T>().Expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return SessionHelper.GetSession().Query<T>().Provider;
            }
        }
    }
}

