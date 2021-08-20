using System;
using Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Api.Data.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Item> _items;
        private readonly DbSet<Recommendation> _recommendations;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
            _recommendations = context.Recommendations;
            _items = context.Items;
        }
        
        public Item GetBy(int id)
        {
            return _items.AsNoTracking().SingleOrDefault(i => i.ItemId == id);
        }

        public IEnumerable<Item> GetAll(int take, int skip)
        {
            return _items.AsNoTracking().OrderBy(i => i.ItemId).Skip(skip).Take(take).ToList();
        }

        public Item GetBestRecommendation(List<string> keywords, string type = null)
        {
            Item bestItem = CalculateItem(keywords, type);
            if (bestItem != null)
                return bestItem;
            bestItem = CalculateItem(null, type);
            if (bestItem != null)
                return bestItem;
            return null;
        }

        private Item CalculateItem(List<string> keywords, string type = null)
        {
            Random random = new Random();
            IList<Recommendation> bestRecommendationList = new List<Recommendation>();
            int highestMatches = 0;
            //TODO: performance
            IEnumerable<Recommendation> recommendations = _recommendations.AsNoTracking().Include(r => r.Items).ToList();
            
            foreach (Recommendation r in recommendations)
            {
                bool canContinue = false;
                string[] keywordsCurrentRecommendation = r.Keywords;
                int machtesThisOne = 0;

                if (!String.IsNullOrEmpty(type))
                {
                    if (new [] {"book", "movie", "serie", "movieserie"}.Contains(type))
                    {
                        foreach (Item item in r.Items)
                        {
                            if (string.Equals(item.TypeName, type, StringComparison.OrdinalIgnoreCase))
                                canContinue = true;
                        }
                    }
                }
                else
                    canContinue = true;

                if (!canContinue) continue;
                if (keywords == null || keywords.Count == 0 || keywords.Count == 1 && string.IsNullOrWhiteSpace(keywords[0]))
                    bestRecommendationList.Add(r);
                else
                {
                    machtesThisOne += keywords.Count(keyword => keywordsCurrentRecommendation.Any(x => string.Equals(x, keyword, StringComparison.CurrentCultureIgnoreCase)));
                    if (machtesThisOne >= highestMatches && machtesThisOne != 0)
                        bestRecommendationList.Add(r);
                }

            }

            if (bestRecommendationList.Count == 0)
                bestRecommendationList = recommendations.ToList();
            Recommendation bestRecommendation = bestRecommendationList[random.Next(bestRecommendationList.Count)];
            IEnumerable<Item> bestItemList = bestRecommendation.Items;

            if (!String.IsNullOrEmpty(type) && new [] {"book", "movie", "serie", "movieserie"}.Contains(type))
            {
                //TODO write better case: oefeningenles 1903
                switch (type.ToLower().Trim())
                {
                    case "book":
                        bestItemList = bestItemList.Where(x => x.TypeName.ToLower() == "book");
                        break;
                    case "movie":
                        bestItemList = bestItemList.Where(x => x.TypeName.ToLower() == "movie");
                        break;
                    case "serie":
                        bestItemList = bestItemList.Where(x => x.TypeName.ToLower() == "serie");
                        break;
                    case "movieserie":
                        bestItemList = bestItemList.Where(x => x.TypeName.ToLower() is "movie" or "serie");
                        break;
                    default:
                        break;
                }
            }

            //TODO: rekening houden met rating
            Item bestItem = bestItemList.ElementAtOrDefault(random.Next(bestItemList.Count()));
            return bestItem;
        }

        public void Add(Item item)
        {
            _items.Add(item);
        }

        public void Delete(Item item)
        {
            _items.Remove(item);
        }

        public void Update(Item item)
        {
            _context.Update(item);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}