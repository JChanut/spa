using Qoveo.Impact.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Bortosky.Google.Visualization;
using Newtonsoft.Json.Linq;
using Qoveo.Impact.Model;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Qoveo.Impact.Helper;
using System.Net;
using System.Collections;

namespace Qoveo.Impact.Controllers
{
    public class StatsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/stats/all
        /// <summary>
        /// Return all the statistics in the Google JSON Data Table format
        /// see : https://google-developers.appspot.com/chart/interactive/docs/reference#DataTable
        /// </summary>
        /// <returns>A Json Datable for google chart</returns>
        [ActionName("all")]
        public JObject GetStats()
        {
            var dt = GetStatDTOsForAdmin().ToDataTable();

            return JObject.Parse(new GoogleDataTable(dt).GetJson()) as JObject;
        }
        // GET api/stats/all?tutorId=1
        /// <summary>
        /// Return all the statistics for a tutor in the Google JSON Data Table format
        /// </summary>
        /// <param name="tutorId">Id of the tutor</param>
        /// <returns>A Json Datable for google chart</returns>
        [ActionName("all")]
        public JObject GetStatsByTutor(int tutorId)
        {
            var dt = GetStatDTOsForTutor(tutorId).ToDataTable();

            return JObject.Parse(new GoogleDataTable(dt).GetJson()) as JObject;
        }

        // GET api/stats/survey
        /// <summary>
        /// Return all the surveys statistic in the google JSON Data Table format
        /// </summary>
        /// <returns></returns>
        [ActionName("survey")]
        public JObject GetSurveyResults()
        {
            var dt = GetSurveyDTOsForAdmin().ToDataTable();

            return JObject.Parse(new GoogleDataTable(dt).GetJson()) as JObject;
        }

        // GET api/stats/survey?tutorId=1
        /// <summary>
        /// Return all the surveys statistic for a tutor in the google JSON Data Table format
        /// </summary>
        /// <param name="tutorId">Id of the tutor</param>
        /// <returns></returns>
        [ActionName("survey")]
        public JObject GetSurveyResultsByTutor(int tutorId)
        {
            var dt = GetSurveyDTOsForTutor(tutorId).ToDataTable();

            return JObject.Parse(new GoogleDataTable(dt).GetJson()) as JObject;
        }

        // GET api/stats/csv
        /// <summary>
        /// Return all the statistics in the CSV file format
        /// </summary>
        /// <returns>A CSV file</returns>
        [ActionName("csv")]
        public HttpResponseMessage GetStatAsCsv()
        {
            var stats = GetStatDTOsForAdmin().OrderBy(stat => stat.MissionId);
            var resp = new HttpResponseMessage()
            {
                Content = new StringContent(stats.ToCsv() , Encoding.Unicode),
                StatusCode = HttpStatusCode.OK
            };

            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            resp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "export.csv"};
            return resp;
        }
        
        // GET api/stats/csv?tutorId=1
        /// <summary>
        /// Return the statistics for a tutor in the CSV file format
        /// </summary>
        /// <param name="tutorId">Id of the tutor</param>
        /// <returns></returns>
        [ActionName("csv")]
        public HttpResponseMessage GetTutorStatAsCsv(int tutorId)
        {
            var stats = GetStatDTOsForTutor(tutorId).OrderBy(stat => stat.TeamId);
            var resp = new HttpResponseMessage()
            {
                Content = new StringContent(stats.ToCsv(), Encoding.Unicode),
                StatusCode = HttpStatusCode.OK
            };

            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            resp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "export.csv" };
            return resp;
        }

        // GET api/stats/surveycsv
        /// <summary>
        /// Return all the survey statistics in the CSV file format
        /// </summary>
        /// <returns>A Csv File</returns>
        [ActionName("surveycsv")]
        public HttpResponseMessage GetSurveyAsCsv()
        {
            var stats = GetSurveyDTOsForAdmin().OrderBy(stat => stat.MissionId);

            var resp = new HttpResponseMessage()
            {
                Content = new StringContent(stats.ToCsv(), Encoding.Unicode),
                StatusCode = HttpStatusCode.OK
            };

            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            resp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "export.csv" };
            return resp;
        }

        // GET api/stats/surveycsv?tutorId=1
        /// <summary>
        /// Return all the survey for a tutor in the CSV file format
        /// </summary>
        /// <param name="tutorId">Id of the tutor</param>
        /// <returns></returns>
        [ActionName("surveycsv")]
        public HttpResponseMessage GetTutorSurveyAsCsv(int tutorId)
        {
            var stats = GetSurveyDTOsForTutor(tutorId).OrderBy(stat => stat.TeamId);
            var resp = new HttpResponseMessage()
            {
                Content = new StringContent(stats.ToCsv(), Encoding.Unicode),
                StatusCode = HttpStatusCode.OK
            };

            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            resp.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "export.csv" };
            return resp;
        }
        
        private IEnumerable<StatDTO> GetStatDTOsForTutor(int tutorId)
        {
            var tutor = _unitOfWork.TutorRepository.Get(t => t.Id == tutorId).FirstOrDefault();

            if (tutor == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            var result = _unitOfWork.ResultRepository.Get(
                r => r.Team.ClusterId == tutor.ClusterId && r.Team.SessionId == tutor.SessionId, 
                includeProperties: "Team, Mission, Team.Cluster, Team.Session");

            return GetStatDTOs(result);
        }

        private IEnumerable<StatDTO> GetStatDTOsForAdmin()
        {
            var result = _unitOfWork.ResultRepository.Get(includeProperties: "Team, Mission, Team.Cluster, Team.Session");

            return GetStatDTOs(result);
        }

        private IEnumerable<SurveyDTO> GetSurveyDTOsForAdmin()
        {
            var result = _unitOfWork.SurveyRepository.Get(includeProperties: "Team, Mission, Team.Cluster, Team.Session");
            return GetSurveyDTOs(result);
        }

        private IEnumerable<SurveyDTO> GetSurveyDTOsForTutor(int tutorId)
        {
            var tutor = _unitOfWork.TutorRepository.Get(t => t.Id == tutorId).FirstOrDefault();

            if (tutor == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            var result = _unitOfWork.SurveyRepository.Get(
                r => r.Team.ClusterId == tutor.ClusterId && r.Team.SessionId == tutor.SessionId,
                includeProperties: "Team, Mission, Team.Cluster, Team.Session");

            return GetSurveyDTOs(result);

        }

        private IEnumerable<StatDTO> GetStatDTOs(IEnumerable<Result> result)
        {
            var stats = from r in result
                        select new StatDTO()
                        {
                            Team = r.Team.Name,
                            Mission = r.Mission.Name,
                            Cluster = r.Team.Cluster.Name,
                            Session = r.Team.Session.Name,
                            TeamId = r.TeamId,
                            MissionId = r.MissionId,
                            ClusterId = r.Team.Cluster.Id,
                            SessionId = r.Team.Session.Id,
                            Jauge1 = r.Jauge1,
                            Jauge2 = r.Jauge2,
                            Jauge3 = r.Jauge3,
                            Jauge4 = r.Jauge4,
                            Jauge5 = r.Jauge5,
                            ScoreEpreuve1 = r.ScoreEpreuve1,
                            ScoreEpreuve2 = r.ScoreEpreuve2,
                            ScoreEpreuve3 = r.ScoreEpreuve3,
                            GlobalScore = r.GlobalScore
                        };
            return stats;
        }

        private IEnumerable<SurveyDTO> GetSurveyDTOs(IEnumerable<Survey> result)
        {
            var stats = from r in result
                       select new SurveyDTO()
                       {
                           SessionId = r.Team.SessionId,
                           TeamId = r.TeamId,
                           ClusterId = r.Team.Cluster.Id,
                           MissionId = r.MissionId,
                           Session = r.Team.Session.Name,
                           Team = r.Team.Name,
                           Cluster = r.Team.Cluster.Name,
                           Mission = r.Mission.Name,
                           Question = r.Question,
                           Answer = r.Answer
                       };

            return stats;
        }
    }
}
