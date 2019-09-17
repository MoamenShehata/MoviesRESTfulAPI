using MoviesRESTfulAPI.DTOs.Movie;
using MoviesRESTfulAPI.Factory;
using MoviesRESTfulAPI.Filters;
using MoviesRESTfulAPI.Models;
using MoviesRESTfulAPI.Repository;
using MoviesRESTfulAPI.Extension_Mehtods;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;


namespace MoviesRESTfulAPI.Controllers
{
    [RoutePrefix("api/movies")]
    [CheckingAcceptHeaderFilter]
    public class MoviesController : ApiController
    {
        private UnitOfWork UOW = new UnitOfWork(new Models.ServiceContext());
        private UrlHelper urlHelper = new UrlHelper();

        [HttpGet]
        [Route("", Name = "GetMoviesInPage")]
        public IHttpActionResult GetMovies([FromUri] MovieResourceParameters parameters)
        {
            if (parameters == null)
                parameters = new MovieResourceParameters();

            var page = UOW.Movies.GetMovieInPage(parameters);

            var currentLink = CreateMoviePageURI(parameters, PageState.CurrentPage);

            var previousLink = page.HasPrevious ? CreateMoviePageURI(parameters, PageState.PreviousPage) : null;

            var nextLink = page.HasNext ? CreateMoviePageURI(parameters, PageState.NextPage) : null;

            var locationHeader = new
            {
                totalCount = page.TotalCount,
                currntPageNumber = page.CurrentPage,
                pageSize = page.PageSize,
                countInPage = page.PageCount,
                currentPageLink = currentLink,
                previousPageLink = previousLink,
                nextPageLink = nextLink,
            };
            //var movies = UOW.Movies.GetAll();

            var movieDTos = Mapper.MoviesDtosFromMovies(page);

            ActionContext.Response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ObjectContent(typeof(IEnumerable<GetMovieDTO>), movieDTos, ActionContext.ActionDescriptor.Configuration.Formatters.JsonFormatter) };

            ActionContext.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(locationHeader));

            return ResponseMessage(ActionContext.Response);

        }

        [HttpGet]
        [Route(template: "{id}", Name = "GetMovieById")]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = UOW.Movies.Get(id);
            if (movie == null)
                return this.Response(HttpStatusCode.NotFound, $"The Movie With ( Id = {id} ) Was Not Found!");
            //return NotFound();

            var movieDTo = Mapper.MovieDtoFromMovie(movie);

            return Ok(movieDTo);
        }



        [HttpPost]
        [Route("")]
        [ValidatorFilter]
        public IHttpActionResult AddMovie([FromBody]PostMovieDTO movieDto)
        {
            var movie = Mapper.MovieFromPostMovieDto(movieDto);

            UOW.Movies.Add(movie);
            if (!UOW.Complete())
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError);
            }

            var getMovieDTo = Mapper.MovieDtoFromMovie(movie);

            return CreatedAtRoute("GetMovieById", new { id = getMovieDTo.Id }, getMovieDTo);
        }



        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteActorForMovie(int id)
        {
            var movie = UOW.Movies.Get(id);
            if (movie == null)
                return NotFound();

            UOW.Movies.Remove(movie);
            if (!UOW.Complete())
                return StatusCode(HttpStatusCode.InternalServerError);

            return StatusCode(HttpStatusCode.NoContent);
        }

        private string CreateMoviePageURI(MovieResourceParameters parameters, PageState pageState)
        {
            urlHelper.Request = ActionContext.Request;
            switch (pageState)
            {
                case PageState.PreviousPage:
                    return urlHelper.Link("GetMoviesInPage", new
                    {
                        PageNumber = parameters.PageNumber - 1,
                        PageSize = parameters.PageSize
                    });
                case PageState.NextPage:
                    return urlHelper.Link("GetMoviesInPage", new
                    {
                        PageNumber = parameters.PageNumber + 1,
                        PageSize = parameters.PageSize
                    });
                case PageState.CurrentPage:
                    return urlHelper.Link("GetMoviesInPage", new
                    {
                        PageNumber = parameters.PageNumber,
                        PageSize = parameters.PageSize
                    });
                default:
                    return null;
            }
        }
    }
}