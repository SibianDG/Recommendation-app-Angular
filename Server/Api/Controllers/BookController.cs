using System;
using Api.DTOs;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public BookController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        
        // [HttpGet("{id}")]
        // public ActionResult<Book> GetItemById(int id)
        // {
        //     Item book = _itemRepository.GetBy(id);
        //     if (book == null) return NotFound();
        //     if (book.GetType() != typeof(Book)) return BadRequest();
        //     return (Book) book;
        // }
        //
        // [HttpPost]
        // public ActionResult<Book> PostItem(BookDTO book)
        // {
        //     Book itemToCreate = new Book (book.Title, book.Summary, book.Image, book.Url, book.Pages);
        //     _itemRepository.Add(itemToCreate);
        //     _itemRepository.SaveChanges();
        //     
        //     return CreatedAtAction(nameof(GetItemById), new { id = itemToCreate.ItemId }, itemToCreate);
        // }
        
        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, Book book)
        {
            if (id != book.ItemId)
            {
                return BadRequest();
            }

            _itemRepository.Update(book);
            _itemRepository.SaveChanges();
            return NoContent();
        }
        
        // [HttpDelete("{id}")]
        // public IActionResult DeleteItem(int id)
        // {
        //     Book book = (Book) _itemRepository.GetBy(id);
        //     if (book == null)
        //     {
        //         return NotFound();
        //     }
        //     _itemRepository.Delete(book);
        //     _itemRepository.SaveChanges();
        //     return NoContent();
        // }
    }
}