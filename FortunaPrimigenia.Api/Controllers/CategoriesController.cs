using FortunaPrimigenia.Api.Data;
using FortunaPrimigenia.Api.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FortunaPrimigenia.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriesController(FortunaPrimigeniaContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<IAsyncEnumerable<Category>> GetAllCategoriesAsync()
    {
        return dbContext.Categories.ToAsyncEnumerable();
    }
}