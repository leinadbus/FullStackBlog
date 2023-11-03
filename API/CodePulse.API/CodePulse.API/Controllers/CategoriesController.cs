﻿using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
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

            await categoryRepository.CreateAsync(Category);

            //Domain model to DTO
            var response = new CategoryDto
            {
                Id = Category.Id,
                Name = Category.Name,
                UrlHandle = Category.UrlHandle,
            };

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await categoryRepository.GetAllAsync();

            //MAP DOMAIN MODEL TO DTO
            var Response = new List<CategoryDto>();

            foreach ( var category in categories)
            {
                Response.Add (new CategoryDto 
                { 
                    Id = category.Id, 
                    Name = category.Name, 
                    UrlHandle=category.UrlHandle 
                });
            }

            return Ok(Response);
        }
    }
}
