using SE171089_BusinessObjects;
using SE171089_Daos.AccountDao;

namespace SE171089_Repositories.AccountRepository
{
    public class AccountRepository : IAccountRepository
    {
        private static AccountRepository? instance;
        private readonly IAccountDao accountDao;
        private AccountRepository()
        {
            accountDao = AccountDao.Instance;
        }
        public static AccountRepository Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Account?> Add(Account obj)
        {
            return await accountDao.Add(obj);
        }

        public async Task<Account?> Delete(int id)
        {
            return await accountDao.Delete(id);
        }

        public async Task<Account?> GetAccountByEmail(string email)
        {
            return await accountDao.GetAccountByEmail(email);
        }

        public async Task<List<Account>> GetAccountsByRoleId(int roleId)
        {
            return await accountDao.GetAccountsByRoleId(roleId);
        }

        public async Task<List<Account>> GetActiveAccounts()
        {
            return await accountDao.GetActiveAccounts();
        }

        public async Task<List<Account>> GetList()
        {
            return await accountDao.GetList();
        }

        public async Task<Account?> GetOne(int id)
        {
            return await accountDao.GetOne(id);
        }

        public async Task<List<Account>> SearchAccounts(string keyword)
        {
            return await accountDao.SearchAccounts(keyword);
        }

        public async Task<Account?> Update(Account obj)
        {
            return await accountDao.Update(obj);
        }
    }
}
