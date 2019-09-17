using MoviesRESTfulAPI.DTOs.Actor;
using MoviesRESTfulAPI.Factory;
using MoviesRESTfulAPI.Filters;
using MoviesRESTfulAPI.Repository;
using System.Net;
using System.Web.Http;

namespace MoviesRESTfulAPI.Controllers
{

    [RoutePrefix("api/movies/{movieId}/actors")]
    public class ActorController : ApiController
    {
        private UnitOfWork UOW = new UnitOfWork(new Models.ServiceContext());

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetActorsForMovie(int movieId)
        {
            if (!UOW.Movies.Exist(movieId))
                return NotFound();

            var actorsForMovie = UOW.Actors.GetActorsForMovie(movieId);

            var actorsForMovieDTos = Mapper.ActorsDtosFromActors(actorsForMovie);
            return Ok(actorsForMovieDTos);
        }

        [HttpGet]
        [Route("{actorId}", Name = "GetActorById")]
        public IHttpActionResult GetActorForMovie(int movieId, int actorId)
        {
            if (!UOW.Movies.Exist(movieId))
                return NotFound();

            var actor = UOW.Actors.GetActorForMovie(movieId, actorId);
            if (actor == null)
                return NotFound();

            var actorForMovieDTo = Mapper.ActorDtoFromActor(actor);
            return Ok(actorForMovieDTo);
        }

        [HttpPost]
        [Route("")]
        [ValidatorFilter]
        public IHttpActionResult AddActor(int movieId, [FromBody] PostActorDTO actorDTO)
        {
            if (!UOW.Movies.Exist(movieId))
                return NotFound();

            var actor = Mapper.ActorFromPostActorDto(actorDTO);

            actor.MovieId = movieId;
            UOW.Actors.Add(actor);
            if (!UOW.Complete())
                return StatusCode(HttpStatusCode.InternalServerError);

            var getActorDTO = Mapper.ActorDtoFromActor(actor);

            return CreatedAtRoute("GetActorById", new { movieId = getActorDTO.MovieId, actorId = getActorDTO.Id }, getActorDTO);
        }

        [HttpPut]
        [Route("{actorId}")]
        [ValidatorFilter]
        public IHttpActionResult UpdateActorForMovie(int movieId, int actorId, [FromBody] PostActorDTO postActorDto)
        {
            if (!UOW.Movies.Exist(movieId))
                return NotFound();

            var actorForMovie = UOW.Actors.GetActorForMovie(movieId, actorId);
            if (actorForMovie == null)
                return NotFound();

            actorForMovie = Mapper.ActorFromPostActorDto(actorForMovie, postActorDto);

            if(!UOW.Complete())
                return StatusCode(HttpStatusCode.InternalServerError);

            return StatusCode(HttpStatusCode.NoContent);
        }

        //[HttpPatch]
        //[Route("{actorId}")]
        //[ValidatorFilter]
        //public IHttpActionResult PartiallyUpdateActorForMovie(int movieId, int actorId, [FromBody] JsonPatchDocument PostActorDTO postActorDto)
        //{
        //    if (!UOW.Movies.Exist(movieId))
        //        return NotFound();

        //    var actorForMovie = UOW.Actors.GetActorForMovie(movieId, actorId);
        //    if (actorForMovie == null)
        //        return NotFound();

        //    actorForMovie = Mapper.ActorFromPostActorDto(actorForMovie, postActorDto);

        //    if (!UOW.Complete())
        //        return StatusCode(HttpStatusCode.InternalServerError);

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        [HttpDelete]
        [Route("{actorId}")]
        public IHttpActionResult DeleteActorForMovie(int movieId, int actorId)
        {
            if (!UOW.Movies.Exist(movieId))
                return NotFound();

            var actorForMovie = UOW.Actors.GetActorForMovie(movieId, actorId);
            if (actorForMovie == null)
                return NotFound();

            UOW.Actors.Remove(actorForMovie);
            if (!UOW.Complete())
                return StatusCode(HttpStatusCode.InternalServerError);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
