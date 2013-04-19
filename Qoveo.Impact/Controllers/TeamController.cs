using Qoveo.Impact.Data;
using Qoveo.Impact.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Qoveo.Impact.Controllers
{
    public class TeamController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public TeamController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/team
        /// <summary>
        /// Return the list of all teams
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Team> Get()
        {
            return _unitOfWork.TeamRepository.Get();
        }

        // GET api/team/tutor?tutorId=1
        /// <summary>
        /// Return the list of teams for a tutor
        /// </summary>
        /// <param name="tutorId">The tutor Id</param>
        /// <returns></returns>
        [ActionName("tutor")]
        public IEnumerable<Team> GetByTutor(int tutorId)
        {
            var tutor = _unitOfWork.TutorRepository.Get(t => t.Id == tutorId).FirstOrDefault();
            IEnumerable<Team> result;
            if (tutor == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            result = _unitOfWork.TeamRepository.Get(t => t.SessionId == tutor.SessionId);
            return result;
        }

        /// <summary>
        /// Update a team
        /// </summary>
        /// <param name="id">The value of the Id</param>
        /// <param name="team">The team object</param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, Team team)
        {
            if (id == team.Id)
            {
                _unitOfWork.TeamRepository.Update(team);
                try
                {
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, team);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // GET api/team/5
        /// <summary>
        /// Return a team
        /// </summary>
        /// <param name="id">The value of Id</param>
        /// <returns></returns>
        public Team Get(int id)
        {
            Team team = _unitOfWork.TeamRepository.Get(u => u.Id == id, includeProperties: "Session, ResultList, Cluster").FirstOrDefault();
            if (team == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return team;
        }
    }
}
