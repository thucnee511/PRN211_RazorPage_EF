using Microsoft.EntityFrameworkCore;
using SE171089_BusinessObjects;

namespace SE171089_Daos.RoleDao
{
    public class RoleDao : IRoleDao
    {
        private static RoleDao? instance; 
        private readonly LibraryManagementContext context;
        private RoleDao()
        {
            context = new LibraryManagementContext();
        }
        public static RoleDao Instance
        {
            get
            {
                instance ??= new RoleDao();
                return instance;
            }
        }

        public async Task<Role?> Add(Role obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj), "Role object is null");
            }
            context.Roles.Add(obj);
            await context.SaveChangesAsync();
            Role? role = await context.Roles.OrderByDescending(r => r.Id).FirstOrDefaultAsync();
            return role;
        }

        public async Task<Role?> Delete(int id)
        {
            Role? role = await GetOne(id);
            if (role != null)
            {
                context.Roles.Remove(role);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Role not found");
            }
            return role;
        }

        public async Task<List<Role>> GetList()
        {
            List<Role> list = await context.Roles.ToListAsync();
            return list;
        }

        public async Task<Role?> GetOne(int id)
        {
            Role? role = await context.Roles.FindAsync(id);
            return role;
        }

        public async Task<Role?> Update(Role obj)
        {
            context.Roles.Update(obj);
            await context.SaveChangesAsync();
            Role? role = await GetOne(obj.Id);
            return role;
        }
    }
}
