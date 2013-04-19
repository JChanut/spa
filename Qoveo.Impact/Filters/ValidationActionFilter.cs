using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json.Linq;

namespace Qoveo.Impact.Filters
{
    public class ValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            var modelState = context.ModelState;
            if (!modelState.IsValid)
            {
                var errors = new JObject();
                foreach (var key in modelState.Keys)
                {
                    var state = modelState[key];
                    if (state.Errors.Any())
                    {
                        if (state.Errors.First().ErrorMessage.Length > 0)
                            errors[key] = state.Errors.First().ErrorMessage;
                    }
                }

                context.Response = context.Request.CreateResponse<JObject>(HttpStatusCode.BadRequest, errors);
            }
        }
    }
}