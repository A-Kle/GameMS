using GamesMS.Core.Services.Internal;
using NHibernate;

namespace GamesMS.Core.Services
{
    public interface ISessionService : IDependency
    {
        public ISession CreateSession();
    }

    public class SessionService : ISessionService
    {
        private readonly ISessionFactoryHolder sessionHolder;

        public SessionService(ISessionFactoryHolder sessionHolder)
        {
            this.sessionHolder = sessionHolder;
        }

        public ISession CreateSession()
        {
            return sessionHolder.SessionFactory.OpenSession();
        }
    }
}
