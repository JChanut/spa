using Qoveo.Impact.Data;
using Qoveo.Impact.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using WebMatrix.WebData;

namespace Qoveo.Impact.Controllers
{
    public class SessionController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly int _numberOfTeams = 60;

        public SessionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/session
        /// <summary>
        /// Return the list of all sessions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Session> Get()
        {
            return _unitOfWork.SessionRepository.Get();
        }

        // GET api/session/tutor?tutorId=1
        /// <summary>
        /// Return the list of session for a tutor
        /// </summary>
        /// <param name="tutorId">The Id of tutor</param>
        /// <returns></returns>
        [ActionName("tutor")]
        public IEnumerable<Session> GetByTutor(int tutorId)
        {
            var tutor = _unitOfWork.TutorRepository.Get(t => t.Id == tutorId).FirstOrDefault();
            IEnumerable<Session> result;
            if (tutor == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            result = _unitOfWork.SessionRepository.Get(s => s.Id == tutor.SessionId);
            return result;
        }

        // GET api/session/5
        /// <summary>
        /// Return a session
        /// </summary>
        /// <param name="id">The value of Id</param>
        /// <returns></returns>
        public Session Get(int id)
        {
            Session session = _unitOfWork.SessionRepository.Get(u => u.Id == id).FirstOrDefault();
            if (session == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return session;
        }

        // PUT api/session/5
        /// <summary>
        /// Update a session
        /// </summary>
        /// <param name="id">The value of Id</param>
        /// <param name="session">The session object</param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, Session session)
        {
            if (id == session.Id)
            {
                _unitOfWork.SessionRepository.Update(session);
                try
                {
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, session);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/session
        /// <summary>
        /// Insert a session
        /// </summary>
        /// <param name="session">The session object</param>
        /// <returns></returns>
        public HttpResponseMessage Post(Session session)
        {
            _unitOfWork.SessionRepository.Add(session);

            // We need to create new teams for this session
            session.TeamList = new Collection<Team>();
            for (int i = 1; i <= _numberOfTeams; i++)
            {
                var teamName = "Team " + i;
                var teamLogin = (teamName + session.Name).Replace(" ", "");
                var password = Helpers.PasswordHelper.GeneratePassword();
                try
                {
                    // Il faut créer l'user pour que le groupe puisse se connecter à l'application
                    WebSecurity.CreateUserAndAccount(teamLogin, password);
                    Roles.AddUserToRole(teamLogin, "Team");
                }
                catch (MembershipCreateUserException e)
                {
                    Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
                }
                session.TeamList.Add(new Team() { Name = teamName, Login = teamLogin, Password = password });
            }

            _unitOfWork.Save();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, session);
            response.Headers.Location = new Uri(Url.Link("ApiControllerAndIntegerId", new { id = session.Id }));
            return response;
        }
    }
}
