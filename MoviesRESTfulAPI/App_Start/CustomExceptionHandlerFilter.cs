//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Http.ExceptionHandling;
//using System.Web.Http.Filters;
////using System.Web.Mvc;

//namespace MoviesRESTfulAPI.App_Start
//{
//    public class CustomExceptionHandlerFilter : FilterAttribute, IExceptionFilter
//    {
//        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
//        {
//            System.IO.Stream stream = new System.IO.MemoryStream();
//            System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);
//            sw.Write("Something went wrong!");

//            actionExecutedContext.Response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
//            actionExecutedContext.Response.Content = new StreamContent(stream);
//            return new Task(() => { });
//        }

//        public void OnException(ExceptionContext filterContext)
//        {
//            filterContext.Result = new HttpStatusCodeResult(500);
//        }
//    }
//}