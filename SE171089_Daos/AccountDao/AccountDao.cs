using Microsoft.EntityFrameworkCore;
using SE171089_BusinessObjects;

namespace SE171089_Daos.AccountDao
{
    public class AccountDao : IAccountDao
    {
        private static AccountDao? instance;
        private readonly LibraryManagementContext context;
        private AccountDao()
        {
            context = new LibraryManagementContext();
        }
        public static AccountDao Instance
        {
            get
            {
                instance ??= new AccountDao();
                return instance;
            }
        }

        public async Task<Account?> Add(Account obj)
        {
            Account? a = await GetAccountByEmail(obj.Email);
            if (a != null)
            {
                throw new DuplicateWaitObjectException(nameof(obj), "Email already exists");
            }
            context.Accounts.Add(obj);
            await context.SaveChangesAsync();
            Account? account = await context.Accounts.OrderByDescending(a => a.Id).FirstOrDefaultAsync();
            return account;
        }

        public async Task<Account?> Delete(int id)
        {
            Account? account = await GetOne(id) ?? throw new KeyNotFoundException("Account not found");
            account.Status = 0;
            account = await Update(account);
            return account;
        }

        public async Task<Account?> GetAccountByEmail(string email)
        {
            Account? account = await context.Accounts.FirstOrDefaultAsync(a => a.Email == email);
            return account;
        }

        public async Task<List<Account>> GetAccountsByRoleId(int roleId)
        {
            List<Account> accounts = await context.Accounts.Where(a => a.RoleId == roleId).ToListAsync();
            return accounts;
        }

        public async Task<List<Account>> GetActiveAccounts()
        {
            List<Account> accounts = await context.Accounts.Where(a => a.Status == 1).ToListAsync();
            return accounts;
        }

        public async Task<List<Account>> GetList()
        {
            List<Account> accounts = await context.Accounts.ToListAsync();
            return accounts;
        }

        public async Task<Account?> GetOne(int id)
        {
            Account? account = await context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
            return account;
        }

        public async Task<List<Account>> SearchAccounts(string keyword)
        {
            return await context.Accounts.Where(a => a.Email.Contains(keyword) || a.Username.Contains(keyword)).ToListAsync();
        }

        public async Task<Account?> Update(Account obj)
        {
            context.Accounts.Update(obj);
            await context.SaveChangesAsync();
            Account? account = await GetOne(obj.Id);
            return account;
        }
    }
}
