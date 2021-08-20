using Api.DTOs;
using Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public MovieController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        
        // [HttpGet("{id}")]
        // public ActionResult<Movie> GetItemById(int id)
        // {
        //     Movie movie = (Movie) _itemRepository.GetBy(id);
        //     if (movie == null) return NotFound();
        //     if (movie.GetType() != typeof(Movie)) return BadRequest();
        //     return movie;
        // }
        //
        // [HttpPost]
        // public ActionResult<Movie> PostItem(MovieDTO movie)
        // {
        //     Movie itemToCreate = new Movie (movie.Title, movie.Summary, movie.Image, movie.Url, movie.Duration);
        //     _itemRepository.Add(itemToCreate);
        //     _itemRepository.SaveChanges();
        //     
        //     return CreatedAtAction(nameof(GetItemById), new { id = itemToCreate.ItemId }, itemToCreate);
        // }
        
        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, Movie movie)
        {
            if (id != movie.ItemId)
            {
                return BadRequest();
            }
            _itemRepository.Update(movie);
            _itemRepository.SaveChanges();
            return NoContent();
        }
        
        // [HttpDelete("{id}")]
        // public IActionResult DeleteItem(int id)
        // {
        //     Movie movie = (Movie) _itemRepository.GetBy(id);
        //     if (movie == null)
        //     {
        //         return NotFound();
        //     }
        //     _itemRepository.Delete(movie);
        //     _itemRepository.SaveChanges();
        //     return NoContent();
        // }
        
    }
}