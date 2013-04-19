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
    public class MissionController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public MissionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/mission
        /// <summary>
        /// Return the list of all missions
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mission> Get()
        {
            return _unitOfWork.MissionRepository.Get();
        }

        // GET api/mission/5
        /// <summary>
        /// Return a mission
        /// </summary>
        /// <param name="id">The value of Id</param>
        /// <returns></returns>
        public Mission Get(int id)
        {
            Mission mission = _unitOfWork.MissionRepository.Get(u => u.Id == id).FirstOrDefault();
            if (mission == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return mission;
        }

        // PUT api/mission/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">The value of Id</param>
        /// <param name="mission">The mission Object</param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, Mission mission)
        {
            if (id == mission.Id)
            {
                _unitOfWork.MissionRepository.Update(mission);

                try
                {
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK, mission);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
