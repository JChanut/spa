using Qoveo.Impact.Data;
using Qoveo.Impact.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using WebMatrix.WebData;

namespace Qoveo.Impact.Controllers
{
    public class TutorController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public TutorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/tutor
        /// <summary>
        /// Return the list of all tutors
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Tutor> Get()
        {
            return _unitOfWork.TutorRepository.Get();
        }

        // GET api/tutor/5
        /// <summary>
        /// Return a mission
        /// </summary>
        /// <param name="id">The value of Id</param>
        /// <returns></returns>
        public Tutor Get(int id)
        {
            Tutor tutor = _unitOfWork.TutorRepository.Get(u => u.Id == id).FirstOrDefault();
            if (tutor == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return tutor;
        }

        // PUT api/tutor/5
        /// <summary>
        /// Update a session
        /// </summary>
        /// <param name="id">The value of Id</param>
        /// <param name="tutor">The tutor object</param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, Tutor tutor)
        {
            if (id == tutor.Id)
            {
                _unitOfWork.TutorRepository.Update(tutor);
                try
                {
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK, tutor);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }


        // POST api/tutor
        /// <summary>
        /// Insert a tutor
        /// </summary>
        /// <param name="tutor">The tutor object</param>
        /// <returns></returns>
        public HttpResponseMessage Post(Tutor tutor)
        {
            // login = concatene first name and name
            tutor.Login = (tutor.FirstName + tutor.Name).Replace(" ", "");
            tutor.Password = Helpers.PasswordHelper.GeneratePassword();

            _unitOfWork.TutorRepository.Add(tutor);
            
            // We need to create the security account for this session
            try
            {
                WebSecurity.CreateUserAndAccount(tutor.Login, tutor.Password);
                Roles.AddUserToRole(tutor.Login, "Tutor");
            }
            catch (MembershipCreateUserException e)
            {
                Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }

            _unitOfWork.Save();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, tutor);
            response.Headers.Location = new Uri(Url.Link("ApiControllerAndIntegerId", new { id = tutor.Id }));
            return response;
        }
    }
}
