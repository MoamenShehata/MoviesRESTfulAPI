using MoviesRESTfulAPI.DTOs.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesRESTfulAPI.DTOs.Movie
{
    public class GetMovieDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public string Length { get; set; }
        public string Genre { get; set; }

        public List<GetActorDTO> Actors
        { get; set; } = new List<GetActorDTO>();
    }
}