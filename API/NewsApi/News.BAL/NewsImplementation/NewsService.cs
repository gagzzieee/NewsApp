using News.BAL.NewsInterface;
using News.DAL.Interfaces;
using News.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.BAL.NewsImplementation
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository _newsRepository;

        public NewsService(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public async Task<IEnumerable<NewsArticle>> GetAllAsync()
        {
            return await _newsRepository.GetAllAsync();
        }

        public async Task<NewsArticle> GetByIdAsync(int id)
        {
            return await _newsRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(NewsArticle newsArticle)
        {
            await _newsRepository.AddAsync(newsArticle);
        }

        public async Task UpdateAsync(NewsArticle newsArticle)
        {
            await _newsRepository.UpdateAsync(newsArticle);
        }

        public async Task SoftDeleteAsync(int id)
        {
            await _newsRepository.SoftDeleteAsync(id);
        }
    }
}
