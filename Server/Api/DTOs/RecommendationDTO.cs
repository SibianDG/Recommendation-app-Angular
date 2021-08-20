using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.DTOs
{
    public class RecommendationDTO
    {
        [Range(0, 1, ErrorMessage = "The range of the rating should be between 0 and 1 (borders included)")]
        public double? Rating { get; set; }
        
        [Required(ErrorMessage = "{0} is required.")]
        public IList<ItemDTO> Items { get; set; }
        
        [Required(ErrorMessage = "{0} is required.")]
        public string[] Keywords { get; set; }
    }
}