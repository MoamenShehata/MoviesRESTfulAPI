using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Results;

namespace MoviesRESTfulAPI.Extension_Mehtods
{
    public static class IHttpStatusCodeWithResult
    {
        public static IHttpActionResult Response<TContent>(this ApiController caller, HttpStatusCode statusCode, TContent content)
        {
            var response = new HttpResponseMessage(statusCode)
            {
                Content = new ObjectContent(typeof(TContent), content, new JsonMediaTypeFormatter())
            };
            return new ResponseMessageResult(response);
        }

    }
}