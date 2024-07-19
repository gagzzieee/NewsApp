using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using News.BAL.NewsInterface;
using News.DAL.Models;

namespace NewsApi.Controllers
{
   [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyAllowSpecificOrigins")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsArticle>>> GetAll()
        {
            var newsArticles = await _newsService.GetAllAsync();
            return Ok(newsArticles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NewsArticle>> GetById(int id)
        {
            var newsArticle = await _newsService.GetByIdAsync(id);
            if (newsArticle == null)
            {
                return NotFound();
            }
            return Ok(newsArticle);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromForm] NewsArticle newsArticle, [FromForm] IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await image.CopyToAsync(ms);
                    newsArticle.ImageBase64 = Convert.ToBase64String(ms.ToArray());
                }
            }
            else
            {
                newsArticle.ImageBase64 = null;
            }

            await _newsService.AddAsync(newsArticle);
            return CreatedAtAction(nameof(GetById), new { id = newsArticle.Id }, newsArticle);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] NewsArticle newsArticle, [FromForm] IFormFile image)
        {
            if (id != newsArticle.Id)
            {
                return BadRequest();
            }

            if (image != null && image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await image.CopyToAsync(ms);
                    newsArticle.ImageBase64 = Convert.ToBase64String(ms.ToArray());
                }
            }

            await _newsService.UpdateAsync(newsArticle);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> SoftDelete(int id)
        {
            await _newsService.SoftDeleteAsync(id);
            return NoContent();
        }
    }
}
