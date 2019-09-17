namespace MoviesRESTfulAPI.Repository
{
    public interface IUnitOfWork
    {
        IMovieRepository Movies { get; }
        IActorRepository Actors { get; }
        bool Complete();
    }
}
