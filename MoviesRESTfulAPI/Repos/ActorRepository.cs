using System.Collections.Generic;
using MoviesRESTfulAPI.Models;
using System.Linq;

namespace MoviesRESTfulAPI.Repository
{
    class ActorRepository : Rpository<Actor, int>, IActorRepository
    {
        public ActorRepository(ServiceContext _context) : base(_context)
        { }

        public bool Exist(int id)
        {
            var matchedMovie = Get(id);
            return matchedMovie != null;
        }

        public IEnumerable<Actor> GetActorsForMovie(int movieId) => GetAll().Where(ac => ac.MovieId == movieId);

        public Actor GetActorForMovie(int movieId, int actorId) => GetActorsForMovie(movieId).Where(ac => ac.Id == actorId).FirstOrDefault();


    }
}
