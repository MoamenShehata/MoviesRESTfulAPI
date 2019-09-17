using MoviesRESTfulAPI.Models;
using System.Collections.Generic;

namespace MoviesRESTfulAPI.Repository
{
    public interface IMovieRepository : IRpository<Movie, int>
    {
        bool Exist(int id);

        IEnumerable<Movie> GetMoviesByIds(IEnumerable<int> ids);
        PageList<Movie> GetMovieInPage(MovieResourceParameters movieResource);



    }
}
