using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Qoveo.Impact.Data;
using System.Configuration;
using Qoveo.Impact.Controllers;

namespace Qoveo.Impact
{
    public static class DependencyConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var unity = new UnityContainer();

            unity.RegisterType<ClusterController>();
            unity.RegisterType<TeamController>();
            unity.RegisterType<SessionController>();
            unity.RegisterType<SurveyController>();
            unity.RegisterType<TutorController>();
            unity.RegisterType<ResultController>();
            unity.RegisterType<MissionController>();
            unity.RegisterType<AccountController>();
            unity.RegisterType<HomeController>();
            unity.RegisterType<StatsController>();

            unity.RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager(),
                new InjectionConstructor(ConfigurationManager.ConnectionStrings["Impact"].ToString()));

            // Pour les controllers Web Api
            config.DependencyResolver = new IoCContainer(unity);

            // Pour les controllers Mvc
            System.Web.Mvc.DependencyResolver.SetResolver(new MvcIoCContainer(unity));

        }
    }
}