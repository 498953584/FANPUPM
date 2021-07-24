namespace WcfNHibernate
{
    using NHibernate;
    using System;
    using System.Runtime.CompilerServices;
    using System.ServiceModel;

    public class NHibernateContextExtension : IExtension<InstanceContext>, INHibernateContextExtension
    {
        public NHibernateContextExtension(ISession session)
        {
            this.Session = session;
        }

        public void Attach(InstanceContext owner)
        {
        }

        public void Detach(InstanceContext owner)
        {
        }

        public ISession Session { get; private set; }
    }
}

