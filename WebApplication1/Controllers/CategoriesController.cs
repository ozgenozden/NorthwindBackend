using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
           _categoryService = categoryService;
        }

        [HttpGet("getall")]
        public IActionResult Index()
        {
            var result = _categoryService.GetList();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);   
        }
    }
}
