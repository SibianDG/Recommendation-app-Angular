using System.Collections.Generic;

namespace Api.Models
{
    public interface IRecommendationRepository
    {
        Recommendation GetBy(int id);
        IEnumerable<Recommendation> GetAll(int take, int skip);
        void Add(Recommendation recommendation);
        void Delete(Recommendation recommendation);
        void Update(Recommendation recommendation);
        void SaveChanges();
    }
}