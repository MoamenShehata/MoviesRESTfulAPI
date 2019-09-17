using System.Data.Entity;

namespace MoviesRESTfulAPI.Models
{
    public class ServiceContext : DbContext
    {
        public ServiceContext()
            : base("MoviesDBAppContext")
        {
        }
       
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
    }
}
