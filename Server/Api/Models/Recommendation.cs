using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Models
{
    public class Recommendation
    {
        #region Properties
        public int RecommendationId { get; set; }
        public double Rating { get; set; }
        public ICollection<Item> Items { get; set; }
        //https://entityframeworkcore.com/knowledge-base/61015049/how-to-add-list-of-ingredients-to-recipe-asp-net-core-mvc
        public string[] Keywords { get; set; }
        #endregion
        
        #region Constructors
        public Recommendation(string[] keywords, ICollection<Item> items)
        {
            Keywords = keywords;
            Items = items;
        }
        public Recommendation()
        {
            Keywords = new string[1];
            Items = new List<Item>();
        }
        #endregion
        
        #region Methods

        public void AddItemToRecommendation(Item item)
        {
            Items.Add(item);
        }

        public void AddKeyWordToRecommendation(string keyword)
        {
            //https://stackoverflow.com/questions/202813/adding-values-to-a-c-sharp-array 
            Keywords = Keywords.Concat(new string[] { keyword }).ToArray();
        }

        public void DeleteItemFromRecommendation(Item item)
        {
            Items.Remove(item);
        }
        public void DeleteKeywordFromRecommendation(string keyword)
        {
            throw new NotImplementedException();
        }

        public void AddRating(double rating)
        {
            Rating = rating;
        }
        #endregion
        
        
    }
}