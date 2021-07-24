namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using NHibernate;
    using NHibernate.Criterion;
    using NHibernate.SqlCommand;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;
    using WcfNHibernate;

    [ServiceContract, NHibernateContext, ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall), AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class MailService : Repository<Mail>
    {
        public void Add(IList<Mail> mailLst)
        {
            ISession session = SessionHelper.GetSession();
            ITransaction transaction = session.BeginTransaction();
            try
            {
                foreach (Mail mail in mailLst)
                {
                    session.Save(mail);
                }
                session.Flush();
                transaction.Commit();
            }
            catch (HibernateException)
            {
                transaction.Rollback();
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                }
            }
        }

        public void AddOrUpdate(Mail mail)
        {
            ISession session = SessionHelper.GetSession();
            session.SaveOrUpdate(mail);
            session.Flush();
        }

        public void AddOrUpdate(IList<Mail> mailLst)
        {
            ISession session = SessionHelper.GetSession();
            ITransaction transaction = session.BeginTransaction();
            try
            {
                foreach (Mail mail in mailLst)
                {
                    session.SaveOrUpdate(mail);
                }
                transaction.Commit();
            }
            catch (HibernateException)
            {
                transaction.Rollback();
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                }
            }
        }

        [WebInvoke(UriTemplate="/Mails", ResponseFormat=WebMessageFormat.Json, Method="PUT"), OperationContract]
        public string CreateTest(C1 c)
        {
            return ("PUT: " + c.name);
        }

        public void Delete(string id)
        {
            ISession session = SessionHelper.GetSession();
            string query = string.Format("from Mail Where MailId = '{0}'", id);
            session.Delete(query);
            session.Flush();
        }

        public void Delete(IList<string> idLst)
        {
            ISession session = SessionHelper.GetSession();
            ITransaction transaction = session.BeginTransaction();
            try
            {
                foreach (string str in idLst)
                {
                    string query = string.Format("from Mail Where MailId = '{0}'", str);
                    session.Delete(query);
                }
                transaction.Commit();
            }
            catch (HibernateException)
            {
                transaction.Rollback();
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                }
            }
        }

        [OperationContract, WebInvoke(UriTemplate="/Mails/{id}", ResponseFormat=WebMessageFormat.Json, Method="DELETE")]
        public string DeleteTest(string id)
        {
            return ("DELETE: " + id);
        }

        private ICriteria GenerateCriteria(string name, string content, string from, string to, DateTime? startDate, DateTime? endDate, string type, bool? isValid, bool? isReaded)
        {
            ICriteria criteria = SessionHelper.GetSession().CreateCriteria<Mail>();
            if (!string.IsNullOrEmpty(name))
            {
                criteria.Add(Restrictions.Like("MailName", name, MatchMode.Anywhere));
            }
            if (!string.IsNullOrEmpty(from))
            {
                criteria.CreateCriteria("MailFromYhmc", JoinType.LeftOuterJoin).Add(Restrictions.Like("v_xm", from, MatchMode.Anywhere));
            }
            if (!string.IsNullOrEmpty(to))
            {
                criteria.CreateCriteria("MailToYhmc", JoinType.LeftOuterJoin).Add(Restrictions.Like("v_xm", to, MatchMode.Anywhere));
            }
            if (startDate.HasValue)
            {
                criteria.Add(Restrictions.Ge("InputDate", startDate.Value));
            }
            if (endDate.HasValue)
            {
                criteria.Add(Restrictions.Le("InputDate", endDate.Value));
            }
            if (!string.IsNullOrEmpty(type))
            {
                criteria.Add(Restrictions.Eq("MailType", type));
            }
            if (isValid.HasValue)
            {
                criteria.Add(Restrictions.Eq("IsValid", isValid.Value));
            }
            if (isReaded.HasValue)
            {
                criteria.Add(Restrictions.Eq("IsReaded", isReaded.Value));
            }
            return criteria;
        }

        private IQueryable<Mail> GenerateQuery(string name, string content, string fromx, string to, DateTime? startDate, DateTime? endDate, string type, bool? isValid, bool? isReaded)
        {
            IQueryable<Mail> queryable = this.AsQueryable<Mail>();
            if (!string.IsNullOrEmpty(name))
            {
                queryable = from m in queryable
                    where m.MailName.Contains(name)
                    select m;
            }
            if (!string.IsNullOrEmpty(content))
            {
                queryable = from m in queryable
                    where m.MailContent.Contains(content)
                    select m;
            }
            if (!string.IsNullOrEmpty(fromx))
            {
                queryable = from m in queryable
                            where m.MailFromYhmc.v_xm.Contains(fromx)
                    select m;
            }
            if (!string.IsNullOrEmpty(to))
            {
                queryable = from m in queryable
                    where m.MailToYhmc.v_xm.Contains(to)
                    select m;
            }
            if (startDate.HasValue)
            {
                queryable = from m in queryable
                    where m.InputDate > startDate.Value
                    select m;
            }
            if (endDate.HasValue)
            {
                queryable = from m in queryable
                    where m.InputDate <= endDate.Value
                    select m;
            }
            if (!string.IsNullOrEmpty(type))
            {
                queryable = from m in queryable
                    where m.MailType == type
                    select m;
            }
            if (isValid.HasValue)
            {
                queryable = from m in queryable
                    where m.IsValid == isValid.Value
                    select m;
            }
            if (isReaded.HasValue)
            {
                queryable = from m in queryable
                    where m.IsReaded == isReaded.Value
                    select m;
            }
            return queryable;
        }

        public IList<Mail> GetAll()
        {
            return SessionHelper.GetSession().CreateQuery("from Mail").List<Mail>();
        }

        [OperationContract, WebGet(UriTemplate="/Mails/{id}", ResponseFormat=WebMessageFormat.Json)]
        public Mail GetById(string id)
        {
            return (from m in this
                where m.MailId == id
                select m).FirstOrDefault<Mail>();
        }

        public int GetCount(string name, string content, string from, string to, DateTime? startDate, DateTime? endDate, string type, bool? isValid, bool? isReaded)
        {
            return this.GenerateQuery(name, content, from, to, startDate, endDate, type, isValid, isReaded).Count<Mail>();
        }

        public IList<Mail> Query(string name, string content, string fromx, string to, DateTime? startDate, DateTime? endDate, string type, bool? isValid, bool? isReaded, int? size, int? index)
        {
            IQueryable<Mail> source = from m in this.GenerateQuery(name, content, fromx, to, startDate, endDate, type, isValid, isReaded)
                orderby m.InputDate descending
                select m;
            if (size.HasValue && index.HasValue)
            {
                source = source.Skip<Mail>(((index.Value - 1) * size.Value)).Take<Mail>(size.Value);
            }
            return source.ToList<Mail>();
        }

        public void Update(IList<Mail> mailLst)
        {
            ISession session = SessionHelper.GetSession();
            ITransaction transaction = session.BeginTransaction();
            try
            {
                foreach (Mail mail in mailLst)
                {
                    session.Update(mail);
                }
                transaction.Commit();
            }
            catch (HibernateException)
            {
                transaction.Rollback();
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Dispose();
                }
            }
        }
    }
}

