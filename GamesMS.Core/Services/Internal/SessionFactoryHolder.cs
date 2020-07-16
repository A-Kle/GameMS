using NHibernate;

namespace GamesMS.Core.Services.Internal
{
    public interface ISessionFactoryHolder : ISingletonDependency
    {
        ISessionFactory SessionFactory { get; }
    }

    sealed class SessionFactoryHolder : ISessionFactoryHolder
    {
        public ISessionFactory SessionFactory { get; }

        public SessionFactoryHolder(ISessionFactoryHelper sessionFactoryHelper)
        {

            if(SessionFactory == null)
            {
                SessionFactory = sessionFactoryHelper.CreateSessionFactory();
            }
        }
    }
}
