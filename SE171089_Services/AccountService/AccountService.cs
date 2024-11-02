using SE171089_BusinessObjects;
using SE171089_Repositories.AccountRepository;
using SE171089_Repositories.RoleRepository;

namespace SE171089_Services.AccountService
{
    public class AccountService : IAccountService
    {
        private static AccountService? instance;
        private readonly IAccountRepository accountRepository;
        private readonly IRoleRepository roleRepository;
        private AccountService()
        {
            accountRepository = AccountRepository.Instance;
            roleRepository = RoleRepository.Instance;
        }
        public static AccountService Instance
        {
            get
            {
                instance ??= new();
                return instance;
            }
        }

        public async Task<Account?> Delete(int id)
        {
            return await accountRepository.Delete(id);
        }

        public async Task<Account?> GetAccountById(int v)
        {
            return await accountRepository.GetOne(v);
        }

        public async Task<IList<Account>> getActiveAccounts()
        {
            return await accountRepository.GetActiveAccounts();
        }

        public async Task<Role?> GetRole(int id)
        {
            return await roleRepository.GetOne(id);
        }

        public async Task<Account?> Insert(Account account)
        {
            return await accountRepository.Add(account);
        }

        public async Task<Account?> Login(string email, string password)
        {
            Account? account = await accountRepository.GetAccountByEmail(email)
                ?? throw new Exception("Wrong login creadentials");
            if (account.Password != password)
            {
                throw new Exception("Wrong login creadentials");
            }
            return account;
        }

        public async Task<Account?> Update(Account account)
        {
            return await accountRepository.Update(account);
        }
    }
}
