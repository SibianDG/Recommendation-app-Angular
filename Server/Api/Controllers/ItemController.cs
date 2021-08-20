using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Api.DTOs;
using Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PuppeteerSharp;

namespace Api.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICustomerRepository _customerRepository;


        public ItemsController(IItemRepository itemRepository, ICustomerRepository customerRepository)
        {
            _itemRepository = itemRepository;
            _customerRepository = customerRepository;
        }

        /// <summary>
        /// Get Items
        /// </summary>
        /// <param name="take">the number you want to take</param>
        /// <param name="skip">the number you want to skip</param>
        /// <returns>List of Items</returns>
        [HttpGet]
        public IEnumerable<Item> GetItems(int take = 10, int skip = 0)
        {
            return _itemRepository.GetAll(take, skip).OrderBy(i => i.Title);
        }
        
        /// <summary>
        /// Get the item with given id
        /// </summary>
        /// <param name="id">the id of the item</param>
        /// <returns>The Item</returns>
        [HttpGet("{id}")]
        /*TODO: let in in?*///[AllowAnonymous]
        public ActionResult<Item> GetItemById(int id)
        {
            Item item = _itemRepository.GetBy(id);
            if (item == null) return NotFound();
            return item;
        }

        /// <summary>
        /// Gets best recommendation item with the given keywords and type
        /// </summary>
        /// <param name="keywords">the keywords that gives the beste recommendation item</param>
        /// <param name="type">the type (book, movie, serie) that gives the beste recommendation item</param>
        /// <returns>The best recommendation item</returns>
        [HttpGet("getrecommendationitem/")]
        /*TODO: let in in?*///[AllowAnonymous]
        public ActionResult<Item> GetBestRecommendation(string keywords, string type)
        {
            String[] keywordsArray;
            if (keywords == null || keywords.Trim().Equals(""))
                keywordsArray = Array.Empty<string>();
            else
                keywordsArray = keywords.Split(',');
            List<string> keywordList = new List<string>(keywordsArray);
            Item item = _itemRepository.GetBestRecommendation(keywordList, type);
            if (item == null) return NotFound();
            //For handling a cycle, because [JsonIgnore] & [IgnoreDataMember] doesn't work...
            item.Recommendations = null;

            return item;
            //return NotFound();
        }

        
        [HttpPost]
        public ActionResult<Item> PostItem(ItemDTO item)
        {
            Item itemToCreate = null;
            if (string.Equals(item.TypeName, "book", StringComparison.OrdinalIgnoreCase))
                itemToCreate = new Book(item.Title, item.Summary, item.Image, item.Url, item.Pages);
            if (string.Equals(item.TypeName, "movie", StringComparison.OrdinalIgnoreCase))
                itemToCreate = new Movie(item.Title, item.Summary, item.Image, item.Url, item.Duration);
            if (string.Equals(item.TypeName, "serie", StringComparison.OrdinalIgnoreCase))
                itemToCreate = new Serie(item.Title, item.Summary, item.Image, item.Url, item.NumberOfEpisodes);

            if (itemToCreate == null)
                return BadRequest();
            _itemRepository.Add(itemToCreate);
            _itemRepository.SaveChanges();
            
            return CreatedAtAction(nameof(GetItemById), new { id = itemToCreate.ItemId }, itemToCreate);
        }
        
        // /// <summary>
        // /// Updates an item with the given id
        // /// </summary>
        // /// <param name="id">the id of the item</param>
        // /// <param name="item">the item you want to update</param>
        // /// <returns>An action result</returns>
        // [HttpPut("{id}")]
        // [AllowAnonymous]
        // public IActionResult UpdateItem(int id, Item item)
        // {
        //     if (id != item.ItemId)
        //     {
        //         return BadRequest();
        //     }
        //     _itemRepository.Update(item);
        //     _itemRepository.SaveChanges();
        //     return NoContent();
        // }
        
        /// <summary>
        /// Deletes an item with the given ID
        /// </summary>
        /// <param name="id">the id of the item you want to delete</param>
        /// <returns>An action result</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            Item item = _itemRepository.GetBy(id);
            if (item == null)
            {
                return NotFound();
            }
            _itemRepository.Delete(item);
            _itemRepository.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Fetch URL: return an Item filled with information of the site.
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        [HttpGet("FetchURL/")]
        [AllowAnonymous]
        public async Task<ActionResult<ItemDTO>> FetchURL(string link)
        {
            if (link == null || link.Trim() == "" || !link.Contains("standaardboekhandel.be"))
                return BadRequest();

            try
            {
                Console.WriteLine("Downloading chromium");
                await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
                var options = new LaunchOptions {Headless = true};
                using (var browser = await Puppeteer.LaunchAsync(options))
                using (var page = await browser.NewPageAsync())
                {
                    await page.GoToAsync(link);
                    var title = await page.QuerySelectorAsync(".c-product-detail__title--badge")
                        .EvaluateFunctionAsync<string>("_ => _.innerText");
                    var summary = await page.QuerySelectorAsync(".c-product-detail__description")
                        .EvaluateFunctionAsync<string>("_ => _.lastElementChild.innerHTML");
                    summary = summary.Trim();
                    summary = summary.Replace("<p>", " ");
                    summary = summary.Replace("</p>", " ");
                    summary = summary.Replace("<br>", "\n");

                    var pageList = await page.EvaluateFunctionAsync(@"(page) => {
                    const anchors = Array.from(document.querySelectorAll(page));
                    return anchors.map(anchor => anchor.innerText)}",
                        ".c-list--semantic.c-product-specs > li:nth-child(1)");
                    string pageSize = (string) pageList[1];
                    pageSize = pageSize.Split(':')[1];
                    Regex.Replace(pageSize, @"\s+", "");

                    var imageList = await page.EvaluateFunctionAsync(
                        @"(x) => Array.from(document.querySelectorAll(x)).map(a => a.href)",
                        ".c-product__slider a[data-lightbox=\"products\"]");
                    //imageList contains front en back. TODO: implement back into application :)
                    var front = imageList[0];

                    ItemDTO item = new ItemDTO
                        {Title = title, Summary = summary, Pages = Int32.Parse(pageSize), Image = (string) front, TypeName = "Book"};

                    return item;
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
