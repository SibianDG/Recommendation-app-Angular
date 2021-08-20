using System;
using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Recommendation> _recommendations;

        public RecommendationRepository(ApplicationDbContext Dbcontext)
        {
            _context = Dbcontext;
            _recommendations = _context.Recommendations;
        }

        public Recommendation GetBy(int id)
        {
            return _recommendations.AsNoTracking().Include(r => r.Items).SingleOrDefault(k => k.RecommendationId == id);
        }

        public IEnumerable<Recommendation> GetAll(int take, int skip)
        {
            return _recommendations.AsNoTracking().OrderBy(r => r.RecommendationId).Skip(skip).Take(take).Include(r => r.Items).ToList();
        }
        
        public void Add(Recommendation recommendation)
        {
            _recommendations.Add(recommendation);
        }

        public void Delete(Recommendation recommendation)
        {
            _recommendations.Remove(recommendation);
        }

        public void Update(Recommendation recommendation)
        {
            _context.Update(recommendation);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}