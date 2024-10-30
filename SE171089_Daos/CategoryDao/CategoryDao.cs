using Microsoft.EntityFrameworkCore;
using SE171089_BusinessObjects;

namespace SE171089_Daos.CategoryDao
{
    public class CategoryDao : ICategoryDao
    {
        private static CategoryDao? instance;
        private readonly LibraryManagementContext context;
        private CategoryDao()
        {
            context = new LibraryManagementContext();
        }
        public static CategoryDao Instance
        {
            get
            {
                instance ??= new CategoryDao();
                return instance;
            }
        }

        public async Task<Category?> Add(Category obj)
        {
            context.Categories.Add(obj);
            await context.SaveChangesAsync();
            Category? category = await context.Categories.OrderByDescending(c => c.Id).FirstOrDefaultAsync();
            return category;
        }

        public Task<Category?> Delete(int id)
        {
            throw new NotSupportedException("This method is not supported");
        }

        public async Task<List<Category>> GetList()
        {
            List<Category> categories = await context.Categories.ToListAsync();
            return categories;
        }

        public async Task<Category?> GetOne(int id)
        {
            Category? category = await context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public Task<Category?> Update(Category obj)
        {
            throw new NotSupportedException("This method is not supported");
        }
    }
}
