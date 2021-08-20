using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Api.DTOs;
using Api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationRepository _recommendationRepository;
        private readonly IItemRepository _itemRepository;

        public RecommendationController(IRecommendationRepository recommendationRepository, IItemRepository iteRepository)
        {
            _recommendationRepository = recommendationRepository;
            _itemRepository = iteRepository;
        }

        /// <summary>
        /// Gets all the Recommendations with the corresponding take and skip parameters.
        /// </summary>
        /// <param name="take">how many recommendations you want to take</param>
        /// <param name="skip">how many recommendations you want to skip</param>
        /// <returns>A list of recommendations</returns>
        [HttpGet]
        public IEnumerable<Recommendation> GetAll(int take = 10, int skip = 0)
        {
            return _recommendationRepository.GetAll(take, skip);
        }
        
        /// <summary>
        /// Gets the Recommendation with the given ID
        /// </summary>
        /// <param name="id">The id of the recommendation</param>
        /// <returns>The recommendation</returns>
        [HttpGet("{id}")]
        public ActionResult<Recommendation> GetItemById(int id)
        {
            Recommendation recommendation = _recommendationRepository.GetBy(id);
            if (recommendation == null) return NotFound();
            return recommendation;
        }

        /// <summary>
        /// Posts / Inserts the given recommendation
        /// </summary>
        /// <param name="recommendation">the recommendation you want to add</param>
        /// <returns>The recommendation you've added with the corresponding ID</returns>
        [HttpPost]
        public ActionResult<Recommendation> PostItem(RecommendationDTO recommendation)
        {
            List<Item> items = new List<Item>();

            foreach (ItemDTO recommendationItem in recommendation.Items)
            {
                Item itemToCreate = null;
                if (string.Equals(recommendationItem.TypeName, "book", StringComparison.OrdinalIgnoreCase))
                    itemToCreate = new Book(recommendationItem.Title, recommendationItem.Summary,recommendationItem.Image,recommendationItem.Url,recommendationItem.Pages);
                if (string.Equals(recommendationItem.TypeName, "movie", StringComparison.OrdinalIgnoreCase))
                    itemToCreate = new Movie(recommendationItem.Title,recommendationItem.Summary,recommendationItem.Image,recommendationItem.Url,recommendationItem.Duration);
                if (string.Equals(recommendationItem.TypeName, "serie", StringComparison.OrdinalIgnoreCase))
                    itemToCreate = new Serie(recommendationItem.Title,recommendationItem.Summary,recommendationItem.Image,recommendationItem.Url,recommendationItem.NumberOfEpisodes);

                if (itemToCreate == null)
                    return BadRequest();
                _itemRepository.Add(itemToCreate);
                items.Add(itemToCreate);
            }
            _itemRepository.SaveChanges();

            Recommendation recommendationToCreate = new Recommendation(recommendation.Keywords, items);
            _recommendationRepository.Add(recommendationToCreate);
            _recommendationRepository.SaveChanges();
          
            return CreatedAtAction(nameof(GetItemById), new { id = recommendationToCreate.RecommendationId }, recommendationToCreate);
            //return NotFound();
        }
        
        /// <summary>
        /// Puts / Updates the given recommendation with the given ID
        /// </summary>
        /// <param name="id">the ID of the recommendation</param>
        /// <param name="recommendation">the recommendation</param>
        /// <returns>An action result</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, Recommendation recommendation)
        {
            Console.WriteLine(id);
            if (id != recommendation.RecommendationId)
            {
                return BadRequest();
            }
            _recommendationRepository.Update(recommendation);
            _recommendationRepository.SaveChanges();
            return NoContent();
        }
        
        /// <summary>
        /// Deletes the recommendation with the given ID
        /// </summary>
        /// <param name="id">the id of the given recommendation</param>
        /// <returns>An action result</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            Recommendation recommendation = _recommendationRepository.GetBy(id);
            if (recommendation == null)
            {
                return NotFound();
            }
            _recommendationRepository.Delete(recommendation);
            _recommendationRepository.SaveChanges();
            return NoContent();
        }
    }
}
