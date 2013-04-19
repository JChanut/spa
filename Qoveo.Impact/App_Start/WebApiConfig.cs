using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Qoveo.Impact.Filters;
using System.Web.Http.Description;
using Qoveo.Impact.Areas.HelpPage;

namespace Qoveo.Impact
{
    public static class WebApiConfig
    {
        public static string ControllerOnly = "ApiControllerOnly";
        public static string ControllerAndId = "ApiControllerAndIntegerId";
        public static string ControllerAction = "ApiControllerAction";


        public static void Register(HttpConfiguration config)
        {
            // This controller-per-type route is ideal for GetAll calls.
            // It finds the method on the controller using WebAPI conventions
            // The template has no parameters.
            //
            // ex: api/sessionbriefs
            // ex: api/sessions
            // ex: api/persons
            config.Routes.MapHttpRoute(
                name: ControllerOnly,
                routeTemplate: "api/{controller}"
            );


            // This is the default route that a "File | New MVC 4 " project creates.
            // (I changed the name, removed the defaults, and added the constraints)
            //
            // This controller-per-type route lets us fetch a single resource by numeric id
            // It finds the appropriate method GetById method
            // on the controller using WebAPI conventions
            // The {id} is not optional, must be an integer, and 
            // must match a method with a parameter named "id" (case insensitive)
            //
            //  ex: api/sessions/1
            //  ex: api/persons/1
            config.Routes.MapHttpRoute(
                name: ControllerAndId,
                routeTemplate: "api/{controller}/{id}",
                defaults: null, //defaults: new { id = RouteParameter.Optional } //,
                constraints: new { id = @"^\d+$" } // id must be all digits
            );

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            /********************************************************
            * The integer id constraint is necessary to distinguish 
            * the {id} route above from the {action} route below.
            * For example, the route above handles
            *     "api/sessions/1" 
            * whereas the route below handles
            *     "api/lookups/all"
            ********************************************************/

            // This RPC style route is great for lookups and custom calls
            // It matches the {action} to a method on the controller 
            //
            // ex: api/lookups/all
            // ex: api/lookups/rooms
            config.Routes.MapHttpRoute(
                name: ControllerAction,
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            //This code sets the JSON formatter to preserve object references, and removes the XML formatter from the pipeline entirely
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling =
                Newtonsoft.Json.PreserveReferencesHandling.Objects;

            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Add model validation, globally
            config.Filters.Add(new ValidationActionFilter());

            // To disable tracing in your application, please comment out or remove the following line of code
            // For more information, refer to: http://www.asp.net/web-api
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
