using Newtonsoft.Json.Linq;
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
    public class SurveyController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public SurveyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/survey
        /// <summary>
        /// Return the list of all surveys
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Survey> Get()
        {
            return _unitOfWork.SurveyRepository.Get();
        }

        // GET api/survey/?mid=2&tid=1
        /// <summary>
        /// Return the survey of a team for a mission
        /// </summary>
        /// <param name="mid">The value of MissionId</param>
        /// <param name="tid">The value of TeamId</param>
        /// <returns></returns>
        public Survey Get(int mid, int tid)
        {
            Survey survey = _unitOfWork.SurveyRepository.Get(u => u.MissionId == mid && u.TeamId == tid).FirstOrDefault();
            if (survey == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return survey;
        }

        // PUT api/survey/
        /// <summary>
        /// Update a survey
        /// </summary>
        /// <param name="survey">The survey object</param>
        /// <returns></returns>
        public HttpResponseMessage Put(Survey survey)
        {
            _unitOfWork.SurveyRepository.Update(survey);
            try
            {
                _unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, survey);
        }

        // POST api/survey
        /// <summary>
        /// Insert a new team's survey for a mission
        /// </summary>
        /// <param name="survey">The survey's list object</param>
        /// <returns></returns>
        public HttpResponseMessage Post(IEnumerable<Survey> surveyList)
        {
            foreach (var survey in surveyList)
            {
                _unitOfWork.SurveyRepository.Add(survey);
            }

            _unitOfWork.Save();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, surveyList);
            return response;
        }
    }
}
