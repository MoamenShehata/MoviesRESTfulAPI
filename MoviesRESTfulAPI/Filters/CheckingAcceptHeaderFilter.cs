using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MoviesRESTfulAPI.Filters
{
    public class CheckingAcceptHeaderFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var requestedAcceptHeaders = new List<MediaTypeHeaderValue>();
            foreach (var acceptType in actionContext.Request.Headers.Accept)
            {
                requestedAcceptHeaders.Add(acceptType);
                Debug.WriteLine(acceptType.MediaType);
            }

            if (requestedAcceptHeaders.Count == 1)
            {
                Type type = requestedAcceptHeaders[0].GetType();
                foreach (var formatter in actionContext.RequestContext.Configuration.Formatters)
                {
                    
                }
                
            }
        }
    }
}