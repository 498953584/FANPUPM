namespace WcfNHibernate
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Dispatcher;

    public class NHibernateContextInitializer : IInstanceContextInitializer
    {
        public void Initialize(InstanceContext instanceContext, Message message)
        {
            NHibernateContextExtension item = new NHibernateContextExtension(NHibernateFactory.OpenSession());
            instanceContext.Extensions.Add(item);
        }
    }
}

