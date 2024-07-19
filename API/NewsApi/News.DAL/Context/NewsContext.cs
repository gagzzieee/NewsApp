using Microsoft.EntityFrameworkCore;
using News.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.DAL.Context
{
    public class NewsContext : DbContext
    {
        public NewsContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Models.NewsArticle> GdNewsArticles  { get; set; }
    }
}
