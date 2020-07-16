using GamesMS.Core.Models;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GamesMS.Core.Services
{
    public interface IRepository<TRecord> : IDependency where TRecord : class, IRecord
    {
        void CreateOrUpdate(TRecord record);
        void Delete(TRecord record);
        TRecord Get(int id);
        IQueryable<TRecord> Query();
        IQueryable<TRecord> Query(int total);
        IList<TRecord> QueryOver(int pageNumber, int pageSize);
    }

    public abstract class Repository<TRecord> : IDisposable, IRepository<TRecord> where TRecord : class, IRecord
    {
        private readonly ISessionService sessionService;
        private ISession session;

        public Repository(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }

        public void Dispose()
        {
            if(session != null)
            {
                session.Close();
                session.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        public virtual void CreateOrUpdate(TRecord record)
        {
            if (session == null)
                session = sessionService.CreateSession();

            using (var trans = session.BeginTransaction())
            {
                session.SaveOrUpdate(record);
                trans.Commit();
            }
        }

        public void Delete(TRecord record)
        {
            if (session == null)
                session = sessionService.CreateSession();

            using (var trans = session.BeginTransaction())
            {
                session.Delete(record);
                trans.Commit();
            }
        }

        public TRecord Get(int id)
        {
            if (session == null)
                session = sessionService.CreateSession();

            using (var trans = session.BeginTransaction())
            {
                return session.Query<TRecord>()
                    .Where(r => r.Id == id)
                    .FirstOrDefault();
            }       
        }

        public IQueryable<TRecord> Query()
        {
            if (session == null)
                session = sessionService.CreateSession();

            using (session.BeginTransaction())
            {
                return session.Query<TRecord>();
            }
        }

        public IList<TRecord> QueryOver(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                pageNumber = 1;

            if (session == null)
                session = sessionService.CreateSession();

            using (session.BeginTransaction())
            {
                return session.QueryOver<TRecord>()
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .List();
            }
        }

        public IQueryable<TRecord> Query(int total)
        {
            if (session == null)
                session = sessionService.CreateSession();

            using (session.BeginTransaction())
            {
                return session.Query<TRecord>()
                    .Take(total);
            }
        }
    }
}
