using MoviesRESTfulAPI.Models;

namespace MoviesRESTfulAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ServiceContext context;

        public UnitOfWork(ServiceContext context)
        {
            this.context = context;
            Movies = new MovieRepository(context);
            Actors = new ActorRepository(context);
        }

        public IMovieRepository Movies { get; private set; }
        public IActorRepository Actors { get; private set; }

        public bool Complete()
        {
            return context.SaveChanges() > 0;
        }
    }
}
