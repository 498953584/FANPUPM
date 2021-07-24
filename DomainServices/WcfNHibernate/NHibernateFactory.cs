namespace WcfNHibernate
{
    using cn.justwin.Domain.Services;
    using NHibernate;
    using System;

    public static class NHibernateFactory
    {
        private static ISessionFactory _sessionFactory;

        public static void Initialize()
        {
            _sessionFactory = SessionHelper.Factory;
        }

        public static ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}

