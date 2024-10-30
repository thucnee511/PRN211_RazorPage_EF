using SE171089_BusinessObjects;
using SE171089_Daos.CategoryDao;

namespace SE171089_Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private static CategoryRepository? instance;
        private readonly ICategoryDao categoryDao;
        private CategoryRepository()
        {
            categoryDao = CategoryDao.Instance;
        }
        public static CategoryRepository Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Category?> Add(Category obj)
        {
            return await categoryDao.Add(obj);
        }

        public async Task<Category?> Delete(int id)
        {
            return await categoryDao.Delete(id);
        }

        public async Task<List<Category>> GetList()
        {
            return await categoryDao.GetList();
        }

        public async Task<Category?> GetOne(int id)
        {
            return await categoryDao.GetOne(id);
        }

        public async Task<Category?> Update(Category obj)
        {
            return await categoryDao.Update(obj);
        }
    }
}
