using MoviesRESTfulAPI.DTOs.Actor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoviesRESTfulAPI.DTOs.Movie
{
    public class PostMovieDTO
    {
        [Required(ErrorMessage = "Name Field Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Rate Field Is Required")]
        public int Rate { get; set; }

        [Required(ErrorMessage = "Length Field Is Required")]
        public TimeSpan Length { get; set; }

        [Required(ErrorMessage = "Genre Field Is Required")]
        public string Genre { get; set; }

        public List<PostActorDTO> Actors
        { get; set; } = new List<PostActorDTO>();
    }
}