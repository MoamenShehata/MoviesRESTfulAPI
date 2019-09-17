using MoviesRESTfulAPI.Models;
using System.Collections.Generic;

namespace MoviesRESTfulAPI.Repository
{
    public interface IActorRepository : IRpository<Actor, int>
    {
        bool Exist(int id);
        IEnumerable<Actor> GetActorsForMovie(int movieId);
        Actor GetActorForMovie(int movieId, int actorId);
    }
}
