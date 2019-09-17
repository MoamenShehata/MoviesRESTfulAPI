using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoviesRESTfulAPI.DTOs.Actor
{
    public class GetActorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int DateOfBirth { get; set; }
        public string Movie { get; set; }
        public int MovieId { get; set; }
    }
}