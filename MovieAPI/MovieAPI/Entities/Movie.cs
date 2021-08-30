using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Entities
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Drirector { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
