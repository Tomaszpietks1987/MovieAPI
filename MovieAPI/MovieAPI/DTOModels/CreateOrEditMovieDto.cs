using System.ComponentModel.DataAnnotations;


namespace MovieAPI.DTOModels
{
    public class CreateOrEditMovieDto
    {
        [Required]
        [StringLength(25, ErrorMessage = "Drirector Name cannot be longer than 25 characters.")]
        public string Drirector { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Title movie cannot be longer than 100 characters.")]
        public string Title { get; set; }
    }
}
