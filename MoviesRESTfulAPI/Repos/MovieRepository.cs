using MoviesRESTfulAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MoviesRESTfulAPI.Repository
{
    public class MovieRepository : Rpository<Movie, int>, IMovieRepository
    {
        public MovieRepository(ServiceContext _context) : base(_context)
        { }

        public bool Exist(int id)
        {
            var matchedMovie = Get(id);
            return matchedMovie != null;
        }

        public PageList<Movie> GetMovieInPage(MovieResourceParameters movieResource)
        {
            var orderedExtendedQuery = Entities
                .OrderBy(m => m.Name)
                .ThenBy(m => m.Rate).AsQueryable();


            if (!string.IsNullOrWhiteSpace(movieResource.Genre))
            {
                var genre = movieResource.Genre.ToLower();
                orderedExtendedQuery = orderedExtendedQuery.Where(m => m.Genre.ToLower() == genre);
            }

            if (!string.IsNullOrWhiteSpace(movieResource.rateop))
            {
                var rateopArray = movieResource.rateop.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);

                var opearion = rateopArray[0];
                int.TryParse(rateopArray[1], out int rate);
                if (rate > 0)
                {
                    switch (opearion)
                    {
                        case ">":
                            orderedExtendedQuery = orderedExtendedQuery
                                .Where(m => m.Rate > rate);
                            break;
                        case "<":
                            orderedExtendedQuery = orderedExtendedQuery
                                .Where(m => m.Rate < rate);
                            break;
                        case ">=":
                            orderedExtendedQuery = orderedExtendedQuery
                                .Where(m => m.Rate >= rate);
                            break;
                        case "<=":
                            orderedExtendedQuery = orderedExtendedQuery
                                .Where(m => m.Rate <= rate);
                            break;
                        case "=":
                            orderedExtendedQuery = orderedExtendedQuery
                                .Where(m => m.Rate == rate);
                            break;
                    }

                }
            }

            if (movieResource.Rate > 0)
            {
                orderedExtendedQuery = orderedExtendedQuery.Where(m => m.Rate == movieResource.Rate);
            }

            var page = orderedExtendedQuery
                .Skip(movieResource.PageNumber * movieResource.PageSize - (movieResource.PageSize))
                .Take(movieResource.PageSize)
                .ToList();

            return new PageList<Movie>(page, Entities.Count(), movieResource.PageNumber, movieResource.PageSize);
        }

        public IEnumerable<Movie> GetMoviesByIds(IEnumerable<int> ids)
        {
            List<Movie> movies = new List<Movie>();
            foreach (var id in ids)
            {
                var matched = GetAll().Where(m => m.Id == id).FirstOrDefault();
                if (matched != null)
                    movies.Add(matched);
            }
            return movies;
        }
    }
}
