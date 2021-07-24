namespace WcfNHibernate
{
    using NHibernate;

    public interface INHibernateContextExtension
    {
        ISession Session { get; }
    }
}

