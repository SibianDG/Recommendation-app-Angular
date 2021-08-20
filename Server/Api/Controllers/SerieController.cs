using System;
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
    public class SerieController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public SerieController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        // [HttpGet("{id}")]
        // public ActionResult<Serie> GetItemById(int id)
        // {
        //     Item serie = _itemRepository.GetBy(id);
        //     if (serie == null) return NotFound();
        //     if (serie.GetType() != typeof(Serie)) return BadRequest();
        //     return (Serie) serie;
        // }
        //
        // [HttpPost]
        // public ActionResult<Serie> PostItem(SerieDTO serie)
        // {
        //     Serie itemToCreate = new Serie (serie.Title, serie.Summary, serie.Image, serie.Url, serie.NumberOfEpisodes);
        //     _itemRepository.Add(itemToCreate);
        //     _itemRepository.SaveChanges();
        //     
        //     return CreatedAtAction(nameof(GetItemById), new { id = itemToCreate.ItemId }, itemToCreate);
        // }
        
        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, Serie serie)
        {
            if (id != serie.ItemId)
            {
                return BadRequest();
            }
            _itemRepository.Update(serie);
            _itemRepository.SaveChanges();
            return NoContent();
        }
        //
        // [HttpDelete("{id}")]
        // public IActionResult DeleteItem(int id)
        // {
        //     Serie serie = (Serie) _itemRepository.GetBy(id);
        //     if (serie == null)
        //     {
        //         return NotFound();
        //     }
        //     _itemRepository.Delete(serie);
        //     _itemRepository.SaveChanges();
        //     return NoContent();
        // }
    }
}