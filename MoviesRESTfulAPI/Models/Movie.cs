using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesRESTfulAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public TimeSpan Length { get; set; }
        public string Genre { get; set; }
        public List<Actor> Actors { get; set; }

    }
}
