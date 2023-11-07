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
        private readonly ICategoryRepository categoryRepository;

        public BlogpostController(IBlogPostRepository blogPostRepository,
            ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
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
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            foreach(var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetByIdAsync(categoryGuid);
                if (existingCategory != null)
                {
                    BlogPost.Categories.Add(existingCategory);
                }
            }

            BlogPost = await blogPostRepository.CreateAsync(BlogPost);

            //convert DOMAIN MODEL back to DTO
            var response = new BlogPostDto
            {
                Id = BlogPost.Id,
                Author = BlogPost.Author,
                Title = BlogPost.Tittle,
                Content = BlogPost.Content,
                FeaturedImageUrl = BlogPost.FeaturedImageUrl,
                IsVisible = BlogPost.IsVisible,
                PublishedDate = BlogPost.PublishedDate,
                ShortDescription = BlogPost.ShortDescription,
                UrlHandle = BlogPost.UrlHandle,
                Categories = BlogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPost()
        {
            var blogPost = await blogPostRepository.GetAllAsync();

            //Convert DOMAIN MODEL to Dto

            var response = new List<BlogPostDto>();

            foreach (var BlogPost in blogPost)
            {
                response.Add(new BlogPostDto
                {
                    Id = BlogPost.Id,
                    Author = BlogPost.Author,
                    Title = BlogPost.Tittle,
                    Content = BlogPost.Content,
                    FeaturedImageUrl = BlogPost.FeaturedImageUrl,
                    IsVisible = BlogPost.IsVisible,
                    PublishedDate = BlogPost.PublishedDate,
                    ShortDescription = BlogPost.ShortDescription,
                    UrlHandle = BlogPost.UrlHandle,
                    Categories = BlogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });
                
            }
            return Ok(response);
        }
    }
}
