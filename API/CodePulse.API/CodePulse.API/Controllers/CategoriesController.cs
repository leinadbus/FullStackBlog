using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ApplicationDbContext dbContext; 
        public CategoriesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestDto request)
        {
            //Map DTO to Domain Model

            var Category = new Category
            {
                Name = request.Name,
                UrlHandle = request.UrlHandle,
            };

            await dbContext.Categorys.AddAsync(Category);

            await dbContext.SaveChangesAsync();

            //Domain model to DTO

            var response = new CategoryDto
            {
                Id = Category.Id,
                Name = Category.Name,
                UrlHandle = Category.UrlHandle,
            };

            return Ok(response);
        }
    }
}
