using System;
using System.Data.Entity;
using Qoveo.Impact.Model;

namespace Qoveo.Impact.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        protected DbContext Context { get; set; }

        public UnitOfWork(string connectionString)
        {
            if (connectionString == null)
                throw new NotImplementedException("connectionString");
            Context = new ImpactDbContext(connectionString);
            AllocateRepositories();
        }

        public UnitOfWork()
	    {
            Context = new DbContext("Impact");
            AllocateRepositories();
	    }

        public void Save()
        {
            Context.SaveChanges();
        }

        private void AllocateRepositories()
        {
            SessionRepository = new Repository<Session>(Context);
            ClusterRepository = new Repository<Cluster>(Context);
            MissionRepository = new Repository<Mission>(Context);
            ResultRepository = new Repository<Result>(Context);
            TeamRepository = new Repository<Team>(Context);
            SurveyRepository = new Repository<Survey>(Context);
            TutorRepository = new Repository<Tutor>(Context);
        }

        public IRepository<Session> SessionRepository { get; private set; }
        public IRepository<Cluster> ClusterRepository { get; private set; }
        public IRepository<Mission> MissionRepository { get; private set; }
        public IRepository<Result> ResultRepository { get; private set; }
        public IRepository<Team> TeamRepository { get; private set; }
        public IRepository<Survey> SurveyRepository { get; private set; }
        public IRepository<Tutor> TutorRepository { get; private set; }

        #region IDisposable Methods
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
