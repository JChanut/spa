using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Qoveo.Impact.Data;
using Qoveo.Impact.Model;
using System.Data.Entity.Infrastructure;


namespace Qoveo.Impact.Controllers
{
    public class ClusterController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ClusterController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/cluster
        /// <summary>
        /// Return the list of all clusters
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Cluster> Get()
        {
            return _unitOfWork.ClusterRepository.Get();
        }

        // GET api/cluster/5
        /// <summary>
        /// Return a cluster
        /// </summary>
        /// <param name="id">The value of Id</param>
        /// <returns></returns>
        public Cluster Get(int id)
        {
            Cluster cluster = _unitOfWork.ClusterRepository.Get(u => u.Id == id).FirstOrDefault();
            if (cluster == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return cluster;
        }

        // GET api/cluster/tutor?tutorId=1
        /// <summary>
        /// Return the list of clusters for a tutor
        /// </summary>
        /// <param name="tutorId">The tutor id</param>
        /// <returns></returns>
        [ActionName("tutor")]
        public IEnumerable<Cluster> GetByTutor(int tutorId)
        {
            var tutor = _unitOfWork.TutorRepository.Get(t => t.Id == tutorId).FirstOrDefault();
            IEnumerable<Cluster> result;
            if (tutor == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            result = _unitOfWork.ClusterRepository.Get(c => c.Id == tutor.ClusterId);
            return result;
        }

        // PUT api/cluster/5
        /// <summary>
        /// Update a cluster
        /// </summary>
        /// <param name="id">The value of Id</param>
        /// <param name="cluster">The cluster object</param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, Cluster cluster)
        {
            if (id == cluster.Id)
            {
                _unitOfWork.ClusterRepository.Update(cluster);
                try
                {
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, cluster);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
