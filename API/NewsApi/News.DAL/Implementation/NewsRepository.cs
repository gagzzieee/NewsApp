using Microsoft.EntityFrameworkCore;
using News.DAL.Context;
using News.DAL.Interfaces;
using News.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.DAL.Implementation
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsContext _context;

        public NewsRepository(NewsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NewsArticle>> GetAllAsync()
        {
            return await _context.GdNewsArticles.Where(n => !n.IsDeleted).ToListAsync();
        }

        public async Task<NewsArticle> GetByIdAsync(int id)
        {
            return await _context.GdNewsArticles.FirstOrDefaultAsync(n => n.Id == id && !n.IsDeleted);
        }

        public async Task AddAsync(NewsArticle newsArticle)
        {
            await _context.GdNewsArticles.AddAsync(newsArticle);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(NewsArticle newsArticle)
        {
            _context.GdNewsArticles.Update(newsArticle);
            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            var newsArticle = await _context.GdNewsArticles.FindAsync(id);
            if (newsArticle != null)
            {
                newsArticle.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }


}

