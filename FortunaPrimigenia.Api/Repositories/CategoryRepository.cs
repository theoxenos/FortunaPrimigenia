using FortunaPrimigenia.Api.Data;
using FortunaPrimigenia.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FortunaPrimigenia.Api.Repositories;

public interface ICategoryRepository
{
    public Task<List<Category>> GetAllCategoriesAsync();
    public Task<Category?> GetCategoryByIdAsync(int categoryId);
    public Task<Category?> GetCategoryByNameAsync(string categoryName);
    public Task<Category?> CreateCategoryAsync(Category category);
    public Task<Category?> UpdateCategoryAsync(Category category);
    public Task<bool> DeleteCategoryAsync(int categoryId);
}

public class CategoryRepository(FortunaPrimigeniaContext dbContext) : ICategoryRepository
{
    public Task<List<Category>> GetAllCategoriesAsync()
    {
        return dbContext.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAsync(int categoryId)
    {
        return await dbContext.Categories.FindAsync(categoryId);
    }

    public async Task<Category?> GetCategoryByNameAsync(string categoryName)
    {
        return await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
    }

    public async Task<Category?> CreateCategoryAsync(Category category)
    {
        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync();
        return category;
    }

    public async Task<Category?> UpdateCategoryAsync(Category category)
    {
        dbContext.Categories.Update(category);
        await dbContext.SaveChangesAsync();
        return category;
    }

    public async Task<bool> DeleteCategoryAsync(int categoryId)
    {
        var category = await dbContext.Categories.FindAsync(categoryId);
        if (category == null) return false;

        dbContext.Categories.Remove(category);
        await dbContext.SaveChangesAsync();
        return true;
    }
}