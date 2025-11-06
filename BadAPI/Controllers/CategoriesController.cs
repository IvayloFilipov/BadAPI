using BadAPI.Data.Entities;
using BadAPI.Data.Interfaces;
using BadAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

using static Common.GlobalConstants;

namespace BadApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        //private CategoryService _service = new CategoryService();
        //private CategoryRepository _repo = new CategoryRepository();
        private readonly ICategoryService categoryService;
        private readonly ICategoryRepository categoryRepo;

        public CategoriesController(ICategoryService categoryService, ICategoryRepository categoryRepo)
        {
            this.categoryService = categoryService;
            this.categoryRepo = categoryRepo;
        }

        //[HttpGet]
        //public ActionResult Get()
        //{
        //    var categories = _service.GetCategories();
        //    return Ok(categories);
        //}

        [HttpGet("getcategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await categoryService.GetCategoriesAsync();

                return Ok(categories);

            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpGet("{id}")]
        //public ActionResult Get(int id)
        //{
        //    var category = _repo.GetById(id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }

        //    if (category.Description != null && category.Description.Length > 100)
        //    {
        //        return Ok(new { category.Id, category.Name, Note = Long_Description });
        //    }

        //    return Ok(category);
        //}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var category = await categoryRepo.GetCategooryByIdAsync(id);

                if (category == null)
                {
                    return NotFound();
                }

                if (category.Description != null && category.Description.Length > 100)
                {
                    return Ok(new { category.Id, category.Name, Note = Long_Description });
                }

                return Ok(category);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpPost]
        //public ActionResult Post(Category category)
        //{
        //    var result = _service.AddCategory(category);
        //    if (result == Category_Name_Is_Required)
        //        return BadRequest(result);

        //    return Ok(result);
        //}
        [HttpPost("add")]
        public async Task<IActionResult> AddCategory(Category category)
        {
            try
            {
                var result = await categoryService.AddCategoryAsync(category);

                if (result == Category_Name_Is_Required)
                {
                    return BadRequest(result);
                }

                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPut("{id}")]
        //public ActionResult Put(int id, Category category)
        //{
        //    if (id != category.Id)
        //        return BadRequest(Id_Mismatch);

        //    var existing = _repo.GetById(id);
        //    if (existing == null)
        //        return NotFound();

        //    existing.Name = category.Name;
        //    existing.Description = category.Description;

        //    _repo.Update(existing);

        //    return Ok(Updated);
        //}
        [HttpPut("{categoryId}")]
        public async Task<ActionResult> UpdateCategory(int id, Category category)
        {
            try
            {
                if (id != category.Id)
                    return BadRequest(Id_Mismatch);

                var existing = await categoryRepo.GetCategooryByIdAsync(id);

                if (existing == null)
                    return NotFound();

                existing.Name = category.Name;
                existing.Description = category.Description;

                await categoryRepo.UpdateCategoryAsync(existing);

                return Ok(Updated);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[HttpDelete("{id}")]
        //public ActionResult Delete(int id)
        //{
        //    var category = _repo.GetById(id);
        //    if (category == null)
        //        return NotFound();

        //    _repo.Delete(id);

        //    return Ok(Deleted);
        //}
        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await categoryRepo.GetCategooryByIdAsync(id);

                if (category == null)
                    return NotFound();

                await categoryRepo.DeleteCategoryAsync(id);

                return Ok(Deleted);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
