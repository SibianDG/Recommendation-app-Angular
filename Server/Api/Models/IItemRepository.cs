using System.Collections.Generic;

namespace Api.Models
{
    public interface IItemRepository
    {
        Item GetBy(int id);
        IEnumerable<Item> GetAll(int take, int skip);
        Item GetBestRecommendation(List<string> keywords, string type);
        void Add(Item item);
        void Delete(Item item);
        void Update(Item item);
        void SaveChanges();
    }
}