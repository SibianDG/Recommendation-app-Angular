using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Api.Models
{
    public class Item
    {
        #region Properties
        public int  ItemId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        
        public string TypeName { get; protected set;}
        
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Recommendation> Recommendations { get; set;}
        #endregion

        public Item()
        {
        }

        public Item(string title, string summary)
        {
            Title = title;
            Summary = summary;
        }
        public Item(string title, string summary, string image = null, string url = null) : this(title, summary)
        {
            Image = image;
            Url = url;
        }

        public void AddToRecommendations(Recommendation recommendation)
        {
            throw new NotImplementedException();
        }

    }
}