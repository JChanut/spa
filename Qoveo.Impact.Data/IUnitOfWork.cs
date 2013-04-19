using System;
using Qoveo.Impact.Model;

namespace Qoveo.Impact.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Session> SessionRepository { get; }
        IRepository<Cluster> ClusterRepository { get; }
        IRepository<Mission> MissionRepository { get; }
        IRepository<Result> ResultRepository { get; }
        IRepository<Team> TeamRepository { get; }
        IRepository<Survey> SurveyRepository { get; }
        IRepository<Tutor> TutorRepository { get; }

        void Save();
    }
}
