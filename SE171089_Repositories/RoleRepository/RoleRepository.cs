using SE171089_BusinessObjects;
using SE171089_Daos.RoleDao;

namespace SE171089_Repositories.RoleRepository
{
    public class RoleRepository : IRoleRepository
    {
        private static RoleRepository? instance;
        private readonly IRoleDao roleDao;
        private RoleRepository()
        {
            roleDao = RoleDao.Instance;
        }
        public static RoleRepository Instance
        {
            get
            {
                instance ??= new RoleRepository();
                return instance;
            }
        }

        public async Task<Role?> Add(Role obj)
        {
            return await roleDao.Add(obj);
        }

        public async Task<Role?> Delete(int id)
        {
            return await roleDao.Delete(id);
        }

        public async Task<List<Role>> GetList()
        {
            return await roleDao.GetList();
        }

        public async Task<Role?> GetOne(int id)
        {
            return await roleDao.GetOne(id);    
        }

        public async Task<Role?> Update(Role obj)
        {
            return await roleDao.Update(obj);
        }
    }
}
