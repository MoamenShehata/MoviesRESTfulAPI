using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesRESTfulAPI.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
    }
}