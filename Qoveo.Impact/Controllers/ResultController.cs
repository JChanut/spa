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
    public class ResultController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResultController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/result
        /// <summary>
        /// Return the list of all Results
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Result> Get()
        {
            return _unitOfWork.ResultRepository.Get();
        }

        // GET api/result/?mid=2&tid=1
        /// <summary>
        /// Return the result of a team for a mission
        /// </summary>
        /// <param name="mid">The value of MissionId</param>
        /// <param name="tid">The value of TeamId</param>
        /// <returns></returns>
        public Result Get(int mid, int tid)
        {
            Result result = _unitOfWork.ResultRepository.Get(u => u.MissionId == mid && u.TeamId == tid).FirstOrDefault();
            if (result == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return result;
        }

        // PUT api/result/
        /// <summary>
        /// Update a result
        /// </summary>
        /// <param name="result">The result object</param>
        /// <returns></returns>
        public HttpResponseMessage Put(Result result)
        {
            _unitOfWork.ResultRepository.Update(result);
            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // POST api/result
        /// <summary>
        /// Insert a new team's result for a mission
        /// </summary>
        /// <param name="result">The result object</param>
        /// <returns></returns>
        public HttpResponseMessage Post(Result result)
        {
            _unitOfWork.ResultRepository.Add(result);

            // Compose location header that tells how to get this attendance
            // e.g. ~/api/result/?mid=2&tid=1
            var queryString = string.Format(
                "?pid={0}&sid={1}", result.MissionId, result.TeamId);

            _unitOfWork.Save();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, result);
            response.Headers.Location = new Uri(Url.Link("ApiControllerOnly", null) + queryString);
            return response;
        }
    }
}
