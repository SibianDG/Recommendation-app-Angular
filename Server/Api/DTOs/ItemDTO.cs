using System.ComponentModel.DataAnnotations;

namespace Api.DTOs
{
    public class ItemDTO
    {
        [Required(ErrorMessage = "{0} is required.", AllowEmptyStrings = false)]
        [MinLength(2, ErrorMessage = "{0} must have at least {1} characters.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "{0} is required.", AllowEmptyStrings = false)]
        [MinLength(5, ErrorMessage = "{0} must have at least {1} characters.")]
        public string Summary { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string TypeName { get; set;}
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
        public int? NumberOfEpisodes { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
        public int? Pages { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed.")]
        public int? Duration { get; set; }
    }
}