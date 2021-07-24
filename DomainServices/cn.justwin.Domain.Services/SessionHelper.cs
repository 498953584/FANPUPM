namespace cn.justwin.Domain.Services
{
    using NHibernate;
    using NHibernate.Cfg;
    using System;

    public static class SessionHelper
    {
        private static ISessionFactory factory = new Configuration().Configure().BuildSessionFactory();

        public static ISession GetSession()
        {
            return factory.OpenSession();
        }

        public static ISessionFactory Factory
        {
            get
            {
                return factory;
            }
        }
    }
}

