using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
using Qoveo.Impact.Models;
using System.Web.Security;
using Qoveo.Impact.Data;

namespace Qoveo.Impact.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class InitializeSimpleMembershipAttribute : ActionFilterAttribute
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Ensure ASP.NET Simple Membership is initialized only once per app start
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        private class SimpleMembershipInitializer
        {
            public SimpleMembershipInitializer()
            {
                Database.SetInitializer<ImpactDbContext>(null);

                try
                {
                    using (var context = new ImpactDbContext())
                    {
                        if (!context.Database.Exists())
                        {
                            // Create the SimpleMembership database without Entity Framework migration schema
                            ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                        }
                    }

                    WebSecurity.InitializeDatabaseConnection("Impact", "UserProfile", "UserId", "UserName", autoCreateTables: true);

                    initSystemRoles();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
                }
            }

            // Initialisation des rôles
            private void initSystemRoles()
            {
                //First User
                const string adminName = "admin";
                // Team Group login test
                const string teamName = "Team1Session1";

                string[] roles = new string[]{
                "Administrator",
                "Tutor",
                "Team"
             };

                foreach (string role in roles)
                {
                    if (!Roles.RoleExists(role))
                    {
                        Roles.CreateRole(role);
                    }

                    if (!WebSecurity.UserExists(adminName))
                    {
                        WebSecurity.CreateUserAndAccount(adminName, "Password!123");
                        Roles.AddUserToRole(adminName, "Administrator");
                    }

                    // Only create team login in Debug mode (for testing purpose)
#if DEBUG
                    if (!WebSecurity.UserExists(teamName) && role == "Team")
                    {
                        WebSecurity.CreateUserAndAccount(teamName, "test1");
                        Roles.AddUserToRole(teamName, "Team");
                    }
#endif
                }
            }
        }
    }
}
