using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MoviesRESTfulAPI.Filters
{
    public class ValidatorFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                string errors = null;
                foreach (var item in actionContext.ModelState.Values)
                {
                    foreach (var err in item.Errors)
                    {
                        errors += Environment.NewLine + err.ErrorMessage;
                    }
                }
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(errors)
                };
            }


            base.OnActionExecuting(actionContext);
        }
    }
}