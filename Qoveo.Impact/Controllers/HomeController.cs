using Qoveo.Impact.Data;
using Qoveo.Impact.Model;
using Qoveo.Impact.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Qoveo.Impact.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly int _numberOfTeams = 60;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.TeamList = GetTeamList();
            if (User.Identity.IsAuthenticated && User.IsInRole("Team"))
            {
                ViewBag.TeamId = _unitOfWork.TeamRepository.Get(t => t.Login == User.Identity.Name).FirstOrDefault().Id;
            }
            if (User.Identity.IsAuthenticated && User.IsInRole("Tutor"))
            {
                ViewBag.TutorId = _unitOfWork.TutorRepository.Get(t => t.Login == User.Identity.Name).FirstOrDefault().Id;
            }

            return View();
        }

        private IEnumerable<SelectListItem> GetTeamList()
        {
            for (int i = 1; i <= _numberOfTeams; i++)
            {
                yield return new SelectListItem{ Text = "Team " + i, Value = "Team " + i };
            }
        }
    }
}