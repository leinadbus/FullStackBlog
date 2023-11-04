using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogpostController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogpostController(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }


        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {
            //Converto DATA FROM DTO to DOMAIN MODEL
            var BlogPost = new BlogPost
            {
                Author = request.Author,
                Tittle = request.Title,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.UrlHandle
            };

            BlogPost = await blogPostRepository.CreateAsync(BlogPost);

            //convert DOMAIN MODEL back to DTO
            var response = new BlogPostDto
            {
                Id = BlogPost.Id,
                Author = request.Author,
                Title = request.Title,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                UrlHandle = request.UrlHandle
            };
            return Ok(response);
        }
    }
}
