using MoviesRESTfulAPI.DTOs.Movie;
using MoviesRESTfulAPI.Factory;
using MoviesRESTfulAPI.Filters;
using MoviesRESTfulAPI.Model_Binders;
using MoviesRESTfulAPI.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace MoviesRESTfulAPI.Controllers
{
    [RoutePrefix("api/moviecollection")]
    public class MovieCollectionController : ApiController
    {
        private UnitOfWork UOW = new UnitOfWork(new Models.ServiceContext());

        [HttpGet]
        [Route("({ids})", Name = "GetMovieCollection")]
        public IHttpActionResult GetMovieCollection([ModelBinder(binderType: typeof(IEnumerableModelBinder<int>))] IEnumerable<int> ids)
        {
            if (ids == null)
                return BadRequest("The Collection Must Contain At Least One Entity");

            var movies = UOW.Movies.GetMoviesByIds(ids);

            if (movies.Count() != ids.Count())
                return NotFound();

            var moviesDTOs = Mapper.MoviesDtosFromMovies(movies);
            return Ok(moviesDTOs);
        }

        [HttpPost]
        [Route("")]
        [ValidatorFilter]
        public IHttpActionResult AddMovieCollection([FromBody]IEnumerable<PostMovieDTO> movieDtos)
        {
            if (movieDtos == null)
                return BadRequest("The Collection Must Contain At Least One Entity");

            var movies = Mapper.MoviesFromMoviesDtos(movieDtos).ToList();

            foreach (var movie in movies)
            {
                UOW.Movies.Add(movie);
            }
            if (!UOW.Complete())
            {
                return StatusCode(System.Net.HttpStatusCode.InternalServerError);
            }

            var getMovieDTos = Mapper.MoviesDtosFromMovies(movies);
            var idsAsString = string.Join(",", getMovieDTos.Select(m => m.Id));

            return CreatedAtRoute("GetMovieCollection", new { ids = idsAsString }, getMovieDTos);
        }
    }
}
